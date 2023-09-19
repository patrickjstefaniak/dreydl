using UnityEngine;
using System.Diagnostics;

public class PrintPDF : MonoBehaviour

{
    
    static public string pdfFilePath = "/Users/forest/Documents/Cash_Out_Voucher_DREYDL/1.pdf";

    public void Print()
    {
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
    }
}
