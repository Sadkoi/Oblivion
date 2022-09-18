using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroIII_pistol : MonoBehaviour
{
    public GameObject pistolbody;
    public GameObject pistoltop;
    public GameObject pistolfire;
    public GameObject cockBack;
    public IntroIII_theFight script;

    public GameObject cam;
    public Light gunshot;
    public float bright;
    
    private float speed; 
    public int range;
    
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = pistoltop.transform.position - pistolbody.transform.position;
        speed = 20f;
        gunshot.intensity = 0;
        bright = 20;
        range = 3000;
    }

    void Update(){
        if(Input.GetKey(KeyCode.Mouse0) && script.playerAssumedControl){
            Fire();
        }
    }


    public void Fire(){
        //print("I wanna be in the cavalry");
        StartCoroutine("firePistol");

        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green , range);
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
            //print(hit.transform.name);
            if(hit.transform.tag == "enemy"){
                enemyBehaviour enemyScript = hit.transform.GetComponent<enemyBehaviour>();
                if(enemyScript != null){
                    enemyScript.takeDamage(25f);
                }
            }
        }
    }

    IEnumerator firePistol(){
        for(float i  = -1f; i <= 1f; i += 0.5f){
            if(i < 0){
                pistoltop.transform.position = Vector3.MoveTowards(pistoltop.transform.position, cockBack.transform.position, speed * Time.deltaTime);
                gunshot.intensity = bright;
            }else{
                pistoltop.transform.position = Vector3.MoveTowards(pistoltop.transform.position, pistolbody.transform.position + offset, speed * Time.deltaTime);
                gunshot.intensity = 0;
            }
            yield return new WaitForSeconds(0.005f);
        }
    }
}
