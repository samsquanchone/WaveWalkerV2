using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    bool ret = false;
    public IdleState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms, bool shouldReturn)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.IDLE;
        npc = _npc;
        ret = shouldReturn;
        anim = _anim;
    }

    //Overriding the base Enter method to define Idle behaviour 
    public override void Enter()
    {
        anim.SetBool("isIdle", true); //Setting animator
        
        base.Enter(); //Triggering enter method in base, which sets stage to Update
    }

    public override void Update()
    {


        if (npc.GetComponent<AI>().enemyState == EnemyState.EndGame)
        {
            nextState = new PersueState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;

        }


        if (Random.Range(0, 100) < 10)
        {
          

            if (this.npc.GetComponent<AI>().patrolType == PatrolType.Patrol)
            {

                nextState = new PatrolState(npc, agent, anim, player, patrolPositions); //If random value less than 10, set state to patrol
                stage = EVENT.EXIT; //Setting stage to exit


            }

            if (ret && this.npc.GetComponent<AI>().patrolType == PatrolType.Guard)
            {
                nextState = new ReturnState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT; //Setting stage to exit
            }

            else if (CanSeePlayer() && !ret)
            {
                nextState = new PersueState(npc, agent, anim, player, patrolPositions);
                stage = EVENT.EXIT;
            }
        }

    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle"); //Reseting isIdle trigger to avoid animator bugs
        base.Exit(); //Set stage to exit
    }

}
