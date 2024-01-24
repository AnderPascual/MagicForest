using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;

    private static float timeElapsed;
    private int minutes, seconds, cents;

    public void UpdateTimer()
    {
        if (timerText != null)
        {
            timeElapsed += Time.deltaTime;
            minutes = (int)(timeElapsed / 60F);
            seconds = (int)(timeElapsed - minutes * 60f);
            cents = (int)((timeElapsed - (int)timeElapsed) * 100f);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
        }
    }

    public void RestartTimer()
    {
        timeElapsed = 0f;
    }
}


