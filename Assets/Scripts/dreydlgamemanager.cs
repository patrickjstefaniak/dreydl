//this is the manager for the overall game, for managing all of the individual hands

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using FMODUnity;

public class dreydlgamemanager : MonoBehaviour
{

    //public dreydlScoring dreydlscoring;
    public mainscore mainscore;
    public GameObject cashoutGO;
    bool isActive;
    string mode = "not started";
    int hands = 1;
    int finishedhands = 0;
    int finishedturns = 0;
    int turnCountMod = 0;
    public List<GameObject> cashOutComponents = new List<GameObject>();
    int currentBet = 10;
    int currentPlayer = 0;
    public GameObject dealdrawFlashing;
    GameObject placeBetText;
    public GameObject d1;
    public GameObject d10;
    public GameObject d15;
    float audioTimer;
    StudioEventEmitter voiceLines;
    float timeOutTimer = 120;
    public GameObject infunds;
    public GameObject[] cashoutoff;
    public GameObject cashOutZero;
    public bool isMachineBuild;
    Vector3 previousMouse;

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
        voiceLines = GetComponent<StudioEventEmitter>();
        resetAudioTimer();
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseup = false;
        bool mousedown = false;
        bool mouseleft = false;
        bool mouseright = false;
        Vector3 curMouse = Input.mousePosition;

        if(curMouse.x != previousMouse.x){
            if(curMouse.x > previousMouse.x){
                mouseright = true;
            }else{
                mouseleft = true;
            }
        }
        if(curMouse.y != previousMouse.y){
            if(curMouse.y > previousMouse.y){
                mouseup = true;
            }else{
                mousedown = true;
            }
        }

        if(isMachineBuild){
            if (Input.GetMouseButtonDown(1))
            {
                placeBet(1);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if ((Input.GetKeyDown("w")))
            {
                placeBet(2);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if ((Input.GetKeyDown("a")))
            {
                placeBet(3);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if ((Input.GetKeyDown("s")))
            {
                placeBet(5);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("d"))
            {
                print("Max Bet");
                maxBet();
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("f"))
            {
                print("Deal Draw");
                dealdrawFlashing.SetActive(false);
                placeBet(0);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }

            if (mouseup)
            {
                print("Cashout");
                cashOut(mainscore.getScore());
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (mousedown)
            {
                print("Call Attendant");
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (mouseleft)
            {
                print("1 hand");
                sethands(1);
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (mouseright)
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
        }else{
            if (Input.GetKeyDown("1") || Input.GetMouseButtonDown(1))
            {
                placeBet(1);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("2") || (Input.GetKeyDown("w")))
            {
                placeBet(2);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("3") || (Input.GetKeyDown("a")))
            {
                placeBet(3);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("5") || (Input.GetKeyDown("s")))
            {
                placeBet(5);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("d"))
            {
                print("Max Bet");
                maxBet();
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown("f"))
            {
                print("Deal Draw");
                dealdrawFlashing.SetActive(false);
                placeBet(0);
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print("Cashout");
                cashOut(mainscore.getScore());
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                print("Call Attendant");
                //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                print("1 hand");
                sethands(1);
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                print("10 hands");
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
                sethands(10);
            }

            //if (Input.GetMouseButtonDown(0))
            if (Input.GetKeyDown("i"))
            {
                print("15 hands");
                sethands(15);
                // FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
            }
        }

        //while dreydl is spinning, possibly trigger audio to play
        if (mode == "spinning")
        {
            //print("its playing");
            if (!voiceLines.IsPlaying())
            {
                //print("its not playing");
                audioTimer -= Time.deltaTime;
                if (audioTimer <= 0)
                {
                    print("play voice");
                    voiceLines.Play();
                    resetAudioTimer();
                }
            }
        }


        timeOutTimer -= Time.deltaTime;
        if(Input.anyKey){
            timeOutTimer = 120;
        }
        if(timeOutTimer <= 0){
            hardReset();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.DownArrow)){
            hardReset();
        }

        if(Input.GetKeyDown("w") && Input.GetKeyDown("a") && Input.GetKeyDown("s") && Input.GetMouseButtonDown(1)){
            hardReset();
        }
    }

    async void maxBet(){

        int curScore = mainscore.getScore();
        print("curScore " + curScore);
        // 15,30,45,75
        // 10,20,30,50
        // 1,2,3,5
        if(curScore >= 75){
            sethands(15);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(5);
        }else if(curScore >= 50){
            sethands(10);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(5);
        }else if(curScore >= 45){
            sethands(15);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(3);
        }else if(curScore >= 30){
            sethands(10);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(3);
            mode = "spinning";
        }else if(curScore >= 20){
            sethands(10);
            await Task.Delay(500);
            mode = "place bet";
            placeBet(2);
            mode = "spinning";
        }else if(curScore >= 15){
            sethands(15);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(1);
        }else if(curScore >= 10){
            sethands(10);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(1);
        }else if(curScore >= 5){
            sethands(1);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(5);
        }else if(curScore >= 3){
            sethands(1);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(3);
        }else if(curScore >= 2){
            sethands(1);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(2);
        }else if(curScore >= 1){
            sethands(1);
            mode = "spinning";
            await Task.Delay(500);
            mode = "place bet";
            placeBet(1);
        }
    }

    async void  insufFunds(){
        print("insufficient funds");
        FMODUnity.RuntimeManager.PlayOneShot("event:/insufficient");
        infunds.SetActive(true);
        
        await Task.Delay(1000);
        infunds.SetActive(false);
    }

    void hardReset(){
        SceneManager.LoadScene("titleScreen");
        ///SceneManager.UnloadScene("dreydl_spin");
    }

    void resetAudioTimer()
    {
        audioTimer = UnityEngine.Random.Range(2, 60);
    }

    void placeBet(int bet)
    {
        if (mode == "place bet")
        {
            //bet is 0 when a repeat bet is made
            //10 is just the original amount so that pressing spin when no bet has been made yet does nothing
            if (bet == 0)
            {
                if (currentBet != 10)
                {
                    //dreydlscoring.placeBet(currentBet);
                    if(!mainscore.checkBet(currentBet * hands)){
                        //play sound or something
                        
                        insufFunds();
                        return;
                    }
                    sendBets(currentBet);
                    mainscore.updateScore(true, -1 * currentBet * hands);
                }
            }
            else
            {
                if(!mainscore.checkBet( bet * hands)){
                    //play sound or something
                    
                    insufFunds();
                    return;
                }
                mainscore.updateScore(true, -1 * bet * hands);
                currentBet = bet;
                //dreydlscoring.placeBet(bet);
                if (hands == 1)
                {
                    if (UnityEngine.Random.Range(0, 100) > 80)
                    {
                        GameObject.Find("dreydl container").GetComponent<basicSpin>().set22(true);
                    }
                    else
                    {
                        GameObject.Find("dreydl container").GetComponent<basicSpin>().set22(false);
                    }
                }
                sendBets(bet);
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


    void sethands(int h)
    {
        if (mode == "place bet")
        {
            hands = h;
            //toggle on and off dreydls
            if (h == 1)
            {
                d1.SetActive(true);
                d10.SetActive(false);
                d15.SetActive(false);
            }
            else if (h == 10)
            {
                d1.SetActive(false);
                d10.SetActive(true);
                d15.SetActive(false);
            }
            else if (h == 15)
            {
                d1.SetActive(false);
                d10.SetActive(false);
                d15.SetActive(true);
            }
        }
    }

    //see if all hands are done, if so set next bet
    public void handFinished()
    {
        finishedhands++;
        print("hand finished: " + finishedhands);
        if (finishedhands >= hands)
        {
            dealdrawFlashing.SetActive(false);
            if (UnityEngine.Random.Range(0, 100) > 90)
            {
                activateSlot();
            }
            else
            {
                startNextBet();
            }
        }
    }

    public void startNextBet()
    {

        mode = "place bet";
        placeBetText.SetActive(true);
        finishedhands = 0;
        finishedturns = 0;
        turnCountMod = 0;
    }

    public void setCurrentPlayer(int p){
        currentPlayer = p;
    }

    public void turnFinished()
    {
        finishedturns++;
        print("turn finished" + finishedturns + " " + turnCountMod);

        if (finishedturns >= hands - turnCountMod)
        {
            //next turn
            
            mainscore.updateScoreBoard();
            finishedturns = 0;
            turnCountMod = finishedhands;
            print("start next turn");
            foreach (dreydlScoring ds in FindObjectsOfType<dreydlScoring>())
            {
                ds.nextTurn();
                
            }
            if(currentPlayer == 0){
                dealdrawFlashing.SetActive(true);
            }
        }
    }

    void sendBets(int bet)
    {
        foreach (dreydlScoring ds in FindObjectsOfType<dreydlScoring>())
        {
            ds.placeBet(bet);
        }
    }

    public async void cashOut(int amount)
    {
        int cashhh = amount;
        mode = "cash out";
        if (isActive)
        {
            isActive = false;
            foreach(GameObject go in cashoutoff){
                go.SetActive(false);
            }
            if(cashhh > 100){
                cashhh = 100;
            }

            if(amount <= 0){
                cashOutZero.SetActive(true);
                FMODUnity.RuntimeManager.PlayOneShot("event:/noMoney");
            }
            else{
                //open cashout scene
                print("cash out: " + cashhh);
                FMODUnity.RuntimeManager.PlayOneShot("event:/cashOut");
                try{
                    string pdfFilePath = $"/Users/forest/Documents/Cash_Out_Voucher_DREYDL/{cashhh}.pdf";
                    PrintPDF.pdfFilePath = pdfFilePath;
                    GetComponent<PrintPDF>().Print();
                }catch(Exception ex){}
               
                cashoutGO.SetActive(true);
                displayCashout(cashhh);
                //mainscore.coinBust(cashhh, 17000);
            }
            
            
            await Task.Delay(18000);
            hardReset();
        }
    }

    public void activateSlot()
    {
        dealdrawFlashing.SetActive(false);
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
