using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoSingleton<GameController>
{
    public GameObject sourisPrefab;
    public int nombreSouris = 10;


    void Start()
    {
        for (int i = 0; i < nombreSouris; i++)
        {
            Vector3 randomPosition = RandomNavMeshLocation();
            GameObject souris = Instantiate(sourisPrefab, randomPosition, Quaternion.Euler(90, 0, 0));
        }
    }

    private Vector3 RandomNavMeshLocation()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        Vector3 randomPoint = Vector3.zero;

        int maxIterations = 100;

    for (int i = 0; i < maxIterations; i++)
    {
        int randomIndex = Random.Range(0, triangulation.indices.Length - 2); 
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
}
