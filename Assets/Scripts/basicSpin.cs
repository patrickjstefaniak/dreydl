using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSpin : MonoBehaviour
{
    private bool isSpinning;
    private bool hasLanded = false;
    private Rigidbody rb;
    public float torque = 10;
    public float rotation = 1;
    private Vector3 startPos;
    private Quaternion startRot;
    public float maxAngVel;
    public dreydlSensor ds;
    public dreydlScoring scoring;
    private string landedFace;
    // Start is called before the first frame update
    void Start()
    {
        isSpinning = true;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rb.maxAngularVelocity = maxAngVel;
        if(isSpinning){
            addSpin();
        }else{
            if(!hasLanded && rb.angularVelocity.magnitude == 0){
                landedFace = ds.getFace();
                hasLanded = true;
                scoring.landed(landedFace);
            }
        }


        if(Input.GetKeyDown("space")){
            if(isSpinning){
                drop();
            }else{
                resetDreydl();
            }
        }
    }

    void addSpin(){
       rb.AddTorque(transform.forward * torque, ForceMode.VelocityChange);
        //rb.AddForce(transform.forward * torque, ForceMode.VelocityChange);
        //transform.Rotate(0,0,rotation);
    }

    void drop(){
        isSpinning = false;
        rb.useGravity = true;
    }

    void resetDreydl(){
        isSpinning = true;
        hasLanded = false;
        rb.useGravity = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
