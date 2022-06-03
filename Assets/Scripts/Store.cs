using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public PlayerMoney playerMoney;
    public Button oneCost;
    public Button threeCost;
    public Button sevenCost;

    public void OnEnable()
    {
        int money = playerMoney.GetMoney();
        if (money >= 1)
        {
            oneCost.interactable = true;
            if (money >= 3)
            {
                threeCost.interactable = true;
                if (money >= 7)
                    sevenCost.interactable = true;
                else
                    sevenCost.interactable = false;
            }
            else
            {
                threeCost.interactable = false;
                sevenCost.interactable = false;
            }
        }
        else
        {
            oneCost.interactable = false;
            threeCost.interactable = false;
            sevenCost.interactable = false;
        }

    }
}
