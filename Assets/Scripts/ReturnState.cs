using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : State 
{
    
    public ReturnState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.RETURNTOORIGN;
        agent.speed = 4;
        agent.isStopped = false;
      
        
    }

    public override void Enter()
    {
        // anim.SetTrigger("isRunning");
        anim.SetBool("IsRunning2", true);
        base.Enter();
        Debug.Log("EnterReturnState");


    }

    public override void Update()
    {
        agent.SetDestination(npc.GetComponent<AI>().position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, npc.GetComponent<AI>().rotation, 0.5f * Time.deltaTime);
                 nextState = new IdleState(npc, agent, anim, player, patrolPositions, false);
                 stage = EVENT.EXIT;
        }
       
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isRunning");
        // anim.SetTrigger("isRunning");
        anim.SetBool("IsRunning2", false);
        base.Exit();
        Debug.Log("ExitReturnState");
    }
}
