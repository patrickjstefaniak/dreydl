using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dreydlSensor : MonoBehaviour
{
    private GameObject face;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        face = other.gameObject;
    }

    public string getFace(){
        return face.name;
    }
}
