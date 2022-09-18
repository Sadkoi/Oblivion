using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_behaviour : MonoBehaviour
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
    public Transform centermass;

    public float maxSteerAngle = 50f;
    public float motorForce = 10f;
    public float AntiRoll = 200f;
    public float topSpeed = 8f;

    public GameObject steeringWheel;

    private int steeringRotation = 0;

    void Start(){
        motorForce = 200f;
        AntiRoll = 2000f;
        //Gwagon.centerOfMass = centermass.position;
    }

    public void GetInput(){
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        //print(Gwagon.velocity.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        //updateSteering();
        //print(frontDriverW.steerAngle);
        steeringWheel.transform.localEulerAngles = new Vector3(steeringWheel.transform.localEulerAngles.x, steeringWheel.transform.localEulerAngles.y, -frontDriverW.steerAngle * 4);
    }

    private void Steer(){
        steeringAngle = maxSteerAngle * x;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;

    }

    private void Accelerate(){
        //left

        float torque;
        
        if(y > 0){
            torque = y * motorForce * (30 - Gwagon.velocity.magnitude);
            torque = Mathf.Clamp(torque, 0f, 50000f);
        }else{
            torque = y * motorForce * (30 - Gwagon.velocity.magnitude) * 0.5f;
            torque = Mathf.Clamp(torque, -50000f, 0f);
        }
        
        frontDriverW.motorTorque = torque;
        rearDriverW.motorTorque = torque;
        frontPassengerW.motorTorque = torque;
        rearPassengerW.motorTorque = torque;
        print(torque);
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

    void downforce(){
        Gwagon.AddForce(-transform.up * Gwagon.velocity.magnitude * 100f);
    }

    private void FixedUpdate(){
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        AntiRollBar(frontDriverW, frontPassengerW);
        AntiRollBar(rearDriverW, rearPassengerW);
        AntiRollBar(frontDriverW, rearPassengerW);
        AntiRollBar(rearDriverW, frontPassengerW);
        downforce();
        Lights();
        //print(Gwagon.velocity.magnitude);
    }



}
