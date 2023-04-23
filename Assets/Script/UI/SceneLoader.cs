using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset startScene;
    [SerializeField] private SceneAsset tutoScene;
    [SerializeField] private SceneAsset gameScene;

    private void OnValidate()
    {
        if (startScene != null) startSceneName = startScene.name;
        if (tutoScene != null) tutoSceneName = tutoScene.name;
        if (gameScene != null) gameSceneName = gameScene.name;
    }
#endif

    [SerializeField] private string startSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string tutoSceneName;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startSceneName);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void LoadTutoScene()
    {
        SceneManager.LoadScene(tutoSceneName);
    }
}
