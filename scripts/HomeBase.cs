using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeBase : MonoBehaviour
{
    public Transform character;
    public Transform mainCamera;
    public Transform playerHands;
    public Rigidbody rb;
    public Vector2 firstPoint;
    public Text instructions;

    public Transform[] fansList = new Transform[7];

    //public Text instructions;
    public GameObject border;

    public float time;
    public float startTime;

    private float x;
    private float y;
    private float sensitivity;
    private float speed;
    private float thrust;
    private float rotX;
    private float rotY;
    private bool readyToRecieveInput;
    private float throwForce;
    // Start is called before the first frame update
    
    void Start()
    {
        sensitivity = 6f;
        thrust = 1000f;
        readyToRecieveInput = false;
        throwForce = 750f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        character.transform.eulerAngles = new Vector3(0f,0f,0f);
        mainCamera.eulerAngles = new Vector3(0f,0f,0f);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1)){
            time += Time.deltaTime;
        }
        GetInput();
        movePlayer();
        mouseRotation();
        environmenthandler();
        instructionsHandler();
        checkButtonPress();
    }

    void environmenthandler(){
        for(int i = 0; i < fansList.Length; i++){
            fansList[i].Rotate(0f,0f, 200 * Time.deltaTime);
        }
    }

    void GetInput(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 2f;
        }else{
            speed = 1.5f;
        }

        RaycastHit hit;
        
        if((Input.GetKeyDown(KeyCode.Space)) && (Physics.Raycast(character.position, character.TransformDirection(Vector3.down),out hit,2f))){
            rb.AddForce(character.transform.TransformDirection(Vector3.up) * thrust);
        }
    }

    void movePlayer(){
        Vector3 vectorMove = new Vector3(x, 0f, y);
        character.Translate(vectorMove.normalized * speed);
        rb.AddForce(Vector3.down * 200f);
    }

    void mouseRotation(){
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -90f, 90f);
        character.eulerAngles = new Vector3(0f, rotX, 0f);
        mainCamera.eulerAngles = new Vector3(-rotY,character.eulerAngles.y,0f);
    }

    void checkButtonPress(){
        if(instructions.text == "Press E to exit the ship and enter a space station" && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene("spaceStation");
        }
    }

    void instructionsHandler(){
        RaycastHit hit;

        if(Physics.Raycast(mainCamera.position + mainCamera.TransformDirection(Vector3.forward), mainCamera.TransformDirection(Vector3.forward),out hit, 80f)){
            if(hit.transform.gameObject.tag == "Untagged"){
                instructions.text = "";
                //border.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                }
            }else{
                if(hit.transform.gameObject.tag == "Airlock"){
                    instructions.text = "Press E to exit the ship and enter a space station";
                    //border.SetActive(false);
                }else{
                    if(hit.transform.gameObject.tag == "Console"){
                        instructions.text = "Use the Captain's Console";
                    }
                }                
            }
            
        }else{
            instructions.text = "";
            //border.SetActive(false);
        }
    }

    IEnumerator toggleInputReciever(bool tf){
        yield return new WaitForSeconds(0.5f);
        readyToRecieveInput = tf;
    }

}
