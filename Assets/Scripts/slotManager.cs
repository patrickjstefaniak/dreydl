using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class slotManager : MonoBehaviour
{
    public reelSpin[] reels;
    public Text screenText;
    private int[] results;
    private int spinCounter = 0;
    private int winning;
    private string winWord;
    private int numOfSpins;
    private mainscore ms;
    private dreydlScoring ds;
    public GameObject mc;
    bool isPlaying;
    FMODUnity.StudioEventEmitter eventEmitterRef;
    FMODUnity.StudioEventEmitter songEmitterRef;

    // Start is called before the first frame update
    void Start()
    {
        results = new int[4];
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
        songEmitterRef = mc.GetComponent<FMODUnity.StudioEventEmitter>();
        FMODUnity.RuntimeManager.PlayOneShot("event:/slotFadeIn");


        ms = GameObject.Find("main score").GetComponent<mainscore>();
        ds = GameObject.Find("scores").GetComponent<dreydlScoring>();

        bool isPlaying = false;
        winning = 0;
        int bet = ms.getBet();
        if (bet == -5)
        {
            numOfSpins = 3;
        }
        else if (bet <= -2)
        {
            numOfSpins = 2;
        }
        else
        {
            numOfSpins = 1;
        }
        
        songEmitterRef.Play();
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

    public void reportResult(int id, int result)
    {

        results[id] = result;
        // print(results[1] + " " + results[2] + " " + results[3]);
        spinCounter++;
        if (spinCounter >= 3)
        {
            calculateWin();
            spinCounter = 0;
            print(winning + winWord);
            screenText.text = winning + " " + winWord;
            numOfSpins--;
            if (winning > 0)
            {
                ms.updateScore(false, winning);
                numOfSpins = 0;
            }
            if (numOfSpins <= 0)
            {
                endSlot();
            }
        }
    }

    async void endSlot()
    {
        songEmitterRef.Stop();

        if (winning > 0)
        {
            await Task.Delay(9000);
        }
        else
        {
            await Task.Delay(2000);
        }
        ms.endSlot();
        SceneManager.UnloadScene("slotmachine");
    }

    void playWinMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/reelWin", Vector3.zero);
        print("play win");
        songEmitterRef.Stop();
    }

    void calculateWin()
    {

        eventEmitterRef.Stop();
        isPlaying = false;
        if (results[1] == 1)
        {
            if (results[2] == 1 && results[3] == 1)
            {
                winning = 75;
                winWord = "ULTIMATE JACKPOT";
                playWinMusic();
            }
            else if (results[2] == 3 && results[3] == 2)
            {
                winning = 33;
                winWord = "WEAR OUT";
                playWinMusic();
            }
            else if (results[2] == 2 && results[3] == 3)
            {
                winning = 33;
                winWord = "AROUSE";
                playWinMusic();
            }
        }
        else if (results[1] == 2)
        {
            if (results[2] == 2 && results[3] == 2)
            {
                winning = 50;
                winWord = "MAJOR JACKPOT";
                playWinMusic();
            }
            else if (results[2] == 2 && results[3] == 1)
            {
                winning = 5;
                winWord = "RIPEN;GREEDILY ABSORB NUTRIENTS FROM THE GROUND";
                playWinMusic();
            }
            else if (results[2] == 1 && results[3] == 3)
            {
                winning = 33;
                winWord = "SCORN/MOCK";
                playWinMusic();
            }
            else if (results[2] == 2 && results[3] == 3)
            {
                winning = 34;
                winWord = "CONJOIN IN CENTRAL CORE";
                playWinMusic();
            }
        }
        else if (results[2] == 2 && results[3] == 1)
        {
            winning = 33;
            winWord = "LACK WHOLENESS";
            playWinMusic();
        }
        else if (results[2] == 3 && results[3] == 1)
        {
            winning = 61;
            winWord = "DENY;OBSTRUCT DEVELOPMENT";
            playWinMusic();
        }
        else if (results[2] == 3 && results[3] == 2)
        {
            winning = 62;
            winWord = "MIX NEW ELEMENTS INTO EXISTING ONES";
            playWinMusic();
        }
        else if (results[2] == 3 && results[3] == 3)
        {
            winning = 25;
            winWord = "MINI JACKPOT ";
            playWinMusic();
        }
        else
        {
            foreach (reelSpin r in reels)
            {
                r.canSpinAgain();
            }
        }
    }
}
