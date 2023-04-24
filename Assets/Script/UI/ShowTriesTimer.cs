using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTriesTimer : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void BackToMenu() {
        SceneController.Instance.LoadScene("MenuPrincipal");
    }
  }
