using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.Threading.Tasks;

public class basicSpin : MonoBehaviour
{
    private bool isSpinning;
    private bool hasLanded = false;
    private Rigidbody rb;
    public float torque = 10;
    public float rotation = 1;
    public float throwForce;
    public float throwTorque;
    private Vector3 startPos;
    private Quaternion startRot;
    public float maxAngVel = 21;
    public dreydlSensor ds;
    public dreydlScoring scoring;
    private string landedFace;
    private FMOD.Studio.EventInstance instance;
    public int voiceChance;
    private int voiceRange;



    // Start is called before the first frame update
    void Start()
    {
        maxAngVel = Random.Range(26, 18);
        isSpinning = true;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        startPos = transform.position;
        startRot = transform.rotation;
        FMODUnity.RuntimeManager.LoadBank("Master");
      
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
                print("landed face " + landedFace);
                
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
        rb.AddForce(Random.Range(-throwForce,throwForce),0,Random.Range(-throwForce,throwForce));
        rb.AddTorque(Random.Range(-throwTorque,throwTorque),0,Random.Range(-throwTorque,throwTorque));
        voiceChance = Random.Range(1, 3);
        print(voiceChance);
  

        if (voiceChance != 3)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/cardPlayer");
            voiceActing();
        }
    }

    async void voiceActing()
    {

        voiceRange = Random.Range(0, 4000);
        
        await Task.Delay(voiceRange);
        FMODUnity.RuntimeManager.PlayOneShot("event:/cardPlayer");

    }

    public void resetDreydl(){
        isSpinning = true;
        hasLanded = false;
        rb.useGravity = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
