//this is the manager for the overall game, for managing all of the individual hands

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using FMODUnity;

public class dreydlgamemanager : MonoBehaviour
{

    public dreydlScoring dreydlscoring;
    public mainscore mainscore;
    public GameObject cashoutGO;
    bool isActive;
    string mode = "not started";
    int hands = 1;
    int finishedhands = 0;
    int finishedturns = 0;
    public List<GameObject> cashOutComponents = new List<GameObject>();
    int currentBet = 10;
    int currentPlayer = 0;
    public GameObject dealdrawFlashing;
    GameObject placeBetText;
    
    // Start is called before the first frame update
    async void Start()
    {
        dealdrawFlashing.SetActive(false);
        isActive = true;
        mode = "not started";
        placeBetText = GameObject.Find("placebetflashing");
        await Task.Delay(500);
        mode = "place bet";
        placeBetText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1") || Input.GetMouseButtonDown(1))
        {
            placeBet(1);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("2") || (Input.GetKeyDown("w"))){
            placeBet(2);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("3") || (Input.GetKeyDown("a"))){
            placeBet(3);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if(Input.GetKeyDown("5") || (Input.GetKeyDown("s") )){
            placeBet(5);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKeyDown("d"))
        {
            print("Max Bet");
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
            placeBet(0);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Cashout");
            cashOut(mainscore.getScore());
           // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Call Attendant");
            //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("1 hand");
            sethands(1);
           // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("10 hands");
           // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
           sethands(10);
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("15 hands");
            sethands(15);
           // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
        }
    }

    void placeBet(int bet){
        if(mode == "place bet"){
            if(bet == 0){
                if(currentBet != 10){
                    dreydlscoring.placeBet(currentBet);
                    mainscore.updateScore(true, -1 * currentBet);
                }
            }else{
                mainscore.updateScore(true, -1 * bet);
                currentBet = bet;
                dreydlscoring.placeBet(bet);
            }
            mode = "spinning";
            finishedhands = 0;
            finishedturns = 0;
            placeBetText.SetActive(false);

            
            //voice stufff .... 

                // voiceRange = Random.Range(0, 2500);
                
                // await Task.Delay(voiceRange);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/cardPlayer");

            
        }
    }

    void sethands(int h){
        if(mode == "place bet"){
            hands = h;
            //toggle on and off dreydls
        }
    }

//see if all hands are done, if so set next bet
    public void handFinished(){
        finishedhands ++;
        if(finishedhands >= hands){
            mode = "place bet";
        }
    }

    public void turnFinished(){
        finishedturns ++;
        if(finishedturns >= hands){
            //next turn
            finishedturns = 0;
        }
    }

    public async void cashOut(int amount){
        mode = "cash out";
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
        mode = "slot";
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
