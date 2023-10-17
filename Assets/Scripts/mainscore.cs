using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using FMODUnity;

public class mainscore : MonoBehaviour
{

    public Text ms;
    public Text uwin;
    public dreydlgamemanager dgm;
    public dreydlScoring ds;
    public GameObject dreydlCamera;
    public ParticleSystem ps;
    GameObject placeBetText;
    bool slotActive;
    public List<GameObject> dreydlUI = new List<GameObject>();
    public GameObject slotFeature;

    int bet;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 18; 
        var emission = ps.emission;
        emission.rate = 0;
        placeBetText = GameObject.Find("placebetflashing");
        slotActive = false;
       
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
        ms.text = "score: " + score;
        if(isBet){
            uwin.text = "you bet: " + change;
            bet = change;
            
                placeBetText.SetActive(false);
            
        }else{
            uwin.text = "you win: " + change;
            if (slotActive == false)
            {
                placeBetText.SetActive(true);
            }
            coinBust(change, 2000);
            if (change >= 1)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/winCoins");
            }
            if (slotActive == false)
            {
                placeBetText.SetActive(true);
            }
            //}
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
