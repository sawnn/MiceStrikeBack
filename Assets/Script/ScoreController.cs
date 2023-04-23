using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoSingleton<ScoreController>
{
    int maxMouseCount;
    public int scorePerMouse = 10;
    public int scorePerTime = 1;
    public int scorePenaltyPerError = 5;

    private int mouseCount = 0;
    private int errorCount = 0;
    private float timeElapsed = 0f;



    private void Start() {
        maxMouseCount = GameController.Instance.nombreSouris;
    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public void MouseCaught()
    {
        mouseCount++;


        if (mouseCount >= maxMouseCount)
        {
            Debug.Log("Gagné !");
            Debug.Log("Score : " + GetScore());
        }
    }

    public void CatchFail()
    {
        errorCount++;
    }

    public int GetScore()
    {
        int score = mouseCount * scorePerMouse - (errorCount * scorePenaltyPerError) / Mathf.RoundToInt(timeElapsed);
        return score;
    }
}