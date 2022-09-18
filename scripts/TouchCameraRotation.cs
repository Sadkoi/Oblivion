using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TouchCameraRotation : MonoBehaviour
{
    public Vector3 firstPoint;
    public IntroIII_theFight IntroIII_theFight;
    public GameObject canvas;
    public GameObject myraHips;
    public GameObject myraNeck;
    public GameObject myraArm;
    public GameObject aimbot;

    public GameObject aimBorder;
    public RawImage aimBorderImage;

    public float startTime;
    public float time;
    public bool isRightMouseDown;

    public float sensitivity = 60f;

    private float rotX;
    private float rotY;
 
    void Start()
    {
        IntroIII_theFight = canvas.GetComponent<IntroIII_theFight>();

    }

    void Update()
    {
        if(IntroIII_theFight.playerAssumedControl){ 
            mouseRotation();
            aimBorderAnim();
        }
        time += Time.deltaTime;
        
    }

    void aimBorderAnim(){
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            startTime = time;
        }
        if(Input.GetKey(KeyCode.Mouse1)){
            float scaleFactor = (1f - (time - startTime));
            scaleFactor = Mathf.Clamp(scaleFactor, 0.75f, 1f);
            aimBorder.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
        }else{
            aimBorder.transform.localScale = new Vector3(1f,1f,1f);
        }
        
    }

    void mouseRotation(){
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -90f, 90f);
        myraHips.transform.eulerAngles = new Vector3(0f, rotX, 0f);
        transform.eulerAngles = new Vector3(-rotY,myraHips.transform.eulerAngles.y,0f);
    }

    /*
    //Code for android build
    void TouchRotation()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstPoint = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 secondPoint = Input.GetTouch(0).position;
 
                float x = FilterGyroValues(secondPoint.x - firstPoint.x);
                RotateRightLeft(x * sensitivity);
 
                float y = FilterGyroValues(secondPoint.y - firstPoint.y);
                RotateUpDown(y * -sensitivity);
 
                firstPoint = secondPoint;
            }
        }

        myraArm.transform.LookAt(aimbot.transform);

    }

    float FilterGyroValues(float axis)
    {
        float thresshold = 0.5f;
        if (axis < -thresshold || axis > thresshold)
        {
            return axis;
        }
        else
        {
            return 0;
        }
    }

    public void RotateRightLeft(float axis)
    {
        myraHips.transform.RotateAround(myraHips.transform.position, Vector3.up, -axis * Time.deltaTime);
    }

    public void RotateUpDown(float axis)
    {
        transform.RotateAround(transform.position, transform.right, -axis * 0.5f * Time.deltaTime);
    }
    */
}