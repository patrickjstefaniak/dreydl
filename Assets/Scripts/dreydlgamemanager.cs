using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dreydlgamemanager : MonoBehaviour
{

    public dreydlScoring dreydlGame;
    public mainscore mainscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1") || Input.GetMouseButtonDown(1))
        {
            placeBet(1);
        }
        if(Input.GetKeyDown("2") || (Input.GetKeyDown("w"))){
            placeBet(2);
        }
        if(Input.GetKeyDown("3") || (Input.GetKeyDown("a"))){
            placeBet(3);
        }
        if(Input.GetKeyDown("5") || (Input.GetKeyDown("s") )){
            placeBet(5);
        }
        if (Input.GetKeyDown("d"))
        {
            print("Max Bet");
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Cashout");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Call Attendant");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("1 hand");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("10 hands");
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("15 hands");
        }
    }

    void placeBet(int bet){
        mainscore.updateScore(true, -1 * bet);
        dreydlGame.placeBet(bet);
    }
}
