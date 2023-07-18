using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float rotationSpeed = 2.0f;
    AudioSource shoot;
    public AttackState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();

    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        agent.isStopped = true;
       // shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 _direction = base.PlayerDistance();
        float angle = Vector3.Angle(_direction, npc.transform.forward);
        _direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * rotationSpeed);
        if (!CanAttackPlayer())
        {
            nextState = new IdleState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
       // anim.ResetTrigger("isShooting");
       // shoot.Stop();
        base.Exit();
    }
}
