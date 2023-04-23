using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameController : MonoSingleton<GameController>
{
    public int nombreSouris = 10;

    public bool hunt = false;

    public float timeBeforeStart = 60f;
    float timer = 0f;

    public List<Souris> sourisList;

    




    void Start()
    {

    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
                Debug.Log("Awake");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (hunt == false  && sourisList.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeStart)
            {
                hunt = true;
                TrapSelectionMenu.Instance.DesactiveManager();
                GameObject.FindGameObjectWithTag("MenuTrap").SetActive(false);
                SpawnSouris();
            }
        }

    }

    public void SpawnSouris()
    {
        foreach (Souris souris in sourisList)
        {
            souris.gameObject.SetActive(true);
        }
       /* for (int i = 0; i < nombreSouris; i++)
        {
            Vector3 randomPosition = RandomNavMeshLocation();
            GameObject souris = Instantiate(sourisPrefab, randomPosition, Quaternion.Euler(90, 0, 0));
        }*/
    }

    private Vector3 RandomNavMeshLocation()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        Vector3 randomPoint = Vector3.zero;

        int maxIterations = 100;

    for (int i = 0; i < maxIterations; i++)
    {
        int randomIndex = UnityEngine.Random.Range(0, triangulation.indices.Length - 2); 
        int index1 = triangulation.indices[randomIndex];
        int index2 = triangulation.indices[randomIndex + 1];
        int index3 = triangulation.indices[randomIndex + 2];

        Vector3 v1 = triangulation.vertices[index1];
        Vector3 v2 = triangulation.vertices[index2];
        Vector3 v3 = triangulation.vertices[index3];

        randomPoint = (v1 + v2 + v3) / 3f; 

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas)) 
        {
            randomPoint = hit.position;
            break;
        }
    }
            return randomPoint;
        }


        public void EndGame(bool isWin) 
        {
            sourisList.Clear();
            timer = 0;
            if (isWin)
            {
                Debug.Log("Gagné !");
                SceneController.Instance.LoadScene("Win");
            }
            else
            {
                Debug.Log("Perdu !");
            }
        }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        switch (scene.name)
        {
            case "DayOne":
                timeBeforeStart = 0f;
                break;
            case "DayTwo":
                timeBeforeStart = 60f;
                break;
            case "DayThree":
                timeBeforeStart = 60f;
                break;
            case "DayFour":
                timeBeforeStart = 60f;
                break;
            default:
                break;
        }
        foreach (Souris go in Resources.FindObjectsOfTypeAll<Souris>())
        {
            if (go.gameObject.scene.name != null)
                sourisList.Add(go);
        }
        nombreSouris = sourisList.Count;
    }
}
