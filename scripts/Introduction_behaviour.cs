using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction_behaviour : MonoBehaviour
{
    public GameObject tripod;
    public GameObject door;
    public GameObject player;
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;
    public Light light5;

    public RawImage presents;
    public RawImage production;
    public RawImage oblivion;
    public Text tapTo;

    private float time = 0.0f;
    public float speedRot = 20f;
    public float linearSpeed = 0f;
    public Animator myAnim;
    private bool firstOneDone = true;
    
    //just to mess with ishraq
    private string[] messlol = {"dis mf", "bruh", "boi", "bruh", 
    "tap it again I swear to f**king god","bruh","You testing me rn"};
    public Text messWith;
    public GameObject messWithText;
    public GameObject canvasGameObject;
    public Font codystar;

    // Start is called before the first frame update
    void Start(){
    //set condition to check savefile if user has completed introduction
    tripod.transform.position = new Vector3(10.3f, 3.3f, -176.2f);
    tripod.transform.eulerAngles = new Vector3(0f,0f,0f);
    myAnim = player.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        time += Time.deltaTime;
        if(time < 16f){
            transform.Rotate(0f,Time.deltaTime * speedRot,0f,Space.World);        
            tripod.transform.Translate(Vector3.forward * Time.deltaTime * linearSpeed);
            updateLinearSpeed();
            updateSpeedRot();
        }
        if(time > 45){
            messWithIshraq();
        }
        
        checkSplashScreen();
        checkSplashScreen2();
        checkSplashScreentransparency();
        beginInteriorView();
        updateLights();
        //print(Input.touches);
        //print(time);
        //print(tripod.transform.eulerAngles + " " + tripod.transform.position);
        //print("f(" + time + ") = " + linearSpeed);
        //(0.0, 2.7, 0.0) (-76.8, 404.7, 136.5)
    }

    void messWithIshraq(){
        if (Input.anyKey){
            if(firstOneDone){
                SceneManager.LoadScene("la_Raza_interior");
                firstOneDone = false;
            }
        }

    }
/*
    void randomlyGenerateText(){
        GameObject text = Instantiate(messWithText, canvasGameObject.transform);
        Text writing = text.GetComponent<Text>();
        writing.text = messlol[Random.Range(0,6)];
        int x = Random.Range((writing.text.Length/2 * 100), (Screen.width - writing.text.Length * 100));
        if(writing.text.Length > 10){
            x = Screen.width/2;
        }
        int y = Random.Range(0,Screen.height - 300);
        writing.transform.position = new Vector3(x,y,0);
        //Text text = canvasGameObject.AddComponent<Text>();
        //text.text = "textString";
        //min: 200, and max: 2760 Screen.width - 200;
        //text.font = codystar;
    }
*/
    void updateLinearSpeed(){
        if(time > 14){
            linearSpeed = 0f;
        }else{
            if(time < 14){
                linearSpeed = 50 - (7f*time);
            }
        }
    }

    void updateSpeedRot(){
        if(time > 12){
            speedRot = 0f;
        }else{
            speedRot = 45f;
        }
    }

    void checkSplashScreen(){
        if(time > 12 && time < 14){
            StartCoroutine(FadeImage(false, presents));
        }else{
            if(time > 15 && time < 16){
                StartCoroutine(FadeImage(true, presents));
            }
        }
    }

    void checkSplashScreen2(){
        if(time > 33 && time < 36){
            StartCoroutine(FadeImage(false, production));
        }else{
            if(time > 37 && time < 38){
                StartCoroutine(FadeImage(true, production));
            }
        }
    }

    void checkSplashScreentransparency(){
        if(time > 16){
            presents.color = new Color(1, 1, 1, 0);
        }
        if(time > 38){
            production.color = new Color(1, 1, 1, 0);
        }
    }

    void beginInteriorView(){
        if(time > 14 && time < 15){
            tripod.transform.parent = null;
            tripod.transform.position = new Vector3(-59.4f, 404.6f, 41.6f);
            tripod.transform.eulerAngles = new Vector3(0f,0f,0f);
            
        }
        if(time > 15){
            //camera bobbing
            if(time < 35){
                tripod.transform.position = new Vector3(tripod.transform.position.x,(0.65f * Mathf.Sin(1.2f * time) + 404.5f),tripod.transform.position.z); 
            }
            //camera tilt
            if(time < 35){
                tripod.transform.eulerAngles = new Vector3(tripod.transform.eulerAngles.x, tripod.transform.eulerAngles.y, (5 * Mathf.Sin(1.4f * time) + 360f));
            }else{
                tripod.transform.eulerAngles = new Vector3(tripod.transform.eulerAngles.x, tripod.transform.eulerAngles.y, 0f);
            }
            // 5 and -5 (355)
            // 355 and 365
            //camera path
            if(time < 35){
                tripod.transform.Translate(Vector3.forward * Time.deltaTime * 3f);
            }
        }
        //teleport to infront of the door
        if(time > 36 && time < 37){
            tripod.transform.position = new Vector3(-76.8f, 404.7f, 136.5f);
        }
        if(time > 37){
            tripod.transform.Translate(Vector3.forward * Time.deltaTime * 0.7f);
        }
        //open door x = -20 to -25
        if(time > 38 && time < 41){
            door.transform.Translate(Vector3.left * Time.deltaTime * 2f);
        }else{
            if(time > 41){
                myAnim.SetBool("isSurprised",true);
            }
        }
        /*
            Critical points: 98.3 (z) 127.7 (z) 135(z)
        */
        if(time > 42){
            StartCoroutine(FadeImage(false, oblivion));
        }
        if(time > 44){
            oblivion.color = new Color(1,1,1,1);
        }
        if(time > 45){
            StartCoroutine(FadeTextIn(tapTo));
        }
    }

    void updateLights(){
        light1.intensity = 50 * Mathf.Sin(2f * time) + 50f;
        light2.intensity = 50 * Mathf.Sin(2f * time) + 50f;
        light3.intensity = 50 * Mathf.Sin(2f * time) + 50f;
        light4.intensity = 50 * Mathf.Sin(2f * time) + 50f;
        light5.intensity = 50 * Mathf.Sin(2f * time) + 50f;
    }

    IEnumerator FadeTextIn(Text txt){
        for (float i = 1; i >= 0; i += (Time.deltaTime * 1f))
            {
                // set color with i as alpha
                txt.color = new Color(1, 1, 1, i);
                yield return null;
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
}
