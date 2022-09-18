using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject myradov;
    public GameObject Canvas;
    public float lookRadius;
    public NavMeshAgent agent;
    public float health;
    public IntroIII_theFight godscript;
    public Animator enemyMoves;

    private Vector3 previousPosition;
    public float curSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lookRadius = 300f;
        health = 100f;
        godscript = Canvas.GetComponent<IntroIII_theFight>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -300){
            moveTowards();
        }
        apoptosis();
        calculatespeed();
        //calculateAnimation();
        //print(curSpeed);
        //print((myradov.transform.position - transform.position).magnitude);
    }

    void moveTowards(){
        if((myradov.transform.position - transform.position).magnitude < lookRadius && (myradov.transform.position - transform.position).magnitude > 50){
            agent.SetDestination(myradov.transform.position);

        }
        Vector3 direction = (myradov.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void takeDamage(float damage){
        health -= damage;
        //print(health);
    }

    void apoptosis(){
        if(health <= 0){
            godscript.numEnemies -= 1;
            Destroy(gameObject);
        }
    }

    void calculatespeed(){
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
    }

    void calculateAnimation(){
        if(curSpeed < 0.1){
            enemyMoves.SetBool("movement", false);
        }else{
            enemyMoves.SetBool("movement", true);
        }
    }
}
