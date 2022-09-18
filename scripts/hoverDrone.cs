using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverDrone : MonoBehaviour
{
    public List<GameObject> springs;
    public Rigidbody rb;
    public float thrust = 500f;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isActive){
        control_normal();
        }
    }

    void control_normal(){
        rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * 4000f, rb.transform.position);
        rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * Input.GetAxis("Horizontal") * 1000f);
        foreach(GameObject spring in springs){
            RaycastHit hit;
            Debug.DrawRay(spring.transform.position, transform.TransformDirection(Vector3.down), Color.red); 
            if(Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit , 3f)){
                rb.AddForceAtPosition((Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(3f - hit.distance, 2) * thrust), spring.transform.position);
            }

            // anti - rollover feature
            Debug.DrawRay(spring.transform.position, transform.TransformDirection(Vector3.up), Color.green);
            if(Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.up), 0.2f)){
                rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.forward) * 600f);
            }
        }
        //rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.forward) * 20f);
    }
}
