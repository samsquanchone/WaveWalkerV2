using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : State
{
    float rotationSpeed = 2.0f;
    private float swinTime = 1.2f;
    private bool canSwing = false;
    private bool startTimer = false;

    public MeleeState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
   : base(_npc, _agent, _anim, _player, _patrolTransforms)
    {
        name = STATE.MELEE;
        

    }
    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        agent.isStopped = true;
        // shoot.Play();
        base.Enter();
        Debug.Log("MeleState");
        MeleAttack();


    }

    public override void Update()
    {
        Vector3 _direction = base.PlayerDistance();
        float angle = Vector3.Angle(_direction, npc.transform.forward);
        _direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * rotationSpeed);

        if(canSwing)
        {
            MeleAttack();
        }

        if(startTimer)
        {
            swinTime -= Time.deltaTime;
            if (swinTime <= 0)
            {
                startTimer = false;
                canSwing = true;
            }
        }

        if (!CanMeleePlayer() && Vector3.Distance(player.position, npc.transform.position) > 3) 
        {
            nextState = new PersueState(npc, agent, anim, player, patrolPositions);
            
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isShooting");
        // shoot.Stop();
        base.Exit();
    }

    void MeleAttack()
    {
        Debug.Log("Swing");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerHit(npc.GetComponent<AI>().damage);
        canSwing = false;
        startTimer = true;
    }

    IEnumerator SwingTimer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerHit(npc.GetComponent<AI>().damage);
        yield return new WaitForSeconds(1f);
        MeleAttack();
    }

}
