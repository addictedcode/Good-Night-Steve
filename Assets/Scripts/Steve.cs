using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Steve : MonoBehaviour
{
    private DateTime wakeUpTime;
    private bool isStressed = false;

    private void Update()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan awakeTime = currentTime.Subtract(wakeUpTime);

        float playerAge = PlayerPrefs.GetInt("Age");

        if (playerAge < 20)
        {
            if (awakeTime.TotalHours >= 20)
            {
                Debug.Log("Steve is lacking sleep");
                setStressed(true);
            }
        }
    }

    public void WakeSteve(DateTime wakeUp)
    {
        wakeUpTime = wakeUp;
    }

    public void setStressed(bool stressed)
    {
        isStressed = stressed;
    }

    public void Feed(int foodType)
    {
        float wideIncrease = 0;
        switch(foodType)
        {
            case 1://free
                wideIncrease = 0.01f;
                break;
            case 2://cheap
                wideIncrease = 0.03f;
                break;
            case 3://medium
                wideIncrease = 0.1f;
                break;
            case 4://expensive
                wideIncrease = 0.35f;
                break;
        }
        Vector3 newSize = gameObject.transform.localScale;
        newSize.x += wideIncrease;
        gameObject.transform.localScale = newSize;
    }
    
    public void Play()
    {

    }
    
    public void Drink(int drinkType)
    {
        float tallIncrease = 0;
        switch (drinkType)
        {
            case 1://free
                tallIncrease = 0.01f;
                break;
            case 2://cheap
                tallIncrease = 0.03f;
                break;
            case 3://medium
                tallIncrease = 0.1f;
                break;
            case 4://expensive
                tallIncrease = 0.35f;
                break;
        }
        Vector3 newSize = gameObject.transform.localScale;
        newSize.y += tallIncrease;
        gameObject.transform.localScale = newSize;
    }
}
