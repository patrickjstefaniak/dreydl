using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

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
                stopSpin();
            }
        }
    }

    void startSpin(){
        timer = Random.Range(timerRange[0], timerRange[1]);
        rb.angularDrag = drag[0];
        rb.AddTorque(transform.forward * Random.Range(spinPower[0],spinPower[1]), ForceMode.VelocityChange);
    }

    void stopSpin(){
        rb.angularDrag = drag[1];
        isSpinning = false;
        float stopAngle = transform.eulerAngles.z;
        if( 30 < stopAngle && stopAngle < 150){
            print(90);
            LeanTween.rotateZ(gameObject, 90, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }else if (-90 < stopAngle && stopAngle< 30){
            print(-30);
            LeanTween.rotateZ(gameObject, -30, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }else{
            print(-150);
            LeanTween.rotateZ(gameObject, -150, Random.Range(.2f,.5f)).setEase(LeanTweenType.easeOutBounce);
        }
    }
}
