using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    public TMP_InputField ageInput;

    public void SetPlayerAge()
    {
        if (int.TryParse(ageInput.text, out int age))
        {
            PlayerPrefs.SetInt("Age", age);
        }
    }
}
