using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basketBallScene : MonoBehaviour
{
    public Transform character;
    public Transform mainCamera;
    public Transform basketball;
    public Transform playerHands;
    public Rigidbody rb;
    public Rigidbody ballrb;
    public Vector2 firstPoint;

    public Material[] segments = new Material[7];
    public Material[] segments2 = new Material[7];
    public int score;

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
    private bool isHoldingBall;
    private bool readyToRecieveInput;
    private float throwForce;
    // Start is called before the first frame update
    
    void Start()
    {
        sensitivity = 6f;
        thrust = 200f;
        isHoldingBall = false;
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
        instructionsHandler();
        basketBallHandler();
        SevenSegmentDisplayHandler();
        Display(segments, new Color(25,163,191), score % 10);
        Display(segments2, new Color(25,163,191), score / 10);
    }

    void GetInput(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 0.2f;
        }else{
            speed = 0.14f;
        }

        RaycastHit hit;
        
        if((Input.GetKeyDown(KeyCode.Space)) && (Physics.Raycast(character.position, character.TransformDirection(Vector3.down),out hit,2f))){
            rb.AddForce(character.transform.TransformDirection(Vector3.up) * thrust);
        }
    }

    void SevenSegmentDisplayHandler(){

    }

    private void Display(Material[] segments, Color Emission, int numberBlueDisplay){
        if(numberBlueDisplay == 1){
            segments[0].SetColor("_EmissionColor", Color.black);
            segments[1].SetColor("_EmissionColor", Color.black);
            segments[2].SetColor("_EmissionColor", Emission);
            segments[3].SetColor("_EmissionColor", Color.black);
            segments[4].SetColor("_EmissionColor", Color.black);
            segments[5].SetColor("_EmissionColor", Emission);
            segments[6].SetColor("_EmissionColor", Color.black);
        }else{
            if(numberBlueDisplay == 2){
                segments[0].SetColor("_EmissionColor", Emission);
                segments[1].SetColor("_EmissionColor", Color.black);
                segments[2].SetColor("_EmissionColor", Emission);
                segments[3].SetColor("_EmissionColor", Emission);
                segments[4].SetColor("_EmissionColor", Emission);
                segments[5].SetColor("_EmissionColor", Color.black);
                segments[6].SetColor("_EmissionColor", Emission);
            }else{
                if(numberBlueDisplay == 3){
                    segments[0].SetColor("_EmissionColor", Emission);
                    segments[1].SetColor("_EmissionColor", Color.black);
                    segments[2].SetColor("_EmissionColor", Emission);
                    segments[3].SetColor("_EmissionColor", Emission);
                    segments[4].SetColor("_EmissionColor", Color.black);
                    segments[5].SetColor("_EmissionColor", Emission);
                    segments[6].SetColor("_EmissionColor", Emission);
                }else{
                    if(numberBlueDisplay == 4){
                        segments[0].SetColor("_EmissionColor", Color.black);
                        segments[1].SetColor("_EmissionColor", Emission);
                        segments[2].SetColor("_EmissionColor", Emission);
                        segments[3].SetColor("_EmissionColor", Emission);
                        segments[4].SetColor("_EmissionColor", Color.black);
                        segments[5].SetColor("_EmissionColor", Emission);
                        segments[6].SetColor("_EmissionColor", Color.black);
                    }else{
                        if(numberBlueDisplay == 5){
                            segments[0].SetColor("_EmissionColor", Emission);
                            segments[1].SetColor("_EmissionColor", Emission);
                            segments[2].SetColor("_EmissionColor", Color.black);
                            segments[3].SetColor("_EmissionColor", Emission);
                            segments[4].SetColor("_EmissionColor", Color.black);
                            segments[5].SetColor("_EmissionColor", Emission);
                            segments[6].SetColor("_EmissionColor", Emission);
                        }else{
                            if(numberBlueDisplay == 6){
                                segments[0].SetColor("_EmissionColor", Emission);
                                segments[1].SetColor("_EmissionColor", Emission);
                                segments[2].SetColor("_EmissionColor", Color.black);
                                segments[3].SetColor("_EmissionColor", Emission);
                                segments[4].SetColor("_EmissionColor", Emission);
                                segments[5].SetColor("_EmissionColor", Emission);
                                segments[6].SetColor("_EmissionColor", Emission);
                            }else{
                                if(numberBlueDisplay == 7){
                                    segments[0].SetColor("_EmissionColor", Emission);
                                    segments[1].SetColor("_EmissionColor", Color.black);
                                    segments[2].SetColor("_EmissionColor", Emission);
                                    segments[3].SetColor("_EmissionColor", Color.black);
                                    segments[4].SetColor("_EmissionColor", Color.black);
                                    segments[5].SetColor("_EmissionColor", Emission);
                                    segments[6].SetColor("_EmissionColor", Color.black);
                                }else{
                                    if(numberBlueDisplay == 8){
                                        segments[0].SetColor("_EmissionColor", Emission);
                                        segments[1].SetColor("_EmissionColor", Emission);
                                        segments[2].SetColor("_EmissionColor", Emission);
                                        segments[3].SetColor("_EmissionColor", Emission);
                                        segments[4].SetColor("_EmissionColor", Emission);
                                        segments[5].SetColor("_EmissionColor", Emission);
                                        segments[6].SetColor("_EmissionColor", Emission);
                                    }else{
                                        if(numberBlueDisplay == 9){
                                            segments[0].SetColor("_EmissionColor", Emission);
                                            segments[1].SetColor("_EmissionColor", Emission);
                                            segments[2].SetColor("_EmissionColor", Emission);
                                            segments[3].SetColor("_EmissionColor", Emission);
                                            segments[4].SetColor("_EmissionColor", Color.black);
                                            segments[5].SetColor("_EmissionColor", Emission);
                                            segments[6].SetColor("_EmissionColor", Emission);
                                        }else{
                                            if(numberBlueDisplay == 0){
                                            segments[0].SetColor("_EmissionColor", Emission);
                                            segments[1].SetColor("_EmissionColor", Emission);
                                            segments[2].SetColor("_EmissionColor", Emission);
                                            segments[3].SetColor("_EmissionColor", Color.black);
                                            segments[4].SetColor("_EmissionColor", Emission);
                                            segments[5].SetColor("_EmissionColor", Emission);
                                            segments[6].SetColor("_EmissionColor", Emission);
                                            }else{
                                                
                                            }    
                                        }     
                                    }                                  
                                }                              
                            }  
                        }  
                    }                    
                }
            }
        }
    }

    void movePlayer(){
        Vector3 vectorMove = new Vector3(x, 0f, y);
        character.Translate(vectorMove.normalized * speed);
    }

    void mouseRotation(){
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -90f, 90f);
        character.eulerAngles = new Vector3(0f, rotX, 0f);
        mainCamera.eulerAngles = new Vector3(-rotY,character.eulerAngles.y,0f);
    }

    void instructionsHandler(){
        RaycastHit hit;

        if(Physics.Raycast(mainCamera.position + mainCamera.TransformDirection(Vector3.forward), mainCamera.TransformDirection(Vector3.forward),out hit, 3f)){
            
            if(hit.transform.gameObject.tag == "basketball" && !isHoldingBall){
                //instructions.text = "Right-Click to pick up the ball";
                //border.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    isHoldingBall = true;
                    ballrb.isKinematic = true;
                }
            }else{
                if(hit.transform.gameObject.tag == "Untagged"){
                    //instructions.text = "";
                    //border.SetActive(false);
                }                
            }
            
        }else{
            //instructions.text = "";
            //border.SetActive(false);
        }
    }

    void basketBallHandler(){
        Debug.DrawRay(basketball.position, mainCamera.TransformDirection(Vector3.forward), Color.green);
        if(isHoldingBall){
            basketball.position = playerHands.position;
            basketball.rotation = playerHands.rotation;
            StartCoroutine(toggleInputReciever(true));
            if(Input.GetKey(KeyCode.Mouse0) && readyToRecieveInput){
                StartCoroutine(toggleInputReciever(false));
                isHoldingBall = false;
                ballrb.isKinematic = false;
                ballrb.AddForce(basketball.TransformDirection(Vector3.forward) * throwForce * Mathf.Clamp((time - startTime),0f,1f), ForceMode.Force);
            }
            if(Input.GetKeyDown(KeyCode.Mouse1)){
                startTime = time;
            }
        }

    }

    IEnumerator toggleInputReciever(bool tf){
        yield return new WaitForSeconds(0.5f);
        readyToRecieveInput = tf;
    }

}
