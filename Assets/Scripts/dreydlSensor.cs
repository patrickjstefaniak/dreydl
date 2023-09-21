using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dreydlSensor : MonoBehaviour
{
    private List<GameObject> face;
    // Start is called before the first frame update
    void Start()
    {
        face = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        face.Add(other.gameObject);
        print(other.gameObject.name);
    }

    void OnTriggerExit(Collider other){
        face.Remove(other.gameObject);
    }

    public string getFace(){
        GameObject go = face[0];
        face.Clear();
        return go.name;
    }
}
