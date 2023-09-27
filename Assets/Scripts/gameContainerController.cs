using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameContainerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
