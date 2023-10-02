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
    public GameObject dreydlCamera;
    public ParticleSystem ps;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 18; 
        var emission = ps.emission;
        emission.rate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getScore(){
        return score;
    }

    public void updateScore(bool isBet, int change){
        score += change;
        ms.text = "score: " + score;
        if(isBet){
            uwin.text = "you bet: " + change;
        }else{
            uwin.text = "you win: " + change;
            //if(Random.Range(0,100) < 50){
                //activate slot machine
               // dgm.activateSlot();
                //dreydlCamera.SetActive(false);
                coinBust(change, 2000);
                if (change >= 1)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/winCoins");
            }
            //}
        }
        if(score <= 0){
            dgm.cashOut(0);
        }
    }

    public async void coinBust(float win, int time){
        var emission = ps.emission;
        emission.rate = win;
        await Task.Delay(time);
        emission.rate = 0;
    }
}
