using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_Text textValidation;
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Successfully set player name");
                textValidation.text = "Successfully set player name";
                textValidation.color = Color.green;
            }
            else
            {
                Debug.Log("Could not set player name" + response.Error);
                textValidation.text = "Could not set player name";
                textValidation.color = Color.red;
            }
            playerNameInputField.text = "";
        });
    }

    public void FetchScoreboard()
    {
        Leaderboard.Instance.FetchTopHighscores();
    }

    public void FetchScoreboardChallenge()
    {
        Leaderboard.Instance.FetchChallengeTopHighscores();
    }


    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        //yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
