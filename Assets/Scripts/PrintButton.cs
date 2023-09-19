using UnityEngine;
public class PrintButton : MonoBehaviour
{
    public PrintPDF printPDF;

    public void OnClick()
    {
        printPDF.Print();
        print("printing");
    }
}
