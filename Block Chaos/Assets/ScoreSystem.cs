using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    public int scorePoints = 0;
    public int enemyKilled = 0;


    private void Start()
    {
        scoreTxt.text = "SCORE: " + scorePoints;
    }

    public void onUnitKilled(GameObject victim, int scorePoint)
    {
        scorePoints += scorePoint;
        scoreTxt.text = "SCORE: " + scorePoints;
        enemyKilled += 1;
    }
}
