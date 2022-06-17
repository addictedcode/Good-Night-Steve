using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    public TMP_InputField ageInput;

    [SerializeField] private TMP_Text m_SleepTime;
    [SerializeField] private Slider m_SleepTimeSlider;
    [SerializeField] private GameObject m_SettingsPanel;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Age")) {
            m_SettingsPanel.SetActive(true);
        }
        else {
            SetSleepTimeSliderMinMax(PlayerPrefs.GetInt("Age"));
        }
    }

    public void SetPlayerAge()
    {
        if (int.TryParse(ageInput.text, out int age))
        {
            PlayerPrefs.SetInt("Age", age);
            SetSleepTimeSliderMinMax(age);
        }
    }

    public void SetSleepTimeSliderMinMax(int age)
    {
        if (age >= 6 && age <= 12) //School Age
        {
            PlayerPrefs.SetInt("MinSleep", 9);
            PlayerPrefs.SetInt("MaxSleep", 12);
            m_SleepTimeSlider.minValue = 9;
            m_SleepTimeSlider.maxValue = 12;
        }
        else if (age >= 13 && age <= 18) //Teen
        {
            PlayerPrefs.SetInt("MinSleep", 8);
            PlayerPrefs.SetInt("MaxSleep", 10);
            m_SleepTimeSlider.minValue = 8;
            m_SleepTimeSlider.maxValue = 10;
        }
        else if (age >= 18 && age <= 60) //Adult
        {
            PlayerPrefs.SetInt("MinSleep", 7);
            PlayerPrefs.SetInt("MaxSleep", 10);
            m_SleepTimeSlider.minValue = 7;
            m_SleepTimeSlider.maxValue = 10;
        }
        else if (age >= 61 && age <= 64) //Old Adult
        {
            PlayerPrefs.SetInt("MinSleep", 7);
            PlayerPrefs.SetInt("MaxSleep", 9);
            m_SleepTimeSlider.minValue = 7;
            m_SleepTimeSlider.maxValue = 9;
        }
        else if (age >= 65) //Senior
        {
            PlayerPrefs.SetInt("MinSleep", 7);
            PlayerPrefs.SetInt("MaxSleep", 8);
            m_SleepTimeSlider.minValue = 7;
            m_SleepTimeSlider.maxValue = 8;
        }
    }

    public void OnSleepTimeValueChange(float newSleepTime)
    {
        m_SleepTime.text = newSleepTime.ToString();
        PlayerPrefs.SetInt("SleepTime", (int)newSleepTime);
    }
}
