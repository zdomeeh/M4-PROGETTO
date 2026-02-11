using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ui_Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        _timerText.text = "Time: " + seconds;
    }
}
