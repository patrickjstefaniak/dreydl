//this is to make an individual dreydl spin and detect when its done and what face it landed on

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
    bool is22sided = false;
    Transform dreydlT;
    Transform dreydlT22;
    GameObject followcam;
    Vector3 followCamDist;


    // Start is called before the first frame update
    void Start()
    {
        maxAngVel = Random.Range(28, 15);
        isSpinning = true;
        dreydlT = transform.Find("dreydl");
        dreydlT22 = transform.Find("22 dreydl");
        rb = dreydlT.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        
        startPos = dreydlT.position;
        startRot = dreydlT.rotation;
        FMODUnity.RuntimeManager.LoadBank("Master");
        followcam = GameObject.Find("follow cam");
        followCamDist = followcam.transform.position - dreydlT.position;
    }

 



        // Update is called once per frame
        void Update()
    {
        
        rb.maxAngularVelocity = maxAngVel;
        if(isSpinning){
            addSpin();
        }else{
            if(!hasLanded && rb.angularVelocity.magnitude == 0 && rb.velocity.magnitude == 0){
                //landedFace = ds.getFace();
                landedFace = getFace();
                hasLanded = true;
               
                scoring.landed(landedFace);
                print("landed face " + landedFace);
                
                maxAngVel = Random.Range(28, 15);

            }
        }

        if(Input.GetKeyDown("o")){
            set22(!is22sided);
        }

        if(is22sided){
            followcam.transform.position = dreydlT22.position + followCamDist;
        }else{
            followcam.transform.position = dreydlT.position + followCamDist;
        }
        
    }


//determine what face it landed on by seeing which string sound collider is highest
    string getFace(){
        float highestPos = 0;
        string highestString = "";
        Transform d = transform.Find("dreydl");;
        if(is22sided){
            d = transform.Find("22 dreydl");
        }
        foreach(Transform child in d){
            if(child.position.y > highestPos){
                highestPos = child.position.y;
                highestString = child.name;
            }
        }
        return highestString;
    }

    public void dropIt(){
        if(isSpinning){
            drop();
        }else if(hasLanded){
            resetDreydl();
            print("reset dropit");
        }
    }

    void addSpin(){
       rb.AddTorque(transform.up * torque, ForceMode.VelocityChange);
        //rb.AddForce(transform.forward * torque, ForceMode.VelocityChange);
        //transform.Rotate(0,0,rotation);
    }

    void drop(){
        isSpinning = false;
        rb.useGravity = true;
       rb.AddForce(Random.Range(-throwForce,throwForce),0,Random.Range(-throwForce,throwForce));
       rb.AddTorque(Random.Range(-throwTorque,throwTorque),0,Random.Range(-throwTorque,throwTorque));
        voiceChance = Random.Range(1, 100);
        //print(voiceChance);
  


    }

    

    public void set22(bool b){
        is22sided = b;
        if(b){
            rb = dreydlT22.gameObject.GetComponent<Rigidbody>();
            dreydlT22.gameObject.SetActive(true);
            dreydlT.gameObject.SetActive(false);
        }else{
            rb = dreydlT.gameObject.GetComponent<Rigidbody>();
            dreydlT22.gameObject.SetActive(false);
            dreydlT.gameObject.SetActive(true);
        }
    }

    public void resetDreydl(){

        isSpinning = true;
        hasLanded = false;
        rb.useGravity = false;
        rb.position = startPos;
        rb.rotation = startRot;
        
    }
}
