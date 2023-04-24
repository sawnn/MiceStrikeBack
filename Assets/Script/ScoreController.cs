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
    int newMouseCount = 0;
    private int errorCount = 0;
    private float timeElapsed = 0f;



    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        maxMouseCount = GameController.Instance.nombreSouris;
    }
    private void Update()
    {
        if (GameController.Instance.gameOn)
        {
            timeElapsed += Time.deltaTime;
        }

    }

    public void UpdateMaxMouseCount(int newMaxMouseCount)
    {
        maxMouseCount = newMaxMouseCount;
    }

    public void MouseCaught()
    {
        newMouseCount++;


        if (newMouseCount >= maxMouseCount)
        {
            mouseCount += newMouseCount;
            newMouseCount = 0;
            Debug.Log("Manche terminée !");
            //Debug.Log("Gagné !");
            Debug.Log("Score : " + CalculateScore());
            SoundManager.Instance.MuteMice();
            GameController.Instance.EndGame(true);
            
        }
    }

    public void CatchFail()
    {
        errorCount++;
        SoundManager.Instance.PlaySound("failSound");
        if(errorCount >= 3)
        {
            GameController.Instance.EndGame(false);
            Debug.Log("Game Over");
        }
    }


    public int CalculateScore()
    {
        int baseScore = mouseCount * 10;
        int timePenalty = (int)(timeElapsed * 0.5f);
        int failurePenalty = errorCount * 5;
        int totalScore = Mathf.Max(0, baseScore - timePenalty - failurePenalty);
        return totalScore;
    }
}