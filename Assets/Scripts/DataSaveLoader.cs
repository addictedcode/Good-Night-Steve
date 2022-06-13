using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class GameData
{
    public DateTime sleepTime;
    public DateTime previousWakeTime;
    public bool isAsleep;
    public int winStreak;
    public int money;
    public float xSize;
    public float ySize;
}

public class DataSaveLoader : MonoBehaviour
{
    private DateTime wakeUpTime;
    private TimeSpan sleepSpan;

    private DateTime sleepTime;
    private bool fellAsleep = false;
    private int winstreak = 0;
    public PlayerMoney playerMoney;
    public GameObject Steve;

    [SerializeField] private NotificationManager m_notificationManager;

    void Awake()
    {
        LoadSleepTime();

        if (fellAsleep)
        {
            wakeUpTime = DateTime.Now;
            float playerAge = PlayerPrefs.GetInt("Age");
            sleepSpan = wakeUpTime.Subtract(sleepTime);
            if (playerAge < 20)
            {
                if (sleepSpan.TotalHours < 8 || sleepSpan.TotalHours > 10)
                {
                    Debug.Log("Poor sleep");
                    winstreak = 0;
                    Steve.GetComponent<Steve>().setStressed(true);
                }
                else
                {
                    Debug.Log("Good sleep");
                    winstreak++;
                    playerMoney.AdjustMoney(Mathf.Min(winstreak, 7));
                    Steve.GetComponent<Steve>().setStressed(false);
                }
            }
            else
            {
                Debug.Log("Dunno don't care");
            }
        }
        else
        {
            Debug.Log("Steve couldn't fall asleep");
        }
        Steve.GetComponent<Steve>().WakeSteve(wakeUpTime);
    }

    public void SaveSleepTime()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/Steve.dat", FileMode.Create);

        GameData data = new GameData();
        data.sleepTime = DateTime.Now;
        data.previousWakeTime = wakeUpTime;
        //don't sleep if awake for less than 8 hrs and didn't lack sleep
        if (DateTime.Now.Subtract(wakeUpTime).TotalHours <= 8 && sleepSpan.TotalHours >= 7)
            data.isAsleep = false;
        else
            data.isAsleep = true;
        data.winStreak = winstreak;
        data.money = playerMoney.GetMoney();
        data.xSize = Steve.transform.localScale.x;
        data.ySize = Steve.transform.localScale.y;

        formatter.Serialize(file, data);

        file.Close();

        Steve.GetComponent<Steve>().SleepSteve();
        m_notificationManager.SendNotification("Steve", "Time to wake up!", PlayerPrefs.GetInt("SleepTime"));
    }

    public void LoadSleepTime()
    {
        if (File.Exists(Application.persistentDataPath + "/Steve.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(Application.persistentDataPath + "/Steve.dat", FileMode.Open);

            GameData data = formatter.Deserialize(file) as GameData;
            //sleep time
            sleepTime = data.sleepTime;
            //past wake up time in case didn't fall asleep
            wakeUpTime = data.previousWakeTime;
            //fell asleep, doesn't sleep if goodnight happens before spending 8 hrs awake
            fellAsleep = data.isAsleep;
            //streak
            winstreak = data.winStreak;
            //money
            playerMoney.SetMoney(data.money);
            //size
            Steve.transform.localScale = new Vector3(data.xSize, data.ySize, 1);

            file.Close();

            Debug.Log("Sleep Time: " + sleepTime);
            Debug.Log(Application.persistentDataPath);
        }
    }
}
