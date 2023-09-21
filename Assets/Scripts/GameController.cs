using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class GameController : MonoBehaviour
{
    
    private InputField input;

    void Awake()
    {

        input = GameObject.Find("InputField").GetComponent<InputField>();
    }
   
   public void GetInput (string printNumber)
    {
        CompareGuesses(int.Parse(printNumber));
        input.text = "";
    }

    void CompareGuesses(int guess)
    {
        if (guess >= 1 && guess <= 100)
        {
            string pdfFilePath = $"/Users/forest/Documents/Cash_Out_Voucher_DREYDL/{guess}.pdf";
            PrintPDF.pdfFilePath = pdfFilePath;
        }






    }



}
