using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EndGamePanel : MonoBehaviour
{

    [Header("Attachment")]
    public TextMeshProUGUI bestScoreTxt;
    public GameObject newHighScoreObj;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI timeElapsedTxt;
    public TextMeshProUGUI enemyKilledTxt;
    private void Start()
    {
        newHighScoreObj.SetActive(false);
        if (GameManager.gameOn)
        {
            gameObject.SetActive(false);
            return;
        }
        newHighScoreObj.SetActive(false);
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
    }
    public void onPressedReplay()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void DisplayEndGameScreen(int score, float timeElapsed, int enemyKilled)
    {
        int bestScore = PlayerPrefs.GetInt("highscore");
        if (score>bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("highscore", bestScore);
            newHighScoreObj.SetActive(true);
        }

        //DISPLAY
        bestScoreTxt.text = bestScore.ToString();
        scoreTxt.text = score.ToString();

        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeElapsedTxt.text = string.Format("Time Elapsed: {0} minutes {1} seconds",minutes,seconds);
        enemyKilledTxt.text = "Enemy Killed: " + enemyKilled.ToString();
    }

    public void onPressedMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }
}
