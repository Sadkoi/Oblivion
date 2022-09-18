using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortBehaviour : MonoBehaviour
{
    private float x;
    private float y;
    private float steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public Rigidbody Gwagon;
    public Material brakeLights;

    public GameObject FDRay, FPRay;
    public GameObject RDRay, RPRay;

    public float maxSteerAngle = 50f;
    public float motorForce = 500f;
    public float AntiRoll = 50000f;
    public float topSpeed = 8f;

    public void GetInput(){
        if(Input.GetAxis("Horizontal") != 0){
            x = Input.GetAxis("Horizontal");
        }
        if(Input.GetAxis("Vertical") != 0){
            y = Input.GetAxis("Vertical");
        }
        
    }

    private void CalculateSteering(){

        FDRay.transform.eulerAngles = new Vector3(0f, FDRay.transform.eulerAngles.y, 0f);
        FPRay.transform.eulerAngles = new Vector3(0f, FPRay.transform.eulerAngles.y, 0f);
        RDRay.transform.eulerAngles = new Vector3(0f, RDRay.transform.eulerAngles.y, 0f);
        RPRay.transform.eulerAngles = new Vector3(0f, RPRay.transform.eulerAngles.y, 0f);


        Debug.DrawRay(FDRay.transform.position, FDRay.transform.forward, Color.green);
        Debug.DrawRay(FDRay.transform.position, -FDRay.transform.right, Color.green);

        Debug.DrawRay(FPRay.transform.position, FPRay.transform.forward, Color.green);
        Debug.DrawRay(FPRay.transform.position, FPRay.transform.right, Color.green);

        Debug.DrawRay(RDRay.transform.position, -RDRay.transform.forward, Color.green);
        Debug.DrawRay(RDRay.transform.position, -RDRay.transform.right, Color.green);

        Debug.DrawRay(RPRay.transform.position, -RPRay.transform.forward, Color.green);
        Debug.DrawRay(RPRay.transform.position, RPRay.transform.right, Color.green);

        RaycastHit hit;
        float[] lateralhits = new float[4];
        if(Physics.Raycast(FDRay.transform.position, -FDRay.transform.right, out hit, 20f)){
            lateralhits[0] = hit.distance;
        }
        if(Physics.Raycast(RDRay.transform.position, -FDRay.transform.right, out hit, 20f)){
            lateralhits[1] = hit.distance;
        }
        if(Physics.Raycast(FPRay.transform.position, FDRay.transform.right, out hit, 20f)){
            lateralhits[2] = hit.distance;
        }
        if(Physics.Raycast(RPRay.transform.position, FDRay.transform.right, out hit, 20f)){
            lateralhits[3] = hit.distance;
        }
        // [Front-Left, Rear-Left, Front-Right, Rear-Right]
        //print("[ " + lateralhits[0] + "," + lateralhits[1] + "," + lateralhits[2] + "," + lateralhits[3] + " ]");


        
    }


    private void Steer(){
        steeringAngle = maxSteerAngle * x;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;

    }

    private void Accelerate(){
        //left

        float torque;
        if(y < 0){
            torque = y * motorForce * 2.5f;
        }else{
            torque = y * motorForce;
        }
        frontDriverW.motorTorque = torque;
        rearDriverW.motorTorque = torque;
        frontPassengerW.motorTorque = torque;
        rearPassengerW.motorTorque = torque;
    }

    private void UpdateWheelPoses(){
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void AntiRollBar(WheelCollider Driver, WheelCollider Passenger){
        // front axle
        // front Driver
        WheelHit hit;
        bool groundedL = Driver.GetGroundHit(out hit);
        float travelL;
        if(groundedL){
            travelL = (-Driver.transform.InverseTransformPoint(hit.point).y - Driver.radius) / Driver.suspensionDistance;
        }else{
            travelL = 1.0f;
        }
        // front Passenger
        WheelHit hit2;
        bool groundedR = Passenger.GetGroundHit(out hit2);
        float travelR;
        if(groundedR){
            travelR = (-Passenger.transform.InverseTransformPoint(hit2.point).y - Passenger.radius) / Passenger.suspensionDistance;
        }else{
            travelR = 1.0f;
        }        

        float antiRollForce = (travelL - travelR) * AntiRoll * (12f + Gwagon.velocity.magnitude)/12f;

        if (groundedL){
            Gwagon.AddForceAtPosition(Driver.transform.up * -antiRollForce, Driver.transform.position);  
        }
        if (groundedR){
            Gwagon.AddForceAtPosition(Passenger.transform.up * antiRollForce, Passenger.transform.position);
        }
    }

    private void Lights(){
        if(y < 0){
            brakeLights.SetColor("_EmissionColor", Color.red);
        }else{
            brakeLights.SetColor("_EmissionColor", Color.black);
        }
    }

    private void FixedUpdate(){
        //GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        AntiRollBar(frontDriverW, frontPassengerW);
        AntiRollBar(rearDriverW, rearPassengerW);
        Lights();
        CalculateSteering();
        //print(Gwagon.velocity.magnitude);
    }
}
