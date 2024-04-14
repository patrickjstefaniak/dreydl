using UnityEngine;
using System.Diagnostics;
using System;

public class PrintPDF : MonoBehaviour

{
    
    static public string pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/1.pdf";
    //this gets changed in gamecontroller.cs 

    public void Print()
    {
/*
        // Escape any double quotes in the file path
        string escapedFilePath = pdfFilePath.Replace("\"", "\\\"");

        // Construct the lp command to print the PDF
        string lpCommand = $"/usr/bin/lp -o landscape -o fit-to-page -o media=2x4in \"{escapedFilePath}\"";

        // Create a new process to run the lp command
        Process process = new Process();
        process.StartInfo.FileName = "/bin/bash"; // Terminal shell
        process.StartInfo.Arguments = $"-c \"{lpCommand}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        // Start the process
        process.Start();
        process.WaitForExit();
        process.Close();
        */


        string path = @"C:\Users\dreydl\Desktop\Cashout_Vouchers\"+pdfFilePath+".pdf";
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = path;
        //process.StartInfo.Verb = "print";

        process.Start();
        //process.WaitForExit();


        /*PowerShell ps = PowerShell.Create();
        ps.AddCommand("Start-Process")
            .AddParameter("FilePath", @"C:\Users\dreydl\Desktop\Cashout_Vouchers\{pdfFilePath}.pdf")
            .AddParameter("Verb", "Print")
            .Invoke();*/
       // "Start-Process -FilePath "C:\Users\dreydl\Desktop\Cashout_Vouchers\{pdfFilePath}.pdf" -Verb Print" 

    }
}
