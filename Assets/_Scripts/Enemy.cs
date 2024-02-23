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
    private int health;
    [SerializeField]protected bool canHit;
    [SerializeField]public float timeBetweenHits;
    protected float timer;

    protected virtual void Start()
    {
        playerGameobject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        timer = timeBetweenHits;
    }
    public void setEnemyToPlayer() {
        isTriggered = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
            canHit = true;
        if (isTriggered) { 
            agent.SetDestination(playerGameobject.transform.position);
            // Check if we've reached the destination
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //check if can hit
                        if (canHit)
                            hit();
                    }
                }
            }
        }
    }
    public virtual void hit()
    {
        
    }
    public void getHit(int damage)
    {
        health -= damage;
    }

}
