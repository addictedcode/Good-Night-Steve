using System;

using UnityEngine;
using UnityEngine.UI;

public class BackgroundTime : MonoBehaviour
{
    [SerializeField] private Image m_BG;

    [SerializeField] private Sprite m_BG_Morning;
    [SerializeField] private Sprite m_BG_Afternoon;
    [SerializeField] private Sprite m_BG_EarlyNight;
    [SerializeField] private Sprite m_BG_LateNight;

    private void Awake()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        //Change BG based on what time it is

        //Morning
        if (currentTime >= new TimeSpan(06, 00, 00) && //Hours, Minutes, Seconds
            currentTime < new TimeSpan(17, 00, 00))
        {
            m_BG.sprite = m_BG_Morning;
        }
        //Afternoon
        else if (currentTime >= new TimeSpan(17, 00, 00) &&
                currentTime < new TimeSpan(18, 00, 00))
        {
            m_BG.sprite = m_BG_Afternoon;
        }
        //Early Night
        else if (currentTime >= new TimeSpan(18, 00, 00) &&
                currentTime < new TimeSpan(20, 00, 00))
        {
            m_BG.sprite = m_BG_EarlyNight;
        }
        //Late Night
        else
        {
            m_BG.sprite = m_BG_LateNight;
        }
    }
}
