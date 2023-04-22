using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Souris : MonoBehaviour
{
     public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float fleeDistance = 5f;
    public float fleeTime = 1f;

    private NavMeshAgent agent;
    public float timer;
    public bool fleeing = false;
    private Vector3 fleePosition;

    public Vector3 newPos;

    public bool stop = false;
    public float stopTimer = 1f;

    float speed = 0;

    public MeshFilter mesh;
    Vector3 center = new Vector3(0, 0, 0);  

    // Use this for initialization
    void Start ()
    {
        Vector3 center = mesh.mesh.bounds.center;
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        timer = 0;
        Vector3 startPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(startPos);
        newPos = RandomNavSphere(startPos, wanderRadius, -1);
    }
	
    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        if (fleeing)
        {
            agent.SetDestination(fleePosition);
            if (timer >= fleeTime || ReachedDestination())
            {
                timer = 0;
                fleeing = false;
            }
        }
        else if (stop)
        {
            if (timer >= stopTimer)
            {
                timer = 0;
                Debug.Log("Fleeing");
                agent.speed = speed;
                fleeing = true;
                stop = false;
            }
        }
        else
        {
            if (timer >= wanderTimer)
            {
                agent.SetDestination(newPos);
                newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                timer = 0;
            }

            if (ReachedDestination())
            {
                timer = wanderTimer;
            }
        }



    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            fleeing = false;
            timer = 0;
            stop = true;
            agent.speed = 0;
            fleePosition = transform.position + (transform.position - other.transform.position).normalized * fleeDistance;

        }
    }

    public bool ReachedDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += center;

        NavMeshHit navHit;
        while (!NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
        }

        return navHit.position;
    }
}

