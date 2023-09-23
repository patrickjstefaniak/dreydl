﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainscore : MonoBehaviour
{

    public Text ms;
    public Text uwin;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 18; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(bool isBet, int change){
        score += change;
        ms.text = "score: " + score;
        if(isBet){
            uwin.text = "you bet: " + change;
        }else{
            uwin.text = "you win: " + change;
        }
    }
}