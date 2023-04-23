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

    public GameObject flash;

    float speed = 0;
    Vector3 center = new Vector3(0, 0, 0);
    Animator animator;
    [SerializeField] GameObject surprise;

    // Use this for initialization
    void Awake ()
    {
        animator = GetComponent<Animator>();
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

		if (agent.velocity.x > 0)
		{
			transform.localScale = new(Mathf.Abs(transform.localScale.x), transform.localScale.y,transform.localScale.z);
		}
		else if (agent.velocity.x < 0)
		{
			transform.localScale = new(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
		}

	}

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("surprise");
            fleeing = false;
            timer = 0;
            stop = true;
            agent.speed = 0;
            fleePosition = transform.position + (transform.position - other.transform.position).normalized * fleeDistance;
            StartCoroutine(SurpriseApparition());
		}
    }
    private IEnumerator SurpriseApparition()
    {
		surprise.SetActive(true);
        yield return new WaitForSeconds(0.75f);
		surprise.SetActive(false);
	}

    public bool ReachedDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += transform.position;

        NavMeshHit navHit;
        while (!NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            randDirection = Random.insideUnitSphere * dist;
            randDirection += transform.position;
        }

        return navHit.position;
    }

    public void Flash()
    {
        flash.SetActive(true);
        StartCoroutine(Wait(1f));
    }

    public void Slow()
    {
        agent.speed = speed / 2;
        StartCoroutine(Wait(6f));
    }

    public void Stun()
    {
        agent.isStopped = true;
        StartCoroutine(Wait(5f));
    }

    IEnumerator Wait(float x)
    {
        yield return new WaitForSeconds(x);
        flash.SetActive(false);
        agent.isStopped = false;
        agent.speed = speed;
    }


}

