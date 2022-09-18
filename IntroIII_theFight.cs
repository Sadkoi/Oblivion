using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroIII_theFight : MonoBehaviour
{
    public GameObject rotator;
    public GameObject myradov;
    private Animator myraAnim;
    public GameObject pistol;
    public GameObject myraHead;
    public GameObject camera;
    public GameObject myraHand;
    public GameObject aimbot;
    public GameObject miniMap;
    public GameObject raycastGround;
    public GameObject enemyTroop1;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;

    public RawImage blackout;
    public GameObject blaque;
    public RawImage forScreen;
    public GameObject ship_logo;
    public GameObject blocker;
    public Text lifealert;
    public Text oxy;
    public Text nextbox;
    public Text nextbox2;
    public Text monke;
    public GameObject screenTapToDismiss;

    private Rigidbody myrarb;
    public GameObject manualAim;
    public Text instructionText;

    private float time;
    private float timePickUpStart;
    public bool playerAssumedControl = false;

    public int numEnemies;

    private bool hasStarted = true;
    private bool hasStarted2 = true;
    private bool hasStarted3 = true;
    private bool hasStarted4 = true;
    private bool hasStarted5 = true;
    private bool hasStarted6 = true;
    private bool hasStarted7 = true;
    private bool hasStarted8 = true;

    //just to mess with Ishraq
    private string[] messlol = {"HERE WE GO AGAIN", "BRUH", "BOI", "B*TCH", 
    "TAP IT AGAIN I SWEAR TO F**KING GOD","YOU TESTING ME RN"};
    public Text messWith;
    public GameObject messWithText;
    public GameObject canvasGameObject;
    public Font codystar;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        myraAnim = myradov.gameObject.GetComponent<Animator>();
        blaque.SetActive(false);
        screenTapToDismiss.SetActive(false);
        myraAnim.speed = 0.5f;
        myrarb = myradov.gameObject.GetComponent<Rigidbody>();
        miniMap.SetActive(false);
        manualAim.SetActive(false);
        myrarb.constraints = RigidbodyConstraints.FreezePositionY;
        instructionText.text = "";
        numEnemies = 9;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        schedule();
        rotateTheRotator();
        cueAnimation();
        //keep player on ground
        if(hasStarted8){
            spawnchecker(200);
            hasStarted8 = false;
        }

        if(numEnemies <= 0){
            StartCoroutine("outro");
        }
        //spawnchecker(25);
        //(-4.0, 2.0, -1.7)
        //print(time);
        //print(myraHead.transform.position - camera.transform.position);
        //(0.4, 0.3, -0.2, 0.8)
        //print(camera.transform.rotation);
        //print((pistol.transform.position - myraHand.transform.position).magnitude);
        //print((pistol.transform.position - myraHand.transform.position) + " " + pistol.transform.rotation);
        //print(timePickUpStart);
    }

    IEnumerator outro(){
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeImage(false, blackout));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("intro_explanation");
    }
/*
    void FixedUpdate(){
        if(playerAssumedControl){
            characterController();
        }
    }
*/
    void schedule(){
        if(time < 1 && hasStarted){
            StartCoroutine(FadeImage2(true, blackout));
            hasStarted = false;
        }else{
            if(time > 0.8 && time < 5){
                blackout.color = new Color(1,1,1,0);
            }
        }
        if(time > 10 && hasStarted2){
            StartCoroutine(FadeImage2(false,blackout));
            hasStarted2 = false;
        }else{
            if(time > 11.5 && time < 12.9){
                blackout.color = new Color(1,1,1,0);
                blaque.SetActive(true);
            }
        }

        if((pistol.transform.position - myraHand.transform.position).magnitude < 5.3 && hasStarted7){
            pistol.transform.parent = myraHand.transform;
            pistol.transform.position = myraHand.transform.position + new Vector3(1.8f, -0.2f, 2.0f);
            //pistol.transform.rotation = new Quaternion(0.4f, 0.3f, 0.0f, 0.9f);
            pistol.transform.rotation = new Quaternion(0.3f, 0.5f, 0.2f, 0.8f);
            pistol.transform.eulerAngles = new Vector3(pistol.transform.eulerAngles.x,pistol.transform.eulerAngles.y,pistol.transform.eulerAngles.z + 4f);
            //pistol.transform.LookAt(indexPointer.transform);
            hasStarted7 = false;
        }

        if(timePickUpStart != 0 && time > timePickUpStart + 6f){
            miniMap.SetActive(true);
            manualAim.SetActive(true);
            myrarb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
            //myraCollider.enabled = true;
            playerAssumedControl = true;
            instructionText.text = "Kill the intruders";
        }

    }


    void rotateTheRotator(){
        rotator.transform.Rotate(0f, 15f * Time.deltaTime, 0f);
    }

    void cueAnimation(){
        if(time > 11.5 && time < 15){
            ship_logo.transform.Translate(0f, 200f * time * Time.deltaTime,0f);
            blocker.transform.Translate(0f, -200f * time * Time.deltaTime,0f);
        }
        if(time > 12 && hasStarted3){
            StartCoroutine(typeOutText("LIFE SUPPORT RESTORED", lifealert,0.1f));
            hasStarted3 = false;
        }
        if(time > 14.2 && hasStarted4){
            StartCoroutine(typeOutText("OXYGEN ................................................................. 0%", oxy,0.03f));
            StartCoroutine(typeOutText("ATMOSPHERE ................................................. 0%", nextbox,0.035f));
            StartCoroutine(typeOutText("HEAT.......................................................................... 0%", nextbox2,0.03f));
            StartCoroutine(typeOutText("MONKE .................................................................... 0%", monke,0.03f));
            hasStarted4 = false;
        }

        if(time > 12 && time < 14){
            //camera.transform.parent = null;
            camera.transform.position = myraHead.transform.position - new Vector3(-4.0f, 2.0f, -1.7f);
            camera.transform.rotation = new Quaternion(0.4f, 0.3f, -0.31f, 0.8f);
            camera.transform.parent = myraHead.transform;
        }

        if(time > 17 && hasStarted5){
            StartCoroutine(increasePercent("OXYGEN ................................................................. ", oxy, 1f, 0.01f));
            StartCoroutine(increasePercent("ATMOSPHERE ................................................. ", nextbox, 1f, 0.05f));
            StartCoroutine(increasePercent("HEAT.......................................................................... ", nextbox2, 2f, 0.03f));
            StartCoroutine(increasePercent("MONKE .................................................................... ", monke, 1f, 0.025f));
            hasStarted5 = false;
        }

        if(time > 22 && hasStarted6){
            screenTapToDismiss.SetActive(true);
            messWithIshraq();
        }
    }

    void messWithIshraq(){
        if (Input.anyKey && hasStarted6){
            StartCoroutine(FadeImage2(true, blackout));
            blaque.SetActive(false);
            screenTapToDismiss.SetActive(false);
            myraAnim.speed = 1f;
            myraAnim.SetBool("pickUp",true);
            timePickUpStart = time;
            manualAim.SetActive(false);

            //must be last call, ensures that if statement is true to complete all above
            hasStarted6 = false; 
        }
    }
    /*
    void randomlyGenerateText(){
        GameObject text = Instantiate(messWithText, canvasGameObject.transform);
        Text writing = text.GetComponent<Text>();
        writing.text = messlol[Random.Range(0,6)];
        int x = Random.Range((writing.text.Length/2 * 100), (Screen.width - writing.text.Length * 100));
        if(writing.text.Length > 30){
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

    void spawnchecker(int deadDist){
            Instantiate(enemyTroop1, spawn1.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn1.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn1.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn2.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn2.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn2.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn3.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn3.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
            Instantiate(enemyTroop1, spawn3.transform.position + new Vector3(Random.Range(-5f,5f), 0f, Random.Range(-5f,5f)), Quaternion.identity);
    }


    IEnumerator increasePercent(string texxt, Text textbox, float multiple, float wait){
        for(int i = 0; i <= (100 / multiple); i += 1){
            textbox.text = texxt + ((int)(multiple * i)).ToString() + "%";
            yield return new WaitForSeconds(wait);
        }
    }


    IEnumerator typeOutText(string texxt, Text textbox, float wait){
        for(int i = 0; i <= texxt.Length; i += 1){
            textbox.text = texxt.Substring(0,i);
            yield return new WaitForSeconds(wait);
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

   IEnumerator FadeImage2(bool fadeAway, RawImage img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime * 1f))
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
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
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
