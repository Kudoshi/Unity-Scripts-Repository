using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private timerMode currentMode = timerMode.idle;
    [Header("View Only")]
    public float currentTime;
    public enum timerMode {idle,graceCountDown, timer};

    public void StartTimer(float time, string timerMode)
    {
        if (timerMode == "graceCountDown")
        {
            currentTime = time;
            currentMode = GameTimer.timerMode.graceCountDown;
        }
        else if (timerMode == "timer")
        {
            currentTime = 0;
            currentMode = GameTimer.timerMode.timer;
        }
    }

    private void Update()
    {
        if (currentMode == timerMode.graceCountDown)
        {
            if (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
                DisplayTime(currentTime);
            }
            else
            {
                currentTime = 0;
                DisplayTime(currentTime);
                currentMode = timerMode.idle;
            }
        }
        else if (currentMode == timerMode.timer && GameManager.gameOn)
        {
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }
    }

    private void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    public float EndTimer()
    {
        if (currentMode != timerMode.timer)
        {
            return 0;
        }
        else
        {
            currentMode = timerMode.idle;
            return currentTime;
        }
        
    }
}