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



    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

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
            Debug.Log("Manche terminée !");
            //Debug.Log("Gagné !");
            //Debug.Log("Score : " + GetScore());
            SoundManager.Instance.MuteMice();
            
        }
    }

    public void CatchFail()
    {
        errorCount++;
        SoundManager.Instance.PlaySound("failSound");
        if(errorCount >= 3)
        {
            Debug.Log("Game Over");
        }
    }

    public int GetScore()
    {
        int score = mouseCount * scorePerMouse - (errorCount * scorePenaltyPerError) / Mathf.RoundToInt(timeElapsed);
        return score;
    }
}