using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TimeTracker : MonoBehaviour
{
    private DateTime currentTime;
    private TimeSpan span;
    // Start is called before the first frame update
    void Start()
    {
        LoadSleepTime();
    }

    static public void SaveSleepTime()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/Steve.dat", false);

        Debug.Log("Current Time: " + DateTime.Now);
        file.Flush();
        file.Write(DateTime.Now.ToBinary());

        file.Close();
    }

    static public void LoadSleepTime()
    {
        if (File.Exists(Application.persistentDataPath + "/Steve.dat"))
        {
            StreamReader file = new StreamReader(Application.persistentDataPath + "/Steve.dat");
            
            string bin = file.ReadLine();
            DateTime sleepTime = DateTime.FromBinary(Convert.ToInt64(bin));

            file.Close();

            Debug.Log("Sleep Time: " + sleepTime);
            Debug.Log(Application.persistentDataPath);
        }
    }
}
