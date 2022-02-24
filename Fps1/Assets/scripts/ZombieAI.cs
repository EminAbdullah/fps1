using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum ZombieState
{
    Idle=0,
    walk=1,
    Dead=2,
    Attack=3,
}

public class ZombieAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    ZombieState zombieState;
    GameObject playerObject;
    ZombieHealth zombieHealth;
    PlayerHealth playerHealth;
   
    // Start is called before the first frame update
    void Start()
    {
        zombieHealth = GetComponent<ZombieHealth>();
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        zombieState = ZombieState.Idle;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieHealth.getHealth() <= 0)
        {
            setState(ZombieState.Dead);
        }
       
        switch (zombieState)
        {
            case ZombieState.Dead:
                killZombie();
                break;
            case ZombieState.Attack:
                Attack();
                break;
            case ZombieState.walk:
                SearchForTarget();
                break;
            case ZombieState.Idle:
                SearchForTarget();
                break;                   
            default:
                break;
        }

    }

    private void Attack()
    {
        
        setState(ZombieState.Attack);
        agent.isStopped = true;
        
    }
    void MakeAttack()
    {
        playerHealth.DeductHealth(10);
        SearchForTarget();
    }
    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance<2)
        {
            Attack();
        }
        else if (distance<10)
        {
            moveToPlayer();
        }
        else
        {
        
           setState(ZombieState.Idle);

           agent.isStopped = true;
        }
    }

    private void setState(ZombieState state)
    {
        zombieState = state;
        //animator
        animator.SetInteger("State", (int)state);
    }

    private void moveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
        setState(ZombieState.walk);
    }

    private void killZombie()
    {
        setState(ZombieState.Dead);
        agent.isStopped = true;
        Destroy(gameObject, 5);
    }
}
