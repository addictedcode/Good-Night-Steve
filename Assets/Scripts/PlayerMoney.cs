using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    private int money = 0;
    public TMP_Text moneyText;
    public void SetMoney(int newMoney)
    {
        money = newMoney;
        moneyText.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }

    public void AdjustMoney(int delta)
    {
        money += delta;
        moneyText.text = money.ToString();
    }
}
