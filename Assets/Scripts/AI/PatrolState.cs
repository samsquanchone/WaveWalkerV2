using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    int currentIndex = -1;

    public PatrolState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
      
    }

    public override void Enter()
    {
        anim.SetBool("isWalking", true);
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < patrolPositions.Count; i++)
        {
            GameObject thisWP = patrolPositions[i].gameObject;
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }

        //anim.SetTrigger("isWalking");
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance < 1 && npc.GetComponent<AI>().enemyState == EnemyState.Normal)
        {
            int nextPos = Random.Range(0, patrolPositions.Count);

            agent.SetDestination(patrolPositions[nextPos].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new PersueState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }

        if (IsShot())
        {
            nextState = new PersueState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }

        else if (CanFlee())
        {
            nextState = new FleeState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
       // anim.ResetTrigger("isWalking");
        base.Exit();
    }
}