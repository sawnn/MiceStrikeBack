using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChallengeMode : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TMPro.TMP_Text showTxt;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetScoreShow()
    {
        showTxt.text = slider.value.ToString();
    }
}
