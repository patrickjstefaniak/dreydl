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
    public List<GameObject> cashOutComponents = new List<GameObject>();
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
            displayCashout(amount);
            mainscore.coinBust(amount, 4500);
            await Task.Delay(4000);
            SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
            SceneManager.UnloadScene("dreydl_spin");
        }
    }

    public void activateSlot(){
        SceneManager.LoadScene("slotmachine", LoadSceneMode.Additive);
    }

    async void displayCashout(int cashoutNumber)
    {
        if (cashoutNumber > 0 && cashoutNumber <= 100)
        {
            TurnOnCashoutComponent(cashoutNumber);
           // await Task.Delay(9000);
           // TurnOffCashoutComponent(cashoutNumber);
        }
        else if (cashoutNumber >= 100)
            {
                TurnOnCashoutComponent(100);
             //   await Task.Delay(9000);
             //   TurnOffCashoutComponent(100);
            }



    }
    public void TurnOnCashoutComponent(int index)
    {
        print("turning UI on");
        if (index >= 0 && index < cashOutComponents.Count)
        {
            cashOutComponents[index].SetActive(true);
        }
    }
    public void TurnOffCashoutComponent(int index)
    {
        print("turning UI off");
        if (index >= 0 && index < cashOutComponents.Count)
        {
            cashOutComponents[index].SetActive(false);
        }
    }
}
