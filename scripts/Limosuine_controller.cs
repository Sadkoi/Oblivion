using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limosuine_controller : MonoBehaviour
{
    private float x;
    private float y;
    private float steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public Rigidbody limo;
    public Material brakeLights;

    public float maxSteerAngle = 50f;
    public float motorForce = 300f;
    public float AntiRoll = 500f;
    public float topSpeed = 8f;

    public void GetInput(){
        if(Input.GetAxis("Horizontal") != 0){
            x = Input.GetAxis("Horizontal");
        }
        if(Input.GetAxis("Vertical") != 0){
            y = Input.GetAxis("Vertical");
        }
        
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
            torque = y * motorForce * 1f;
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

        float antiRollForce = (travelL - travelR) * AntiRoll * (12f + limo.velocity.magnitude)/12f;

        if (groundedL){
            limo.AddForceAtPosition(Driver.transform.up * -antiRollForce, Driver.transform.position);  
        }
        if (groundedR){
            limo.AddForceAtPosition(Passenger.transform.up * antiRollForce, Passenger.transform.position);
        }
    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        AntiRollBar(frontDriverW, frontPassengerW);
        AntiRollBar(rearDriverW, rearPassengerW);
    }
}
