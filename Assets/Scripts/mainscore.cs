using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using FMODUnity;

public class mainscore : MonoBehaviour
{

    public Text ms;
    public Text betText;
    public Text uwin;
    public dreydlgamemanager dgm;
    public dreydlScoring ds;
    public GameObject dreydlCamera;
    //public ParticleSystem ps;
    bool slotActive;
    public List<GameObject> dreydlUI = new List<GameObject>();
    public GameObject slotFeature;
    public GameObject winText;
    public GameObject previousOne;
    public GameObject previousTwo;
    int[] pScores;
    int totalPot;
    public Text p1;
    public Text p2;
    public Text p3;
    public Text p4;
    public Text tp;

    int bet;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 18; 
        //var emission = ps.emission;
        //emission.rate = 0;
        pScores = new int[4];
        slotActive = false;
        previousOne.SetActive(true);
        previousTwo.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z")){
            score = 0;
        }
        if(Input.GetKeyDown("h")){
            score = 101;
        }
    }

    public bool checkBet(int bet){
        if(score - bet < 0){
            return false;
        }
        return true;
    }

    public int getScore(){
        return score;
    }

    public int getBet(){
        return bet;
    }

    public void scoreUiUpdate(int pot, int[] ps){
        totalPot += pot;
        pScores[0] += ps[0];
        pScores[1] += ps[1];
        pScores[2] += ps[2];
        pScores[3] += ps[3];
    }

//updates total score ui at top of screen
    public void updateScoreBoard(){
        resetScoreCount();

        foreach (dreydlScoring ds in FindObjectsOfType<dreydlScoring>())
        {
            ds.sendScoreUi();
        }

        p1.text = "" +pScores[0];
        p2.text = "" +pScores[1];
        p3.text = "" +pScores[2];
        p4.text = "" +pScores[3];
        tp.text = "" +totalPot;
        resetScoreCount();
    }

    public void resetScoreCount(){
        totalPot = 0;
        pScores[0] = 0;
        pScores[1] = 0;
        pScores[2] = 0;
        pScores[3] = 0;
    }



    public async void updateScore(bool isBet, int change){
        score += change;
        ms.text = ""+ score;
        if(isBet){
            winText.SetActive(false);
            betText.text = "" + change;
            bet = change;
            previousOne.SetActive(true);
            previousTwo.SetActive(true);


            // hand is over           
        }
        else{
            if(change > 0){
                //await Task.Delay(1000);
                
                uwin.text = "You win: " + change;
                previousOne.SetActive(false);
                previousTwo.SetActive(false);
                winText.SetActive(true);
                if(change > 10){
                    coinBust(change * 2, 5000);
                }else{
                    coinBust(change, 2000);
                }
                
                if (change >= 1)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/winCoins");
                }


                await Task.Delay(2000);

                previousOne.SetActive(true);
                previousTwo.SetActive(true);
                winText.SetActive(false);

            }
        }
        if(score <= 0){
            dgm.cashOut(0);
        }
    }


//i dont think these are used anymore
    public async void maybeStartSlot(){
        if(Random.Range(0,100) > 100){
            slotActive = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/slotFadeIn");
            //activate slot machine
            await Task.Delay(4500);
                slotFeature.SetActive(true);
            await Task.Delay(3000);
                slotFeature.SetActive(false);
                 dgm.activateSlot();
                dreydlCamera.SetActive(false);
                ds.setSlotMode(true);
            for (int i = 0; i < dreydlUI.Count; i++) {
                dreydlUI[i].SetActive(false);
            }
        }
    }

    public async void endSlot(){
        await Task.Delay(2000);
        ds.setSlotMode(false);
        dreydlCamera.SetActive(true);
        slotActive = false;
        dgm.startNextBet();
        for (int i = 0; i < dreydlUI.Count; i++)
        {
            dreydlUI[i].SetActive(true);
        }
    }

    public async void coinBust(float win, int time){
        GameObject[] pss = GameObject.FindGameObjectsWithTag("goldbust");
        print("coinnn");
        foreach(GameObject ps in pss){
            var emission = ps.GetComponent<ParticleSystem>().emission;
            emission.rate = win;
        }
        await Task.Delay(time);
        foreach(GameObject ps in pss){
            var emission = ps.GetComponent<ParticleSystem>().emission;
            emission.rate = 0;
        }
    }
}
