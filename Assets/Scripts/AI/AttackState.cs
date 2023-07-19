using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float rotationSpeed = 2.0f;
    GameObject npc;
    AudioSource shoot;
    bool hasExited = false;
    public Vector3 lastPosition;
    public AttackState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();
        npc = _npc;

        ShootGun();
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        agent.isStopped = true;
       // shoot.Play();
        base.Enter();
        Debug.Log("EnterAttackState");
    }

    IEnumerator ShootInterval()
    {
        MonoBehaviourInterface.Instance.InstantiateObject(npc.GetComponent<AI>().muzzleFlash, npc.GetComponent<AI>().firePoint);
        Debug.Log("Shooooooot");
        //Muzzle flash
        int hitChance = Random.Range(0, 15);


        if (hitChance == 1) {/* Damage player  */  player.GetComponent<Player>().PlayerHit(npc.GetComponent<AI>().damage); }

        yield return new WaitForSeconds(0.15f);
        ShootGun();
       
    }

    public override void Update()
    {
        Vector3 _direction = base.PlayerDistance();
        float angle = Vector3.Angle(_direction, npc.transform.forward);
        _direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * rotationSpeed);

        


        if (!CanAttackPlayer())
        {
            hasExited = true;
            MonoBehaviourInterface.Instance.StopRoutine(ShootInterval());
            nextState = new IdleState(npc, agent, anim, player, patrolPositions, false);
            stage = EVENT.EXIT;
        }

        if (npc.transform.localPosition == lastPosition)
            anim.SetBool("IsRunning2", false);
        else
            anim.SetBool("IsRunning2", true);

        lastPosition = npc.transform.localPosition;
    }

    void ShootGun()
    {
        Debug.Log("steve seagul");
        if(!hasExited)
        MonoBehaviourInterface.Instance.StartRoutine(ShootInterval());
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isShooting");
        // shoot.Stop();
        anim.SetBool("IsRunning2", false);
        base.Exit();
        Debug.Log("ExitAttackState");
    }
}
