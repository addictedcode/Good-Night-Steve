using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int money = 0;
    public void SetMoney(int newMoney)
    {
        money = newMoney;
    }

    public int GetMoney()
    {
        return money;
    }

    public void AdjustMoney(int delta)
    {
        money += delta;
    }
}
