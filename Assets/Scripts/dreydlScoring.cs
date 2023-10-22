//this is the scoring module for an individual dreydl hand

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using System.Threading.Tasks;


public class dreydlScoring : MonoBehaviour
{
    int pot = 0;
    int currentPlayer = 0;
    int[] players;
    int ante = 1;
    string lastSide;
    bool skipNextPlayer;
    bool reverseOrder; 
    bool isPlaying = false;
    bool isSlot = false;
    public Text playerT;
    public Text player2T;
    public Text player3T;
    public Text player4T;
    public Text currentPlayerT;
    public Text potT;
    public Text sideT;
    public Text ruleT;
    public basicSpin bSpin;
    private float spinTimer;
    private float nextTurnTimer;
    public mainscore mainscore;
    public float[] nextTurnTimes;
    public Text previousT;
    string landedLetter;
    string landedRule;
    public GameObject ruleText;
    public List<GameObject> uiComponents = new List<GameObject>();
    string hebrewletter;
    
    // private FMOD.Studio.EventInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        players = new int[4];
        nextTurnTimer = 9999999;
       

    }

    // Update is called once per frame
    void Update()
    {
        print(players[0]);
        if(!isSlot){
            if(isPlaying){
                if(nextTurnTimer >= 0){
                    nextTurnTimer -= Time.deltaTime;
                    if(nextTurnTimer <= 0){
                        bSpin.resetDreydl();
                        spinTimer = Random.Range(3.3f,0.5f);
                    }
                    
                }else{
                    if(currentPlayer == 0){
                        if (Input.GetKeyDown("space")){
                            dropIt();
                        }
                    }else{
                        spinTimer -= Time.deltaTime;
                        
                       
                        if (spinTimer <= 0){
                            bSpin.dropIt();
                        }
                    }
                }
            }
        }
    }


    public void dropIt(){


        bSpin.dropIt();
        //FMODUnity.RuntimeManager.PlayOneShot("event:/buttonClick", GameObject.Find("dreydl").transform.position);
    }



    public void setSlotMode(bool b){
        isSlot = b;
    }

    public void TurnOnUIComponent(int index)
    {
        print("turning UI on");
        if (index >= 0 && index < uiComponents.Count)
        {
            uiComponents[index].SetActive(true);
            ruleText.SetActive(true);
        }
    }

    public void TurnOffUIComponent(int index)
    {
        print("turning UI off");
        if (index >= 0 && index < uiComponents.Count)
        {
            uiComponents[index].SetActive(false);
            ruleText.SetActive(false);
        }
    }


    void payAnte(){
        for(int i = 0; i < 4; i++){
            players[i] -= ante;
        }
        pot += ante * 4;
    }

    async void displayLetter(int letterNumber)
    {
        print("DISPLAYING LETTER");
        TurnOnUIComponent(letterNumber);
        await Task.Delay(2200);
        TurnOffUIComponent(letterNumber);


    }

    void updateValues(){
        playerT.text = "you: " + players[0];
        player2T.text = "forest: " + players[1];
        player3T.text = "bugsy: " + players[2];
        player4T.text = "patrick: " + players[3];
        potT.text = "pot: " + pot;
        ruleT.text = landedRule;

        sideT.text = lastSide;
    }

    void nextTurn(){
        if(!reverseOrder){
            currentPlayer += 1;
            if(currentPlayer > 3){
                currentPlayer = 0;
            }
        }else{
            currentPlayer -= 1;
            if (currentPlayer < 0){
                currentPlayer = 3;
            }
        }
        switch(currentPlayer){
            case 0: 
                currentPlayerT.text = "current player: you";
                break;
            case 1: 
                currentPlayerT.text = "current player: forest";
                break;
            case 2: 
                currentPlayerT.text = "current player: bugsy";
                break;
            case 3: 
                currentPlayerT.text = "current player: patrick";
                break;
            default:
                currentPlayerT.text = "whoops";
                break;
        }
        spinTimer = 999999;
        nextTurnTimer = Random.Range(nextTurnTimes[0], nextTurnTimes[1]);
        if(skipNextPlayer){
            skipNextPlayer = false;
            nextTurn();
        }
    }

    //nothing
    //take half of pot
    //get the whole pot
    //put in 5

    //ante 5

    int playerToLeft(){
        int left = currentPlayer ++;
            if(left >3){
                left = 0;
            }
        return left;
    }

    public async void placeBet(int bet){
        pot = 0;
        currentPlayer = 0;
        currentPlayerT.text = "current player: you";
        nextTurnTimer = 999;
        print(players[0]);
        for(int i = 0; i < 4; i++){
            players[i] = bet;
        }
        payAnte();
        updateValues();
        isPlaying = true;
        bSpin.resetDreydl();
        await Task.Delay((int)(Random.Range(50,600)));
        dropIt();
    }

    public void landed(string side){
        lastSide = side;
        int half;
        int left;

        print("scoring "+ side);
        switch (side){
            case "Heh":
            //half
                print("heh");
                half = (int)Mathf.Floor(pot /2);
                players[currentPlayer] += half;
                pot -= half;
                landedRule = "";
                landedLetter = "heh";
                hebrewletter = "ה";
                landedRule = "Halb - Take half the pot";
                displayLetter(5);
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Heh", GameObject.Find("dreydl").transform.position);
                break;
            case "Nun":
            //nothing
                print("nun");
                landedLetter = "nun";
                hebrewletter = "נ";
                landedRule = "Nisht - Nothing happens";
                displayLetter(14);
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Nun", GameObject.Find("dreydl").transform.position);
                break;
            case "Gimel":
            //all
                print("gimel");
                displayLetter(3);
                landedLetter = "gimel";
                hebrewletter = "ג";
                landedRule = "Gantz - Take the whole pot";
                players[currentPlayer] += pot;
                pot = 0;
                if (currentPlayer == 0)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Gimel_WIN_DREYDL", GameObject.Find("dreydl container").transform.position);
                    }
                else
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/OtherGimel", GameObject.Find("dreydl container").transform.position);
                    }
                break;
            case "Shin":
            //put in
                print("shin");
                players[currentPlayer] -= ante;
                pot += ante;
                landedLetter = "shin";
                hebrewletter = "ש";
                    landedRule = "Shtel Arayn - Put one in the pot";
                    displayLetter(21);
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Shin", GameObject.Find("dreydl").transform.position);
                    break;
            case "Alef":
                print("Alef");
                displayLetter(1);
                hebrewletter = "א";
                landedRule = "Antlayen - Take one from the player to your left";
                //take one from player to left
                left = playerToLeft();
                players[left] --;
                players[currentPlayer] ++;
                landedLetter = "Alef";
                 
                break;
            case "Beys":
                print("Beys");
                landedLetter = "Beys";
                displayLetter(2);
                hebrewletter = "ב";
                landedRule = "Bagzlen - Take one from each other player";
                //take one from every other player
                for (int i = 0; i < 4; i++){
                    players[i] --;
                }
                players[currentPlayer] += 2;
                break;
            case "Daled":
                print("Daled");
                landedLetter = "Daled";
                hebrewletter = "ד";
                landedRule = "Dreyen - Spin the dreydl again";
                displayLetter(4);
                //spin again
                break;
             
            case "Vov":
            //everyone puts one in pot
                print("Vov");
                displayLetter(6);
                hebrewletter = "ו";
                landedLetter = "Vov";
                landedRule = "Vetn Zikh - All players add one to the pot";
                for (int i = 0; i < 4; i++){
                    players[i] --;
                }
                pot += 4;
                   
                    break;
                
            case "Zayen":
            //half
                print("Zayen");
                landedLetter = "Zayen";
                hebrewletter = "ז";
                displayLetter(7);
                landedRule = "Zamlen - Take one third of the pot";
                //take one third of pot
                half = (int)Mathf.Floor(pot /3);
                players[currentPlayer] += half;
                pot -= half;
              
                    break;
            case "Khes":
                //Return 1/2 your gelt to the pot
                print("Khes");
                landedLetter = "Khes";
                hebrewletter = "ח";
                displayLetter(8);
                landedRule = "Kharote Hobn - Return half your gelt to the pot";
                half = (int)Mathf.Floor(players[currentPlayer] /2);
                players[currentPlayer] -= half;
                pot += half;
               
                    break;
            case "Tes":
            //Divide the pot amongst the players
                print("Tes");
                displayLetter(9);
                landedLetter = "Tes";
                hebrewletter = "ט";
                landedRule = "Teyln zikh - Divide the pot amongst the players";
                half = (int)Mathf.Floor(pot /4);
                players[currentPlayer] += half;
                pot = 0;
             
                    break;
            case "Yud":
            //Swap gelt with the player to your left
                print("Yud");
                displayLetter(10);
                landedLetter = "Yud";
                hebrewletter = "י";
                landedRule = "Yentsn - Swap gelt with the player to your left";
                left = playerToLeft();
                int leftsGelt = players[left];
                players[left] = players[currentPlayer];
                players[currentPlayer] = leftsGelt;
              
                    break;
            case "Khof":
            //Give one to the player to your left
                print("Khof");
                displayLetter(11);
                landedLetter = "Khof";
                hebrewletter = "כ";
                landedRule = "Khabar - Give one to the player to your left";
                left = playerToLeft();
                players[currentPlayer] --;
                players[left] ++;
             
                    break;
            case "Lamed":
            //Player to your left puts one in the pot
                print("Lamed");
                landedLetter = "Lamed";
                    hebrewletter = "ל";
                displayLetter(12);
                landedRule = "Litsitator - Player to your left puts one in the pot";
                left = playerToLeft();
                players[left] --;
                pot ++;
                break;
            case "Mem":
            //Player to your left takes one from the pot
                print("Mem");
                displayLetter(13);
                landedLetter = "Mem";
                hebrewletter = "מ";
                landedRule = "Matone - Player to your left takes one from the pot";
                left = playerToLeft();
                players[left] ++;
                pot --;
               

                    break;
            case "Samekh":
            //All players take one from the pot (starting with player to your left)
                print("Samekh");
                landedLetter = "Samekh";
                    hebrewletter = "ס";
                landedRule = "Sotsfarzikher	- All players take one from the pot";
                displayLetter(15);
                left = playerToLeft();
                for(int i = 0; i < 3; i++){
                    players[left] ++;
                    pot --;
                    if(pot <= 0){
                        break;
                    }
                    left ++;
                    if(left > 3){
                        left = 0;
                    }
                }
                break;
            case "Ayen":
            //Skip next players turn
                print("Ayen");
                displayLetter(16);
                landedLetter = "Ayen";
                hebrewletter = "ע";
                landedRule = "Iber-Yor - Skip next players turn";
                skipNextPlayer = true;
               
                    break;
            case "Fey":
                //Player to your left takes half the pot
                displayLetter(17);
                print("Fey");
                landedLetter = "Fey";
                hebrewletter = "פ";
                landedRule = "Plet - Player to your left takes half the pot";
                left = playerToLeft();
                 half = (int)Mathf.Floor(pot /2);
                players[left] += half;
                pot -= half;
               
                    break;
            case "Tsadek":
            //Take one from player with most gelt  (take from multiple players if tied)
                print("Tsadek");
                displayLetter(18);
                landedLetter = "Tsadek";
                hebrewletter = "צ";
                landedRule = "Tsoltarif	- Take one from player with most gelt";
                int highestAmount = 0;
                for(int i = 0; i < 4; i++){
                    if(players[i] > highestAmount){
                        highestAmount = players[i];
                    }
                }
                for(int i = 0; i < 4; i++){
                    if(players[i] == highestAmount){
                        players[i] --;
                        players[currentPlayer] ++; 
                    }
                }
               
                    break;
            case "Kuf":
            //Split the pot with the player to your left
                print("Kuf");
                displayLetter(19);
                landedLetter = "Kuf";
                hebrewletter = "ק";
                landedRule = "Kooperatsiye - Split the pot with the player to your left";
                left = playerToLeft();
                 half = (int)Mathf.Floor(pot /2);
                players[left] += half;
                players[currentPlayer] += half;
                pot = 0;
              
                    break;
            case "Reysh":
            //Reverse turn order
                print("Reysh");
                displayLetter(20);
                landedLetter = "Reysh";
                hebrewletter = "ר";
                landedRule = "Rotirn - Reverse turn order";

                reverseOrder = true;
                
                    break;
            case "Tof":
            //Pot goes to player with the least gelt
                print("Tof");
                    hebrewletter = "ת";
                    landedLetter = "Tof";
                landedRule = "Tikun - Pot goes to player with the least gelt";
                displayLetter(22);
                int lowestAmount = 99999;
                for(int i = 0; i < 4; i++){
                    if(players[i] < lowestAmount){
                        lowestAmount = players[i];
                    }
                }
                int numOfLowest = 0;
                for(int i = 0; i < 4; i++){
                    if(lowestAmount == players[i]){
                        numOfLowest ++;
                    }
                }
                 half = (int)Mathf.Floor(pot /numOfLowest);
                pot = 0;
                for(int i = 0; i < 4; i++){
                    if(lowestAmount == players[i]){
                        players[i] ++;
                    }
                }
                landedLetter = "Tof";
                break;
            default:
                print("no sides matched");
                break;
        }

        
        previousT.text = hebrewletter + previousT.text;
        //see if round is over
        if(pot == 0 || players[0] < 0){
            isPlaying = false;
            if(players[0] > 0){
                mainscore.updateScore(false, players[0]);
            }
            mainscore.maybeStartSlot();
        }else{
            nextTurn();
        }
        updateValues();
        // if(pot < 1){
        //     payAnte();
        // }
        
    }
}
