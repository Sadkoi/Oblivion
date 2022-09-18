using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntroIItheNewBeginning : MonoBehaviour
{
    public RawImage oblivion;
    public GameObject myradov;
    public Rigidbody myraRig;
    public GameObject mcamera;
    
    public Text instruct;
    public Text dialogue;
    public GameObject enemyTroop;
    public float time;
    public GameObject marker;
    public GameObject hand;
    public GameObject enemyhead;
    public RawImage blackout;
    public GameObject blackoutGameO;

    private bool theEnd = true;
    private bool hasStarted = true;
    private bool playerAssumedControl = false;
    private bool camerahashappened = true;
    private Animator myAnim;
    private Animator AnimEnemy1;
    private bool aboutToHitOfficer = false;
    private bool hitButtonDown = false;
    private float ypos;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        myAnim = myradov.gameObject.GetComponent<Animator>();
        AnimEnemy1 = enemyTroop.gameObject.GetComponent<Animator>();
        blackoutGameO.SetActive(false);
        ypos = myradov.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        schedule();
        checkPlayerControl();
        dialogueEngine();
        pressTargetFirstTime();
        myradov.transform.position = new Vector3(myradov.transform.position.x, ypos, myradov.transform.position.z);
    }

    void schedule(){
        if(time < 1 && hasStarted){
            StartCoroutine(FadeImage(true, oblivion));
            hasStarted = false;
        }else{
            if(time > 0.8 && time < 5){
                oblivion.color = new Color(1,1,1,0);
                myAnim.SetBool("reborn",true);
            }
        }
        //begin switch from reborn to sprint
        if(time > 10 && time < 12){
            //(-217.2, 286.5, 1442.5)
            //(-197.9, 286.5, 1400.5) - original
            Vector3 newAngle = new Vector3(1, 0, 0);
            Vector3 newpos = new Vector3(-217.2f, 286.5f, 1442.5f);
            myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newpos,Time.deltaTime * 25);
            myradov.transform.eulerAngles = new Vector3(0, 360 + -45*(time - 10), 0);
        }
        //check if in sprint state, then initiate player control

        if(time > 12){
            if(!aboutToHitOfficer){
                if(!playerAssumedControl){
                    myAnim.speed = 0.05f;
                    AnimEnemy1.speed = 0.05f;
                    myradov.transform.Translate(Vector3.forward * Time.deltaTime * 5);
                    instruct.text = "PRESS W TO SPRINT";
                    //myraRig.MovePosition(Vector3.forward * Time.deltaTime * 5);
                    //myraRig.velocity = new Vector3(0f,0f,5f);
                }else{
                    myAnim.speed = Input.GetAxis("Vertical");
                    myradov.transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * 100);
                    AnimEnemy1.speed = 0.9f;
                    instruct.text = "";
                    //myraRig.MovePosition(Vector3.forward * Time.deltaTime * 100);
                }
            }else{
                if(!hitButtonDown){
                myAnim.speed = 0.05f;
                AnimEnemy1.speed = 0.05f;
                }else{
                    if(((hand.transform.position - enemyhead.transform.position).magnitude) < 7){
                        AnimEnemy1.SetBool("punched",true);
                    }
                    mcamera.transform.LookAt(enemyhead.transform);
                }
                //prompt new animation
            }
        }

        //check first turn
        if(myradov.transform.position.x <= -700 && myradov.transform.position.z <= 2000){
            if(myradov.transform.eulerAngles.y >= 270){
                myradov.transform.Rotate(0f, 50 * (Input.GetAxis("Vertical") + 0.1f)  * Time.deltaTime, 0f);
                Vector3 newpos = new Vector3(-765, myradov.transform.position.y, myradov.transform.position.z);
                myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newpos,Time.deltaTime * 25);
            }
        }
        //check second and third turns
        if(myradov.transform.position.z >= 2096){
            if(myradov.transform.eulerAngles.y <= 90 && myradov.transform.position.x < -110){
                myradov.transform.Rotate(0f, 50 * (Input.GetAxis("Vertical") + 0.1f) * Time.deltaTime, 0f);
                Vector3 newpos = new Vector3(myradov.transform.position.x, myradov.transform.position.y, 2150);
                myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newpos,Time.deltaTime * 25);
            }

            if(myradov.transform.position.x >= -110){
                if(myradov.transform.eulerAngles.y <= 100){
                    myradov.transform.Rotate(0f, -50 * (Input.GetAxis("Vertical") + 0.1f) * Time.deltaTime, 0f);
                    Vector3 newpos = new Vector3(-25, myradov.transform.position.y, myradov.transform.position.z);
                    myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newpos,Time.deltaTime * 25);
                }
            }
        }

        //if entered captian's room
        if(myradov.transform.position.z > 2300){
            Vector3 newPos = new Vector3(-77.5f,285.2f,2512.2f);
            if(!playerAssumedControl){
                myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newPos,Time.deltaTime * 15);
            }else{
                myradov.transform.position = Vector3.MoveTowards(myradov.transform.position,newPos,Time.deltaTime * 30);
            }
            myradov.transform.LookAt(marker.transform);
            if(camerahashappened){
                mcamera.transform.localEulerAngles = new Vector3(mcamera.transform.localEulerAngles.x - 20f, mcamera.transform.localEulerAngles.y, mcamera.transform.localEulerAngles.z);
                camerahashappened = false;
            }
            if((newPos - myradov.transform.position).magnitude < 20.0f && !hitButtonDown){
                aboutToHitOfficer = true;
                instruct.text = "LEFT CLICK TO PUNCH";
                myAnim.SetBool("punch",true);
            }
        }

        if(enemyhead.transform.position.y < 295 && theEnd){
            blackoutGameO.SetActive(true);
            StartCoroutine(FadeImage2(blackout));
            theEnd = false;
        }
        //IF IMAGE COLOR IS 0,0,0,1 THEN OPEN NEXT SCENE
        if(blackout.color.a > 0.95f){
            SceneManager.LoadScene("la_Raza_Intro_III");
        }

    }

    void pressTargetFirstTime(){
        if(aboutToHitOfficer && Input.GetKeyDown(KeyCode.Mouse0)){
            hitButtonDown = true;
            AnimEnemy1.speed = 1f;
            myAnim.speed = 1f;
            instruct.text = "";
        }
    }

    void dialogueEngine(){
        if(time > 3 && time < 9){
            dialogue.text = "fuck.";
        }
        if(time > 9){
            dialogue.text = "";
        }
    }

    void checkPlayerControl(){
        if(Input.GetAxis("Vertical") <= 0){
            playerAssumedControl = false;
        }else{
            playerAssumedControl = true;
        }

    }

    IEnumerator FadeImage(bool fadeAway, RawImage img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime * 1f))
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    IEnumerator FadeImage2(RawImage img)
    {
        // fade from transparent to opaque
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
