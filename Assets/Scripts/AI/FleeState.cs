using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{

    public FleeState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.FLEE;





    }

    public override void Enter()
    {
        //anim.SetTrigger("isRunning");
        agent.isStopped = false;
        agent.speed = 6;
        agent.SetDestination(GameEnvironment.Singleton.safeSpot.transform.position);
        base.Enter();

    }

    public override void Update()
    {

        if (agent.remainingDistance < 1)
        {
            nextState = new IdleState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
       // anim.ResetTrigger("isRunning");
        base.Exit();
    }





}
