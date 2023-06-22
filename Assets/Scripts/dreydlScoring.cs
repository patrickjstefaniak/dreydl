using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dreydlScoring : MonoBehaviour
{
    int pot = 0;
    int currentPlayer = 0;
    int[] players;
    int ante = 5;
    string lastSide;
    public Text playerT;
    public Text player2T;
    public Text player3T;
    public Text player4T;
    public Text potT;
    public Text sideT;
    // Start is called before the first frame update
    void Start()
    {
        players = new int[4];
        for(int i = 0; i < 4; i++){
            players[i] = 25;
        }
        payAnte();
        updateValues();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        currentPlayer += 1;
        if(currentPlayer >= 4){
            currentPlayer = 0;
        }
    }

    //nothing
    //take half of pot
    //get the whole pot
    //put in 5

    //ante 5

    public void landed(string side){
        lastSide = side;
        switch (side)
        {
        case "heh":
        //half
            print("heh");
            int half = (int)Mathf.Floor(pot /2);
            players[currentPlayer] += half;
            pot -= half;
            break;
        case "nun":
        //nothing
            print("nun");
            break;
        case "gimel":
        //all
            print("gimel");
            players[currentPlayer] += pot;
            pot = 0;
            break;
        case "shin":
        //put in
            print("shin");
            players[currentPlayer] -= ante;
            pot += ante;
            break;
        default:
            break;
        }
        if(pot <= 1){
            payAnte();
        }
        updateValues();
        nextTurn();
    }
}
