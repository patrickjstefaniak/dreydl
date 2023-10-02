using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using FMODUnity;

public class dreydlgamemanager : MonoBehaviour
{

    public dreydlScoring dreydlGame;
    public mainscore mainscore;
    public GameObject cashoutGO;
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1") || Input.GetMouseButtonDown(1))
        {
            placeBet(1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("2") || (Input.GetKeyDown("w"))){
            placeBet(2);
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("3") || (Input.GetKeyDown("a"))){
            placeBet(3);
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("5") || (Input.GetKeyDown("s") )){
            placeBet(5);
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKeyDown("d"))
        {
            print("Max Bet");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Cashout");
            cashOut(mainscore.getScore());
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Call Attendant");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("1 hand");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("10 hands");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("15 hands");
            FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
    }

    void placeBet(int bet){
        mainscore.updateScore(true, -1 * bet);
        dreydlGame.placeBet(bet);
    }

    public async void cashOut(int amount){
        if(isActive){
            isActive = false;
            //open cashout scene
            print("cash out: " + amount);
            FMODUnity.RuntimeManager.PlayOneShot("event:/cashOut");
            // string pdfFilePath = $"/Users/forest/Documents/Cash_Out_Voucher_DREYDL/{amount}.pdf";
            // PrintPDF.pdfFilePath = pdfFilePath;
            // GetComponent<PrintPDF>().Print();
            cashoutGO.SetActive(true);
            mainscore.coinBust(amount, 4500);
            await Task.Delay(5000);
            SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
            SceneManager.UnloadScene("dreydl_spin");
        }
    }

    public void activateSlot(){
        SceneManager.LoadScene("slotmachine", LoadSceneMode.Additive);
    }
}
