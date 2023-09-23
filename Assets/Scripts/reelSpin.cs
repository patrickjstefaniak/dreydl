using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using FMODUnity;

public class reelSpin : MonoBehaviour
{

    public int slotID;
    private bool isSpinning;
    private bool hasLanded; 
    public float[] spinPower;
    public float[] drag;
    public float maxAngVel;
    private Rigidbody rb;
    public float[] timerRange;
    private float timer;
    public slotManager slotManager;


    public int stopCount;

    // Start is called before the first frame update
    void Start()
    {
        stopCount = 0;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngVel;
   

    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKeyDown("f"))
        {
            startSpin();
            isSpinning = true;
            
        }
        if(isSpinning){
            timer -= Time.deltaTime;
            if(timer <= 0){
                stopSpin();
            }
        }
    }

    void startSpin(){
        timer = Random.Range(timerRange[0], timerRange[1]);
        rb.angularDrag = drag[0];
        rb.AddTorque(transform.forward * Random.Range(spinPower[0],spinPower[1]), ForceMode.VelocityChange);
        slotManager.playSound();
    



    }

    void stopSpin(){
        rb.angularDrag = drag[1];
        isSpinning = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/reelStop");



        float stopAngle = transform.eulerAngles.z;
        if( 30 < stopAngle && stopAngle < 150){
            print(90 + "ב");
            //ב
            //result 2
            slotManager.reportResult(slotID, 2);
            LeanTween.rotateZ(gameObject, 90, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }else if (-90 < stopAngle && stopAngle< 30){
            print(-30 + "א");
            //א
            //result 1 
            slotManager.reportResult(slotID, 1);
            LeanTween.rotateZ(gameObject, -30, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }else{
            print(-150 + "ל");
            //ל
            //result 3
            slotManager.reportResult(slotID, 3);
            LeanTween.rotateZ(gameObject, -150, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }
    }
}
