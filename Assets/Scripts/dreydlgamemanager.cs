using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class dreydlgamemanager : MonoBehaviour
{

    public dreydlScoring dreydlGame;
    public mainscore mainscore;
    public GameObject cashoutGO;
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1") || Input.GetMouseButtonDown(1))
        {
            placeBet(1);
        }
        if(Input.GetKeyDown("2") || (Input.GetKeyDown("w"))){
            placeBet(2);
        }
        if(Input.GetKeyDown("3") || (Input.GetKeyDown("a"))){
            placeBet(3);
        }
        if(Input.GetKeyDown("5") || (Input.GetKeyDown("s") )){
            placeBet(5);
        }
        if (Input.GetKeyDown("d"))
        {
            print("Max Bet");
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
        }
        if (Input.GetKeyDown("f"))
        {
            print("Deal Draw");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Cashout");
            cashOut(mainscore.getScore());
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Call Attendant");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("1 hand");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("10 hands");
        }

        if (Input.GetMouseButtonDown(0))
        {
            print("15 hands");
        }
    }

    void placeBet(int bet){
        mainscore.updateScore(true, -1 * bet);
        dreydlGame.placeBet(bet);
    }

    public async void cashOut(int amount){
        if(isActive){
            isActive = false;
            //open cashout scene
            print("cash out: " + amount);
            // string pdfFilePath = $"/Users/forest/Documents/Cash_Out_Voucher_DREYDL/{amount}.pdf";
            // PrintPDF.pdfFilePath = pdfFilePath;
            // GetComponent<PrintPDF>().Print();
            cashoutGO.SetActive(true);
            await Task.Delay(5000);
            SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
            SceneManager.UnloadScene("dreydl_spin");
        }
    }
}
