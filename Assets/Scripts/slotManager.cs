using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotManager : MonoBehaviour
{
    public reelSpin[] reels;
    public Text screenText;
    private int[] results;
    private int spinCounter = 0;
    private int winning;
    private string winWord;

    // Start is called before the first frame update
    void Start()
    {
        results = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reportResult(int id, int result){
        results[id] = result;
       // print(results[1] + " " + results[2] + " " + results[3]);
        spinCounter ++;
        if(spinCounter >= 3){
            calculateWin();
            spinCounter = 0;
            print(winning+winWord);
            screenText.text = winning + " " + winWord;
        }
    }

    void calculateWin(){
        if(results[1] == 1){
            if(results[2] == 1 && results[3] == 1){
                winning = 1000;
                winWord = "ULTIMATE JACKPOT";
            }else if(results[2] == 3 && results[3] == 2){
                winning = 33;
                winWord = "WEAR OUT";
            }else if(results[2] == 2 && results[3] == 3){
                winning = 33;
                winWord = "AROUSE";
            }
        }else if(results[1] == 2){
            if(results[2] == 2 && results[3] == 2){
                winning = 500;
                winWord = "MAJOR JACKPOT";
            }else if(results[2] == 2 && results[3] == 1){
                winning = 5;
                winWord = "RIPEN;GREEDILY ABSORB NUTRIENTS FROM THE GROUND";
            }else if(results[2] == 1 && results[3] == 3){
                winning = 33;
                winWord = "SCORN/MOCK";
            }else if(results[2] == 2 && results[3] == 3){
                winning = 34;
                winWord = "CONJOIN IN CENTRAL CORE";
            }
        }else if(results[2] == 2 && results[3] == 1){
            winning = 33;
            winWord = "LACK WHOLENESS";
        }else if(results[2] == 3 && results[3] == 1){
            winning = 61;
            winWord = "DENY;OBSTRUCT DEVELOPMENT";
        }else if(results[2] == 3 && results[3] == 2){
            winning = 62;
            winWord = "MIX NEW ELEMENTS INTO EXISTING ONES";
        }else if(results[2] == 3 && results[3] == 3){
            winning = 250;
            winWord = "MINI JACKPOT ";
        }
    }
}
