﻿using System.Collections;
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
    public float maxAngVel = 21;
    public dreydlSensor ds;
    public dreydlScoring scoring;
    private string landedFace;
    // Start is called before the first frame update
    void Start()
    {
        maxAngVel = Random.Range(26, 18);
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
            if(!hasLanded && rb.angularVelocity.magnitude == 0 && rb.velocity.magnitude == 0){
                landedFace = ds.getFace();
                //landedFace = getFace();
                hasLanded = true;
               
                scoring.landed(landedFace);
                maxAngVel = Random.Range(26, 18);

            }
        }


        
    }

    string getFace(){
        float z = transform.eulerAngles.z;
        float x = transform.eulerAngles.x;
        print("z " + z + " x " + x);
        if(z >= 87 && z <= 93){
            return "gimel";
        }else if(z >= -93 && z <= -87){
            return "shin";
        }else if(((z >= -3 && z <= 3) || (z >= 357 && z <= 360)) && x >= -3 && x <= 3){
            return "heh";
        }else{
            return "nun";
        }
    }

    public void dropIt(){
        if(isSpinning){
            drop();
        }else if(hasLanded){
            resetDreydl();
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

    public void resetDreydl(){
        isSpinning = true;
        hasLanded = false;
        rb.useGravity = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
