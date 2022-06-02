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
    public int winStreak;
    public int money;
    public float xSize;
    public float ySize;
}

public class DataSaveLoader : MonoBehaviour
{
    private DateTime currentTime;
    private TimeSpan span;

    private DateTime sleepTime;
    private int winstreak = 0;
    public PlayerMoney playerMoney;
    public GameObject Steve;
    // Start is called before the first frame update
    void Start()
    {
        LoadSleepTime();

        float playerAge = PlayerPrefs.GetInt("Age");
        span = currentTime.Subtract(sleepTime);
        if (playerAge < 20)
        {
            if (span.TotalHours < 8 || span.TotalHours > 10)
            {
                Debug.Log("Poor sleep");
                winstreak = 0;
            }
            else
            {
                Debug.Log("Good sleep");
                winstreak++;
                playerMoney.AdjustMoney(Mathf.Min(winstreak, 7));
            }
        }
        else
        {
            Debug.Log("Dunno don't care");
        }
    }

    public void SaveSleepTime()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/Steve.dat", FileMode.Create);

        GameData data = new GameData();
        data.sleepTime = DateTime.Now;
        data.winStreak = winstreak;
        data.money = playerMoney.GetMoney();
        data.xSize = Steve.transform.localScale.x;
        data.ySize = Steve.transform.localScale.y;

        formatter.Serialize(file, data);

        file.Close();
    }

    public void LoadSleepTime()
    {
        if (File.Exists(Application.persistentDataPath + "/Steve.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(Application.persistentDataPath + "/Steve.dat", FileMode.Open);

            GameData data = formatter.Deserialize(file) as GameData;
            //time
            sleepTime = data.sleepTime;
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
