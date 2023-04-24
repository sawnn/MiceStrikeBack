using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreBoard : MonoBehaviour
{
    [SerializeField] private LineScore lineScore;
    [SerializeField] private RectTransform rectTransform;

    public static UIScoreBoard instance;
    public TMP_Text title;

    private void Awake()
    {
        instance = this;
    }
    private void Start() {
        Debug.Log("StartScoreboard");
        MusicManager.Instance.PlayMenuMusic();
    }

    public void AddLine(string name, string score, bool isChallenge)
    {
        if (isChallenge)
        {
            title.text = "CHALLENGE SCOREBOARD";
        }
        else
        {
            title.text = "SCOREBOARD";
        }
        RectTransform temp = lineScore.Create(name, score).GetComponent<RectTransform>();
        temp.SetParent(rectTransform);
        temp.localScale = Vector3.one;
    }

    public void Clear()
    {
        foreach (Transform child in rectTransform)
        {
            Destroy(child.gameObject);
        }
    }

}
