﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text playerT;
    public Text player2T;
    public Text player3T;
    public Text player4T;
    public Text currentPlayerT;
    public Text potT;
    public Text sideT;
    public basicSpin bSpin;
    private float spinTimer;
    private float nextTurnTimer;
    public mainscore mainscore;
    public float[] nextTurnTimes;
    // Start is called before the first frame update
    void Start()
    {
        players = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying){
            if(nextTurnTimer >= 0){
                nextTurnTimer -= Time.deltaTime;
                if(nextTurnTimer <= 0){
                    bSpin.resetDreydl();
                    spinTimer = Random.Range(2,1);
                }
                
            }else{
                if(currentPlayer == 0){
                    if(Input.GetKeyDown("space")){
                        bSpin.dropIt();
                    }
                }else{
                    spinTimer -= Time.deltaTime;
                    if(spinTimer <= 0){
                        bSpin.dropIt();
                    }
                }
            }
        }
    }

    void payAnte(){
        for(int i = 0; i < 4; i++){
            players[i] -= ante;
        }
        pot += ante * 4;
    }

    void updateValues(){
        playerT.text = "you: " + players[0];
        player2T.text = "forest: " + players[1];
        player3T.text = "bugsy: " + players[2];
        player4T.text = "patrick: " + players[3];
        potT.text = "pot: " + pot;

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
            if(currentPlayer < 0){
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

    public void placeBet(int bet){
        pot = 0;
        currentPlayer = 0;
        currentPlayerT.text = "current player: you";
        nextTurnTimer = 0;
        for(int i = 0; i < 4; i++){
            players[i] = bet;
        }
        payAnte();
        updateValues();
        isPlaying = true;
    }

    public void landed(string side){
        lastSide = side;
        int half;
        int left;
        string landedLetter;
        switch (side)
        {
        case "heh":
        //half
            print("heh");
            half = (int)Mathf.Floor(pot /2);
            players[currentPlayer] += half;
            pot -= half;
            landedLetter = "heh";
            break;
        case "nun":
        //nothing
            print("nun");
            landedLetter = "nun";
            break;
        case "gimel":
        //all
            print("gimel");
            players[currentPlayer] += pot;
            pot = 0;
            landedLetter = "gimel";
            break;
        case "shin":
        //put in
            print("shin");
            players[currentPlayer] -= ante;
            pot += ante;
            landedLetter = "shin";
            break;
        case "Alef":
            print("Alef");
            //take one from player to left
             left = playerToLeft();
            players[left] --;
            players[currentPlayer] ++;
            landedLetter = "Alef";
            break;
        case "Beys":
            print("Beys");
            landedLetter = "Beys";
            //take one from every other player
            for(int i = 0; i < 4; i++){
                players[i] --;
            }
            players[currentPlayer] += 2;
            break;
        case "Daled":
            print("Daled");
            //spin again
            break;
            landedLetter = "Daled";
        case "Vov":
        //everyone puts one in pot
            print("Vov");
            for(int i = 0; i < 4; i++){
                players[i] --;
            }
            pot += 4;
            break;
            landedLetter = "Vov";
        case "Zayen":
        //half
            print("Zayen");
            //take one third of pot
             half = (int)Mathf.Floor(pot /3);
            players[currentPlayer] += half;
            pot -= half;
            landedLetter = "Zayen";
            break;
        case "Khes":
        //Return 1/2 your gelt to the pot
             half = (int)Mathf.Floor(players[currentPlayer] /2);
            players[currentPlayer] -= half;
            pot += half;
            print("Khes");
            landedLetter = "Khes";
            break;
        case "Tes":
        //Divide the pot amongst the players
            print("Tes");
             half = (int)Mathf.Floor(pot /4);
            players[currentPlayer] += half;
            pot = 0;
            landedLetter = "Tes";
            break;
        case "Yud":
        //Swap gelt with the player to your left
            print("Yud");
             left = playerToLeft();
            int leftsGelt = players[left];
            players[left] = players[currentPlayer];
            players[currentPlayer] = leftsGelt;
            landedLetter = "Yud";
            break;
        case "Khof":
        //Give one to the player to your left
            print("Khof");
             left = playerToLeft();
            players[currentPlayer] --;
            players[left] ++;
            landedLetter = "Khof";
            break;
        case "Lamed":
        //Player to your left puts one in the pot
            print("Lamed");
            landedLetter = "Lamed";
             left = playerToLeft();
            players[left] --;
            pot ++;
            break;
        case "Mem":
        //Player to your left takes one from the pot
            print("Mem");
             left = playerToLeft();
            players[left] ++;
            pot --;
            landedLetter = "Mem";
            break;
        case "Samekh":
        //All players take one from the pot (starting with player to your left)
            print("Samekh");
            landedLetter = "Samekh";
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
            skipNextPlayer = true;
            landedLetter = "Ayen";
            break;
        case "Fey":
        //Player to your left takes half the pot
             left = playerToLeft();
             half = (int)Mathf.Floor(pot /2);
            players[left] += half;
            pot -= half;
            print("Fey");
            landedLetter = "Fey";
            break;
        case "Tsadek":
        //Take one from player with most gelt  (take from multiple players if tied)
            print("Tsadek");
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
            landedLetter = "Tsadek";
            break;
        case "Kuf":
        //Split the pot with the player to your left
            print("Kuf");
             left = playerToLeft();
             half = (int)Mathf.Floor(pot /2);
            players[left] += half;
            players[currentPlayer] += half;
            pot = 0;
            landedLetter = "Kuf";
            break;
        case "Reysh":
        //Reverse turn order
            print("Reysh");
            reverseOrder = true;
            landedLetter = "Reysh";
            break;
        case "Tof":
        //Pot goes to player with the least gelt
            print("Tof");
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
            break;
        }
        if(pot == 0 || players[0] < 0){
            isPlaying = false;
            if(players[0] > 0){
                mainscore.updateScore(false, players[0]);
            }
        }else{
            
            nextTurn();
        }
        updateValues();
        // if(pot < 1){
        //     payAnte();
        // }
        
    }
}
