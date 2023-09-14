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
        if(Input.GetKeyDown("1")){
            placeBet(1);
        }
        if(Input.GetKeyDown("2")){
            placeBet(2);
        }
        if(Input.GetKeyDown("3")){
            placeBet(3);
        }
        if(Input.GetKeyDown("5")){
            placeBet(5);
        }
    }

    void placeBet(int bet){
        mainscore.updateScore(true, -1 * bet);
        dreydlGame.placeBet(bet);
    }
}
