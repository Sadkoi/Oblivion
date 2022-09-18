using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spaceStationScript : MonoBehaviour
{
    public Transform character;
    public Transform mainCamera;
    public Transform elevator;
    public Transform elevatorShaft;

    public Transform elevatorDoor1, elevatorDoor2;
    public Transform elevatorDoor3, elevatorDoor4;
    public Rigidbody rb;
    public Vector2 firstPoint;
    public Text instructions;

    public Animator ShahAnim;
    public AudioSource ShahSaysIt;

    //public Transform[] fansList = new Transform[7];

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

    private float elevatorDoor1Euler;
    private float elevatorDoor2Euler;
    private float elevatorDoor3Euler;
    private float elevatorDoor4Euler;

    public int targetFloor;
    private float[] FloorToWorldPos = {1.06f,4.12f,7.16f,10.1f,13.6f,17.1f};

    private bool animationPlayed;
    // Start is called before the first frame update
    
    void Start()
    {
        sensitivity = 4f;
        thrust = 300f;
        readyToRecieveInput = false;
        throwForce = 750f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        elevatorDoor1Euler = elevatorDoor1.eulerAngles.y;
        elevatorDoor2Euler = elevatorDoor2.eulerAngles.y;
        elevatorDoor3Euler = elevatorDoor3.eulerAngles.y;
        elevatorDoor4Euler = elevatorDoor4.eulerAngles.y;

        character.transform.eulerAngles = new Vector3(0f,90f,0f);
        mainCamera.eulerAngles = new Vector3(0f,90f,0f);

        ShahAnim.speed = 0.9f;

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
        determineTargetFloor();
        print(elevatorDoor4.eulerAngles.y);
        //print((elevator.position - character.position).magnitude);
        instructionsHandler();
    }
    /*
    1 - 1.06
    2 - 4.02
    3 - 7.06
    4 - 9.88
    5 - 13.4
    6 - 16.93
    */
    void determineTargetFloor(){
        if((elevator.position - character.position).magnitude > 2){
            if(character.position.y < FloorToWorldPos[1]){
                targetFloor = 0;
            }else{
                if(character.position.y < FloorToWorldPos[2]){
                    targetFloor = 1;
                }else{
                    if(character.position.y < FloorToWorldPos[3]){
                        targetFloor = 2;
                    }else{
                        if(character.position.y < FloorToWorldPos[4]){
                            targetFloor = 3;
                        }else{
                            if(character.position.y < FloorToWorldPos[5]){
                                targetFloor = 4;
                            }else{
                                targetFloor = 5;
                            }
                        }
                    }
                }
            }
        }
    }


    void environmenthandler(){
        float elevatorDoorSpeed = 40f;
        float elevatorSpeed = 1f;
        //for(int i = 0; i < fansList.Length; i++){
        //    fansList[i].Rotate(0f,0f, 200 * Time.deltaTime);
        //}
        //330 - 360 - 60
        // Move the thing
        print(Mathf.Abs(elevatorShaft.position.y - FloorToWorldPos[targetFloor]));
        if(Mathf.Abs(elevatorShaft.position.y - FloorToWorldPos[targetFloor]) > 0.01f){ // if the target floor is not the current floor -- during transit
            elevatorShaft.position = Vector3.MoveTowards(elevatorShaft.position, new Vector3(elevatorShaft.position.x, FloorToWorldPos[targetFloor],elevatorShaft.position.z), elevatorSpeed * Time.deltaTime);
            if(elevatorDoor1.eulerAngles.y > elevatorDoor1Euler){
                elevatorDoor1.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
            }
            if(elevatorDoor2.eulerAngles.y < elevatorDoor2Euler){
                elevatorDoor2.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
            }
        }else{
            if(targetFloor == 0){
                // Elevator door animation 
                if((elevator.position - character.position).magnitude < 1){ //inside the elevator
                    if(elevatorDoor1.eulerAngles.y > elevatorDoor1Euler){
                        elevatorDoor1.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                    }
                    if(elevatorDoor2.eulerAngles.y < elevatorDoor2Euler){
                        elevatorDoor2.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                    }

                }else{
                    if((elevator.position - character.position).magnitude < 6){ //close to the elevator
                        if(elevatorDoor1.eulerAngles.y < elevatorDoor1Euler + 100f){
                            elevatorDoor1.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                        if(elevatorDoor2.eulerAngles.y > elevatorDoor2Euler - 100f){
                            elevatorDoor2.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                    }else{
                        if(elevatorDoor1.eulerAngles.y > elevatorDoor1Euler){ // away from the elevator - close
                            elevatorDoor1.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                        if(elevatorDoor2.eulerAngles.y < elevatorDoor2Euler){
                            elevatorDoor2.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                    }
                }
            }else{
                if((elevator.position - character.position).magnitude < 1){ //inside the elevator
                    if(elevatorDoor3.eulerAngles.y > elevatorDoor3Euler){
                        elevatorDoor3.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                    }
                    if(elevatorDoor4.eulerAngles.y < elevatorDoor4Euler){
                        elevatorDoor4.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                    }

                }else{
                    if((elevator.position - character.position).magnitude < 6){ //close to the elevator
                        if(elevatorDoor3.eulerAngles.y < elevatorDoor3Euler + 100f){
                            elevatorDoor3.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                        if(elevatorDoor4.eulerAngles.y > elevatorDoor4Euler - 100f){
                            elevatorDoor4.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                    }else{
                        if(elevatorDoor3.eulerAngles.y > elevatorDoor3Euler){ // away from the elevator - close
                            elevatorDoor3.Rotate(0f, -elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                        if(elevatorDoor4.eulerAngles.y < elevatorDoor4Euler){
                            elevatorDoor4.Rotate(0f, elevatorDoorSpeed * Time.deltaTime, 0f);
                        }
                    }
                }
            }
        }

    }
    

    void GetInput(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if((elevator.position - character.position).magnitude < 2){
            speed = 0.03f;
        }else{
            if(Input.GetKey(KeyCode.LeftShift)){
                speed = 0.15f;
            }else{
                speed = 0.07f;
            }
        }

        RaycastHit hit;
        
        if((Input.GetKeyDown(KeyCode.Space)) && (Physics.Raycast(character.position, character.TransformDirection(Vector3.down),out hit,2f))){
            rb.AddForce(character.transform.TransformDirection(Vector3.up) * thrust);
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
            //print(hit.transform.gameObject.tag);
            if(hit.transform.gameObject.tag == "Untagged"){
                instructions.text = "";
                //border.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                }
            }else{
                if(hit.transform.gameObject.tag == "floor1"){
                    instructions.text = "Travel to ground floor";
                    if(Input.GetKeyDown(KeyCode.Mouse0)){
                        targetFloor = 0;
                    }
                }else{
                    if(hit.transform.gameObject.tag == "floor2"){
                        instructions.text = "Travel to second floor";
                        if(Input.GetKeyDown(KeyCode.Mouse0)){
                            targetFloor = 1;
                        }
                    }else{
                        if(hit.transform.gameObject.tag == "floor3"){
                            instructions.text = "Travel to third floor";
                            if(Input.GetKeyDown(KeyCode.Mouse0)){
                                targetFloor = 2;
                            }
                        }else{
                            if(hit.transform.gameObject.tag == "floor4"){
                                instructions.text = "Travel to fourth floor";
                                if(Input.GetKeyDown(KeyCode.Mouse0)){
                                    targetFloor = 3;
                                }
                            }else{
                                if(hit.transform.gameObject.tag == "floor5"){
                                    instructions.text = "Travel to fifth floor";
                                    if(Input.GetKeyDown(KeyCode.Mouse0)){
                                        targetFloor = 4;
                                    }
                                }else{
                                    if(hit.transform.gameObject.tag == "floor6"){
                                        instructions.text = "Travel to sixth floor";
                                        if(Input.GetKeyDown(KeyCode.Mouse0)){
                                            targetFloor = 5;
                                        }
                                    }else{
                                        if(hit.transform.gameObject.tag == "basketball"){
                                            instructions.text = "Speak to Shah Wafi";
                                            if(Input.GetKeyDown(KeyCode.Mouse0)){
                                                //trigger animation trigger sound
                                                ShahAnim.SetBool("handrill",true);
                                                ShahSaysIt.Play();
                                                StartCoroutine(toggleShahAnim(false));
                                            }
                                        }else{
                                            if(hit.transform.gameObject.tag == "Respawn"){
                                                instructions.text = "Press E to exit the space station and return to the ship";
                                                if(Input.GetKeyDown(KeyCode.E)){
                                                    SceneManager.LoadScene("Home_Base");
                                                }
                                            }else{
                                                instructions.text = "";
                                            }
                                        }
                                    }
                                }
                            }
                        }
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

    IEnumerator toggleShahAnim(bool tf){
        yield return new WaitForSeconds(6f);
        ShahAnim.SetBool("handrill",tf);
        ShahSaysIt.Stop();
    }

}
