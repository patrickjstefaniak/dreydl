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
        if (guess == 1)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/1.pdf";


        }
        if (guess == 2)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/2.pdf";

        }
        if (guess == 3)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/3.pdf";

        }
        if (guess == 4)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/4.pdf";

        }
        if (guess == 5)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/5.pdf";

        }
        if (guess == 6)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/6.pdf";

        }
        if (guess == 7)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/7.pdf";

        }
        if (guess == 8)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/8.pdf";

        }
        if (guess == 9)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/9.pdf";

        }
        if (guess == 10)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/10.pdf";

        }
        if (guess == 11)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/11.pdf";

        }
        if (guess == 12)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/12.pdf";

        }
        if (guess == 13)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/13.pdf";

        }
        if (guess == 14)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/14.pdf";

        }
        if (guess == 15)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/15.pdf";

        }
        if (guess == 16)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/16.pdf";

        }
        if (guess == 17)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/17.pdf";

        }
        if (guess == 18)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/18.pdf";

        }
        if (guess == 19)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/19.pdf";

        }
        if (guess == 20)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/20.pdf";

        }
        if (guess == 21)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/21.pdf";

        }
        if (guess == 22)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/22.pdf";

        }
        if (guess == 23)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/23.pdf";

        }
        if (guess == 24)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/24.pdf";

        }
        if (guess == 25)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/25.pdf";

        }
        if (guess == 26)
        {
            PrintPDF.pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/26.pdf";

        }


    }



}
