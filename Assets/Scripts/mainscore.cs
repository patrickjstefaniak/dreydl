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
    public ParticleSystem ps;
    bool slotActive;
    public List<GameObject> dreydlUI = new List<GameObject>();
    public GameObject slotFeature;
    public GameObject winText;
    public GameObject previousOne;
    public GameObject previousTwo;


    int bet;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 18; 
        var emission = ps.emission;
        emission.rate = 0;
        
        slotActive = false;
        previousOne.SetActive(true);
        previousTwo.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getScore(){
        return score;
    }

    public int getBet(){
        return bet;
    }

  

    public void updateScore(bool isBet, int change){
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
                coinBust(change, 2000);
                if (change >= 1)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/winCoins");
                }
                
            }
        }
        if(score <= 0){
            dgm.cashOut(0);
        }
    }

    public async void maybeStartSlot(){
        if(Random.Range(0,100) > 100){
            slotActive = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/slotFunction");
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
        var emission = ps.emission;
        emission.rate = win;
        await Task.Delay(time);
        emission.rate = 0;
    }
}
