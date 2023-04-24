using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using Palmmedia.ReportGenerator.Core;

public class Leaderboard : MonoSingleton<Leaderboard>
{
    int leaderboardID = 13648;
    int leaderboardChallengeID = 13727;

    public int playerScore = 0;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    void Start()
    {

    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public void SubmitScore(int scoreToUpload)
    {
        StartCoroutine(SubmitScoreRoutine(scoreToUpload));
    }

    public void SubmitScoreChallenge(int scoreToUpload)
    {
        StartCoroutine(SubmitScoreChallengeRoutine(scoreToUpload));
    }

    IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        if (scoreToUpload > playerScore)
        {
            playerScore = scoreToUpload;
            LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID.ToString(), (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully uploaded score");
                    done = true;
                }
                else
                {
                    Debug.Log("Failed" + response.Error);
                }
            });

            yield return new WaitWhile(() => done == false);
        }
        else {
            yield return null;
        }
    
    }

    IEnumerator SubmitScoreChallengeRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        if (scoreToUpload > playerScore)
        {
            playerScore = scoreToUpload;
            LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardChallengeID.ToString(), (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully uploaded score");
                    done = true;
                }
                else
                {
                    Debug.Log("Failed" + response.Error);
                }
            });

            yield return new WaitWhile(() => done == false);
        }
        else {
            yield return null;
        }
    
    }

    public void FetchTopHighscores()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }

    public void FetchChallengeTopHighscores()
    {
        StartCoroutine(FetchChallengeTopHighscoresRoutine());
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        UIScoreBoard.instance.Clear();
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID.ToString(), 100, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames = members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames = "Unknown";
                    }
                    tempPlayerScores = members[i].score.ToString();
                    UIScoreBoard.instance.AddLine(tempPlayerNames, tempPlayerScores);

                }
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

       public IEnumerator FetchChallengeTopHighscoresRoutine()
    {
        UIScoreBoard.instance.Clear();
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardChallengeID.ToString(), 100, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames = members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames = "Unknown";
                    }
                    tempPlayerScores = members[i].score.ToString();
                    UIScoreBoard.instance.AddLine(tempPlayerNames, tempPlayerScores);

                }
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}