using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyraController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject myradov;
    public IntroIII_theFight IntroIII_theFight;
    public GameObject canvas;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        controller = myradov.gameObject.GetComponent<CharacterController>();
        IntroIII_theFight = canvas.GetComponent<IntroIII_theFight>();
        speed = 150f;
    }

    // Update is called once per frame
    void Update()
    {
        if(IntroIII_theFight.playerAssumedControl){
            movePlayer();
        }
        //print(myradov.transform.eulerAngles);
    }

    void movePlayer(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if(direction.magnitude >= 0.1f){
            controller.Move(myradov.transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
            controller.Move(myradov.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }
    }
}
