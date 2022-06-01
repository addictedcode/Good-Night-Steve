using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TimeTracker : MonoBehaviour
{
    private DateTime currentTime;
    private DateTime sleepTime;
    private TimeSpan span;
    // Start is called before the first frame update
    void Start()
    {
        LoadSleepTime();
        float playerAge = PlayerPrefs.GetInt("Age");
        span = currentTime.Subtract(sleepTime);
        if (playerAge < 20)
        {
            if (span.TotalHours < 8 || span.TotalHours > 10)
                Debug.Log("Poor sleep");
            else
                Debug.Log("Good sleep");
        }
        else
        {
            Debug.Log("Dunno don't care");
        }
    }

    public void SaveSleepTime()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/Steve.dat", false);

        Debug.Log("Current Time: " + DateTime.Now);
        file.Flush();
        file.Write(DateTime.Now.ToBinary());

        file.Close();
    }

    public void LoadSleepTime()
    {
        if (File.Exists(Application.persistentDataPath + "/Steve.dat"))
        {
            StreamReader file = new StreamReader(Application.persistentDataPath + "/Steve.dat");
            
            string bin = file.ReadLine();
            sleepTime = DateTime.FromBinary(Convert.ToInt64(bin));

            file.Close();

            Debug.Log("Sleep Time: " + sleepTime);
            Debug.Log(Application.persistentDataPath);
        }
    }
}
