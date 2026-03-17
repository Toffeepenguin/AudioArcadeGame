using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JBA_Money : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text healthText;

    public void Setup(int money, int health)
    {
        gameObject.SetActive(true);
        moneyText.text = "Money : " + money.ToString();
        gameObject.SetActive(true);
        healthText.text = "Health : " + health.ToString();
    }
}
