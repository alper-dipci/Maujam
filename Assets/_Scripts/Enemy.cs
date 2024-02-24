using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject playerGameobject;
    private NavMeshAgent agent;
    public bool isTriggered;
    [SerializeField] protected bool isHitting;
    private Animator animator;
    [SerializeField]private int health;
    [SerializeField]protected bool canHit;

    [SerializeField]public float timeBetweenHits;
    protected float timer;
    [SerializeField] private float HitRange;
    [SerializeField] private float detectRange;
    [SerializeField] private float unDetectRange;

    private Vector3 startPos;
    private Vector3 patrolPos;
    [SerializeField] private float randomCircleArea=10f;
    [SerializeField] private float timeBetweenPatrols = 2f;
    private float waitForPatroltimer;
    private bool waitForPatrol;

    protected virtual void Start()
    {
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        timer = timeBetweenHits;
        startPos = transform.position;
        setAgentRandomPatrol();
    }
    public void setEnemyToPlayer() {
        isTriggered = true;
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if(waitForPatrol)
            waitForPatroltimer -= Time.deltaTime;
        if(timer < 0)
            canHit = true;

        if(Vector3.Distance(playerGameobject.transform.position, transform.position)< detectRange)
            setEnemyToPlayer();
        if (Vector3.Distance(playerGameobject.transform.position, transform.position) > unDetectRange)
            isTriggered = false;
        if (isTriggered) {
            LookAtTarget();
            agent.stoppingDistance = HitRange;
            agent.SetDestination(playerGameobject.transform.position);
            if (isAgentArrived()&&canHit)
                hit();
        }
        else if (isAgentArrived())
        {
            waitForPatrol = true;
            if (waitForPatroltimer <= 0)
            {
                waitForPatrol = false;
                waitForPatroltimer = timeBetweenPatrols;
                setAgentRandomPatrol();
            }
            
        }
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = playerGameobject.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    private  void setAgentRandomPatrol()
    {       
        float random = Random.Range(-randomCircleArea, randomCircleArea);
        Mathf.Clamp(Mathf.Abs(random), 2, randomCircleArea);
        patrolPos = new Vector3(startPos.x + random, startPos.y, startPos.z + random);
        agent.SetDestination(patrolPos);
    }
    public bool isAgentArrived()
    {
        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public virtual void hit()
    {
        Debug.LogError("hit not implemented");
    }

    public void getHit(int damage)
    {
        health -= damage;
        if (health < 0) die();
        //ResetAllTriggers();
        //animator.SetBool("getHit", true);
    }
    void die()
    {
        //Instantiate()
        Destroy(gameObject);
    }
    private void ResetAllTriggers()
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }

}
