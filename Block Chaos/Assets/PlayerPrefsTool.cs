using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerPrefsTool : MonoBehaviour
{
    public bool runTool = false;
    public bool resetScore = false;
    public bool checkScore = false;
    public int bestScore;
    public bool setBestScore = false;
    private void Awake()
    {
        if (!DebugMode.debugMode)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (resetScore)
        {
            PlayerPrefs.SetInt("highscore", 0);
            resetScore = false;
            print("[PLAYER PREF RESET] Resetted player pref highscore");
        }
        if (checkScore)
        {
            checkScore = false;
            print("[PLAYER PREF] Highscore: " + PlayerPrefs.GetInt("highscore"));
        }
        if (setBestScore)
        {
            setBestScore = false;
            PlayerPrefs.SetInt("highscore", bestScore);
            print("[PLAYER PREF] Highscore: " + PlayerPrefs.GetInt("highscore"));
        }




    }
}
