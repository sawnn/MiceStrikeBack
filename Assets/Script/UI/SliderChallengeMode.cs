using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChallengeMode : MonoBehaviour
{
    public bool isMice = false;
    private Slider slider;
    [SerializeField] private TMPro.TMP_Text showTxt;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetScoreShow()
    {
        showTxt.text = slider.value.ToString();
        if (isMice)
        {
            GameController.Instance.ChangeNbSouris((int)slider.value);
        }
        else
        {
            GameController.Instance.ChangeNbTrap((int)slider.value);
        }
    }
}
