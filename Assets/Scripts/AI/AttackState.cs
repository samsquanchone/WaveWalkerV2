using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float rotationSpeed = 2.0f;
    GameObject npc;
    AudioSource shoot;
    private Vector3 lastPosition;
    bool canShoot = false;

    public AttackState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();
        npc = _npc;
        

       
    }

    public override void Enter()
    {
        canShoot = true;
        ShootGun();
        //anim.SetTrigger("isShooting");
        agent.isStopped = true;
       // shoot.Play();
        base.Enter();
        Debug.Log("EnterAttackState");
        anim.SetBool("IsRunning2", false);
        anim.SetBool("isShooting", true);
    }

    public IEnumerator ShootInterval()
    {
        
      
        //Muzzle flash
        int hitChance = Random.Range(0, 15);


        if (hitChance == 1 && this.npc.GetComponent<AI>() != null) {/* Damage player  */  player.GetComponent<Player>().PlayerHit(npc.GetComponent<AI>().damage); }
        else { yield return 0; }
        yield return new WaitForSeconds(0.15f);
        ShootGun();
       
    }

    public override void Update()
    {
        Vector3 _direction = base.PlayerDistance();
        float angle = Vector3.Angle(_direction, npc.transform.forward);
        _direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * rotationSpeed);




        if (!CanAttackPlayer() && npc.GetComponent<AI>().patrolType == PatrolType.Patrol)
        {
            

            MonoBehaviourInterface.Instance.StopRoutine(ShootInterval());
            nextState = new IdleState(npc, agent, anim, player, patrolPositions, false);
            stage = EVENT.EXIT;
        }

        else if (!CanAttackPlayer() && npc.GetComponent<AI>().patrolType == PatrolType.Guard) 
        {
            MonoBehaviourInterface.Instance.StopRoutine(ShootInterval());
            nextState = new ReturnState(npc, agent, anim, player, patrolPositions);
            stage = EVENT.EXIT;
        }

        /*
        if (npc.transform.localPosition == lastPosition)
            anim.SetBool("IsRunning2", false);
        else
            anim.SetBool("IsRunning2", true);

        lastPosition = npc.transform.localPosition;
        */
    }

    void ShootGun()
    {
        if (this.npc.GetComponent<AI>() != null && canShoot)
        {
            
                MonoBehaviourInterface.Instance.StartRoutine(ShootInterval());
        }
    }

    public override void Exit()
    {
        canShoot = false;
        MonoBehaviourInterface.Instance.StopRoutine(ShootInterval());
        anim.SetBool("isShooting", false);
       // shoot.Stop();
        base.Exit();
        Debug.Log("ExitAttackState");
    }
}
