using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScore : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text textName;
    [SerializeField] private TMPro.TMP_Text textScore;

    public GameObject Create(string name, string score)
    {
        LineScore newLine = Instantiate(this);
        newLine.textName.text = name;
        newLine.textScore.text = score;
        return newLine.gameObject;
    }
}
