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
        base.Enter();

    }

    public override void Update()
    {
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
            // if can melee enemy and is melee enemy then enter attack state
            else if (!CanSeePlayer())
            {
                nextState = new PatrolState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
       //anim.ResetTrigger("isRunning");
        base.Exit();
    }

}
