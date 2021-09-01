using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOn;

    [Header("Attachment")]
    public EndGamePanel endGamePanel;

    private GameTimer gameTimer;
    private ScoreSystem scoreSystem;

    private void Awake()
    {
        gameTimer = GetComponent<GameTimer>();
        scoreSystem = GetComponent<ScoreSystem>();
        gameOn = true;
    }

    public void triggerEndGame()
    {
        gameOn = false;
        Time.timeScale = 0;
        float elapsedTime = gameTimer.EndTimer();
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.DisplayEndGameScreen(scoreSystem.scorePoints, elapsedTime, scoreSystem.enemyKilled);

    }
}
