using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    [SerializeField]
    GameObject popUpUI;
    [SerializeField]
    TextMeshProUGUI popUpText;    
    [SerializeField]
    float popupTime;

    string[] popUpsDom = new string[11];
    string[] popUpsClose = new string[3];
    string[] popUpsComeback = new string[3];

    private void Start()
    {
        PopUpInitialiser();
    }

    public void PopUpDom()
    {
        StartCoroutine(PopUp("DOM"));
    }

    public void PopUpClose()
    {
        StartCoroutine(PopUp("CLOSE"));
    }
    
    public void PopUpComeback()
    {
        StartCoroutine(PopUp("COMEBACK"));
    }

    IEnumerator PopUp(string type)
    {
        popUpUI.SetActive(true);
        switch (type)
        {
            case "DOM":
                popUpText.text = popUpsDom[Random.Range(0, popUpsDom.Length)];
                break;
            case "CLOSE":
                popUpText.text = popUpsClose[Random.Range(0, popUpsClose.Length)];
                break;
            case "COMEBACK":
                popUpText.text = popUpsComeback[Random.Range(0, popUpsComeback.Length)];
                break;
        }        
        yield return new WaitForSecondsRealtime(3f);
        popUpUI.SetActive(false);
    }

    void PopUpInitialiser()
    {
        popUpsDom[0] = "DEMOLISHMENT";
        popUpsDom[1] = "OBLITERATION";
        popUpsDom[2] = "DOMINATION";
        popUpsDom[3] = "ANNIHILATION";
        popUpsDom[4] = "EXTERMINATION";
        popUpsDom[5] = "ERADICATION";
        popUpsDom[6] = "DECIMATION";
        popUpsDom[7] = "MASSACRE";
        popUpsDom[8] = "SLAUGHTER";
        popUpsDom[9] = "LOPSIDED";
        popUpsDom[10] = "Are you even trying?";

        popUpsClose[0] = "TIGHT GAME";
        popUpsClose[1] = "TIE-BREAKER";
        popUpsClose[2] = "CLOSE MATCH";

        popUpsComeback[0] = "COMEBACK!";
        popUpsComeback[1] = "AGAINST ALL ODDS";
        popUpsComeback[2] = "FUMBLE";
    }
}
