using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class slotManager : MonoBehaviour
{
    public reelSpin[] reels;
    public Text screenText;
    private int[] results;
    private int spinCounter = 0;
    private int winning;
    private string winWord;
    bool isPlaying;
    FMODUnity.StudioEventEmitter eventEmitterRef;

    // Start is called before the first frame update
    void Start()
    {
        results = new int[4];
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
      
        bool isPlaying = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {

        if (isPlaying == false)
        {
            eventEmitterRef.Play();
            isPlaying = true;
        }
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

        eventEmitterRef.Stop();
        isPlaying = false;
        if (results[1] == 1){
            if(results[2] == 1 && results[3] == 1){
                winning = 1000;
                winWord = "ULTIMATE JACKPOT";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
            else if(results[2] == 3 && results[3] == 2){
                winning = 33;
                winWord = "WEAR OUT";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
            else if(results[2] == 2 && results[3] == 3){
                winning = 33;
                winWord = "AROUSE";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
        }else if(results[1] == 2){
            if(results[2] == 2 && results[3] == 2){
                winning = 500;
                winWord = "MAJOR JACKPOT";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
            else if(results[2] == 2 && results[3] == 1){
                winning = 5;
                winWord = "RIPEN;GREEDILY ABSORB NUTRIENTS FROM THE GROUND";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
            else if(results[2] == 1 && results[3] == 3){
                winning = 33;
                winWord = "SCORN/MOCK";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
            else if(results[2] == 2 && results[3] == 3){
                winning = 34;
                winWord = "CONJOIN IN CENTRAL CORE";
                FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
            }
        }else if(results[2] == 2 && results[3] == 1){
            winning = 33;
            winWord = "LACK WHOLENESS";
            FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
        }
        else if(results[2] == 3 && results[3] == 1){
            winning = 61;
            winWord = "DENY;OBSTRUCT DEVELOPMENT";
            FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
        }
        else if(results[2] == 3 && results[3] == 2){
            winning = 62;
            winWord = "MIX NEW ELEMENTS INTO EXISTING ONES";
            FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
        }
        else if(results[2] == 3 && results[3] == 3){
            winning = 250;
            winWord = "MINI JACKPOT ";
            FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin");
        }
    }
}
