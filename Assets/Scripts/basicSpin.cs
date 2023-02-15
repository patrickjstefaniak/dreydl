using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSpin : MonoBehaviour
{
    private bool isSpinning;
    private Rigidbody rb;
    public float torque = 10;
    private Vector3 startPos;
    private Quaternion startRot;
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
        if(isSpinning){
            addSpin();
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
        rb.AddTorque(transform.forward * torque);

    }

    void drop(){
        isSpinning = false;
        rb.useGravity = true;
    }

    void resetDreydl(){
        isSpinning = true;
        rb.useGravity = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
