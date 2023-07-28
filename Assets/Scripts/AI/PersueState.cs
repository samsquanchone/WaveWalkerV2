using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueState : State
{
    public PersueState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.PERSUE;
        agent.speed = 7;
        agent.isStopped = false;
    }

    public override void Enter()
    {
       
        // anim.SetTrigger("isRunning");
        anim.SetBool("IsRunning2", true);
        base.Enter();
        Debug.Log("EnterPersueState");
     

    }

    public override void Update()
    {



        if (npc.GetComponent<AI>().attackType == AttackType.Range) { agent.stoppingDistance = 20; }

        else if(npc.GetComponent<AI>().attackType == AttackType.Melee) { agent.stoppingDistance = 1.2f; }

        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer() && npc.GetComponent<AI>().attackType == AttackType.Range) //and shoot enemy
            {
                nextState = new AttackState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT;
            }
            if (CanMeleePlayer() && npc.GetComponent<AI>().attackType == AttackType.Melee)
            {
                nextState = new MeleeState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT;
            }
            float distance = Vector3.Distance(this.npc.GetComponent<AI>().position, this.npc.transform.position);
            if (distance > 10 && npc.GetComponent<AI>().patrolType == PatrolType.Guard)
            {
                nextState = new IdleState(npc, agent, anim, player, patrolPositions, true);
                stage = EVENT.EXIT;
            }
            // if can melee enemy and is melee enemy then enter attack state
            else if (!CanSeePlayer())
            {
                switch (npc.GetComponent<AI>().patrolType)
                {
                    case PatrolType.Patrol:
                        nextState = new PatrolState(npc, agent, anim, player, patrolPositions);
                        break;

                    case PatrolType.Guard:
                        nextState = new ReturnState(npc, agent, anim, player, patrolPositions);
                        break;
                }
               
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isRunning");
        anim.SetBool("IsRunning2", false);
        base.Exit();
        Debug.Log("ExitPersueState");
    }

}
