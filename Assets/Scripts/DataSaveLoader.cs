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
    public bool isStressed;
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
    [SerializeField] private GameObject m_GoodnightButton;
    [SerializeField] private GameObject m_WakeupButton;

    private void Awake() {
        LoadSleepTime();
        if (fellAsleep) {
            Steve.GetComponent<Steve>().SleepSteve();
            m_GoodnightButton.SetActive(false);
            m_WakeupButton.SetActive(true);
        }
    }

    public void WakeUpSteve() {
        if (fellAsleep) {
            wakeUpTime = DateTime.Now;
            float playerAge = PlayerPrefs.GetInt("Age");
            sleepSpan = wakeUpTime.Subtract(sleepTime);
            m_notificationManager.SendNotification("Steve", "Time to say goodnight to Steve!", 24 - (int)sleepSpan.TotalHours);

            if (sleepSpan.TotalHours < PlayerPrefs.GetInt("MinSleep") || sleepSpan.TotalHours > PlayerPrefs.GetInt("MaxSleep")) {
                Debug.Log("Poor sleep");
                winstreak = 0;
                Steve.GetComponent<Steve>().setStressed(true);
            }
            else {
                Debug.Log("Good sleep");
                winstreak++;
                playerMoney.AdjustMoney(Mathf.Min(winstreak, 7));
                Steve.GetComponent<Steve>().setStressed(false);
            }
        }
        else {
            Debug.Log("Steve couldn't fall asleep");
        }
        Steve.GetComponent<Steve>().WakeSteve(wakeUpTime);
        Save();
    }

    public void WakeUpSteveForceGood()
    {
        if (fellAsleep)
        {
            Debug.Log("Good sleep");
            winstreak++;
            playerMoney.AdjustMoney(Mathf.Min(winstreak, 7));
            Steve.GetComponent<Steve>().setStressed(false);
        }
        Steve.GetComponent<Steve>().WakeSteve(wakeUpTime);
        Save();
    }

    public void WakeUpSteveForceBad()
    {
        if (fellAsleep)
        {
            Debug.Log("Poor sleep");
            winstreak = 0;
            Steve.GetComponent<Steve>().setStressed(true);
        }
        Steve.GetComponent<Steve>().WakeSteve(wakeUpTime);
        Save();
    }

    public void Save() {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/Steve.dat", FileMode.Create);

        GameData data = new GameData();
        data.sleepTime = DateTime.Now;
        data.previousWakeTime = wakeUpTime;
        //don't sleep if awake for less than 8 hrs and didn't lack sleep
        data.isAsleep = false;
        data.winStreak = winstreak;
        data.money = playerMoney.GetMoney();
        data.xSize = Steve.transform.localScale.x;
        data.ySize = Steve.transform.localScale.y;

        formatter.Serialize(file, data);

        file.Close();
    }

    public void SaveSleepTime()
    {
        fellAsleep = true;

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
        data.isStressed = Steve.GetComponent<Steve>().isStressed;

        formatter.Serialize(file, data);

        file.Close();

        Steve.GetComponent<Steve>().SleepSteve();
        //m_notificationManager.SendNotification("Steve", "Time to say goodmorning to Steve!", PlayerPrefs.GetInt("SleepTime"));
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
            Steve.GetComponent<Steve>().setStressed(data.isStressed);

            file.Close();

            Debug.Log("Sleep Time: " + sleepTime);
            Debug.Log(Application.persistentDataPath);
        }
    }
}
