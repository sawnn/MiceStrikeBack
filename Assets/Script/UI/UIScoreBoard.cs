using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreBoard : MonoBehaviour
{
    [SerializeField] private LineScore lineScore;
    [SerializeField] private RectTransform rectTransform;

    public static UIScoreBoard instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start() {
        Debug.Log("StartScoreboard");
        MusicManager.Instance.PlayMenuMusic();
    }

    public void AddLine(string name, string score)
    {
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
