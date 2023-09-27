using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenController : MonoBehaviour
{
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f") && isActive)
        {
            isActive = false;
            SceneManager.LoadScene("dreydl_spin", LoadSceneMode.Additive);
            SceneManager.UnloadScene("titleScreen");
        }
    }
}
