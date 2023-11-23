using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text deathResultText;
    [SerializeField] private TMP_Text buttonResultText;
    [SerializeField] private TMP_Text coinsText;

    public void ShowDeathScreen(bool isDeath, int coins)
    {
        gameObject.SetActive(true);
        
        if (isDeath)
        {
            deathResultText.text = "LOSE";
            buttonResultText.text = "TRY AGAIN";
        }
        else
        {
            deathResultText.text = "WIN";
            buttonResultText.text = "PROCEED";
        }

        coinsText.text = "+" + coins;
    }

    public void HideDeathScreen()
    {
        gameObject.SetActive(false);
    }
}
