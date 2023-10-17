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
    int currentBet = 10;
    
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
            placeBet(0);
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
        
        if(bet == 0){
            if(currentBet != 10){
                dreydlGame.placeBet(currentBet);
                mainscore.updateScore(true, -1 * currentBet);
            }
        }else{
            mainscore.updateScore(true, -1 * bet);
            currentBet = bet;
            dreydlGame.placeBet(bet);
        }
        
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
            mainscore.coinBust(amount, 17000);
            await Task.Delay(18000);
            SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
            SceneManager.UnloadScene("dreydl_spin");
        }
    }

    public void activateSlot(){
        SceneManager.LoadScene("slotmachine", LoadSceneMode.Additive);
    }

    async void displayCashout(int cashoutNumber)
    {
        GameObject dreyd = GameObject.Find("dreydl");
        print("turning UI on");
        if (cashoutNumber >= 0 && cashoutNumber < cashOutComponents.Count)
        {
            cashOutComponents[cashoutNumber].SetActive(true);
        }
        dreyd.SetActive(false);
        await Task.Delay(17000);
        print("turning UI off");
        if (cashoutNumber >= 0 && cashoutNumber < cashOutComponents.Count)
        {
            cashOutComponents[cashoutNumber].SetActive(false);
        }
        dreyd.SetActive(true);
    }


}
