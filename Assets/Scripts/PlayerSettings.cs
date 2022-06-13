using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    public TMP_InputField ageInput;

    [SerializeField] private TMP_Text m_SleepTime;

    public void SetPlayerAge()
    {
        if (int.TryParse(ageInput.text, out int age))
        {
            PlayerPrefs.SetInt("Age", age);
        }
    }

    public void OnSleepTimeValueChange(float newSleepTime)
    {
        m_SleepTime.text = newSleepTime.ToString();
        PlayerPrefs.SetInt("SleepTime", (int)newSleepTime);
    }
}
