using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>
{
    // Start is called before the first frame update

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
