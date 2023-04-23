using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Laser : MonoBehaviour
{
    public float range = 10f;

    public float detectDuration = 2f;
    public Color detectColor = Color.red;
    private float detectTimeLeft = 0f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        lineRenderer.material.color = Color.white;
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.up * range, Color.red);
        if (Physics.Raycast(transform.position, transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Souris"))
            {
                SoundManager.Instance.PlaySound("laserSound");
                Debug.Log("Hit");
                StartDetection();
            }
            NavMeshHit navHit;
            if (NavMesh.Raycast(transform.position, hit.point, out navHit, NavMesh.AllAreas))
            {
                Debug.Log("NavMesh Hit");
                // Place le point d'intersection sur le navmesh
                hit.point = navHit.position;
                // Récupère la distance du point de l'émetteur jusqu'au point d'intersection sur le navmesh
                float distance = Vector3.Distance(transform.position, hit.point);
                // Définit la position de fin du rayon laser
                lineRenderer.SetPosition(1, new Vector3(0f, 0f, distance));
            }
            else {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }


        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.up * range);

            
        }

        if (detectTimeLeft > 0f)
        {
            detectTimeLeft -= Time.deltaTime;

            float lerp = Mathf.PingPong(Time.time, 0.5f) * 2f; // This will create a flashing effect
            lineRenderer.material.color = Color.Lerp(Color.white, detectColor, lerp);
        }
        else
        {
            lineRenderer.material.color = Color.white;
        }
    }


    IEnumerator UpdateLaserColor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < detectDuration)
        {
            float lerp = Mathf.PingPong(Time.time, 0.5f) * 2f; // This will create a flashing effect
            lineRenderer.material.color = Color.Lerp(Color.white, detectColor, lerp);
            yield return new WaitForSeconds(0.05f);
            elapsedTime += 0.05f;
        }
        lineRenderer.material.color = Color.white;
    }

    void StartDetection()
    {
        detectTimeLeft = detectDuration;
        StartCoroutine(UpdateLaserColor());
    }


    
}
