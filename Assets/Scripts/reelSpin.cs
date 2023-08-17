using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reelSpin : MonoBehaviour
{

    private bool isSpinning;
    private bool hasLanded; 
    public float[] spinPower;
    public float[] drag;
    public float maxAngVel;
    private Rigidbody rb;
    public float[] timerRange;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngVel;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            startSpin();
            isSpinning = true;
        }
        if(isSpinning){
            timer -= Time.deltaTime;
            if(timer <= 0){
                rb.angularDrag = drag[1];
            }
        }
    }

    void startSpin(){
        timer = Random.Range(timerRange[0], timerRange[1]);
        rb.angularDrag = drag[0];
        rb.AddTorque(transform.forward * Random.Range(spinPower[0],spinPower[1]), ForceMode.VelocityChange);
    }
}
