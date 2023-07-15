using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.IDLE;
    }

    //Overriding the base Enter method to define Idle behaviour 
    public override void Enter()
    {
        //anim.SetTrigger("isIdle"); //Setting animator
        base.Enter(); //Triggering enter method in base, which sets stage to Update
    }

    public override void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            nextState = new PatrolState(npc, agent, anim, player, patrolPositions); //If random value less than 10, set state to patrol
            stage = EVENT.EXIT; //Setting stage to exit

            if (CanSeePlayer())
            {
                nextState = new PersueState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT;
            }
        }

    }

    public override void Exit()
    {
        //anim.ResetTrigger("isIdle"); //Reseting isIdle trigger to avoid animator bugs
        base.Exit(); //Set stage to exit
    }

}
