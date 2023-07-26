using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    //Enum, providing enums for the specific states 
    public enum STATE
    {
        IDLE, PATROL, PERSUE, ATTACK, SLEEP, FLEE, MELEE, RETURNTOORIGN
    };

    //Stages of the current state, provided in the form of enums
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; // Used for saving state name
    protected EVENT stage; //used for saving what stage of state the NPC is in 
    protected GameObject npc; //Used for assigning to the desired NPC
    protected Animator anim; //Used for the animator
    protected Transform player; //Player transform to be provided to the NPC
    protected State nextState; //Used for providing the next state to the NPC
    protected UnityEngine.AI.NavMeshAgent agent; //Provides the class with the NPCS NavMeshAgent
    protected List<Transform> patrolPositions;

  

    float visDist = 30.0f; //Distance from where the NPC can detect the player
    float visAngle = 180.0f; //Angle at which the NPC can detect the player
    float shootDist = 30.0f; //Distance at which the NPC can start shooting at the player 

    public State(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player, List<Transform> _patrolTransforms)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        patrolPositions = _patrolTransforms;


    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState; //Return next state 
        }
        return this; //If no next state, return current state 
    }
    public bool CanSeePlayer()
    {
        Vector3 _direction = PlayerDistance();
        float angle = Vector3.Angle(_direction, npc.transform.forward);

        if (_direction.magnitude < visDist && angle < visAngle)
        {
            return true; //NPC can see player 
        }
        return false; //NPC can't see player 
    }

    //For shooting
    public bool CanAttackPlayer()
    {
        Vector3 _direction = PlayerDistance();
        if (_direction.magnitude < shootDist)
        {
            return true;
        }
        return false;

    }

 
    public bool IsShot()
    {
        if (npc.GetComponent<EnemyHealth>().GetHealth() < 100) //Should add a max health field to health script
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool CanMeleePlayer()
    {
        Vector3 _direction = PlayerDistance();
        if (_direction.magnitude < 2)
        {
            return true;
        }
        return false;

    }

    public bool CanFlee()
    {
        Vector3 _direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(_direction, npc.transform.forward);

        if (_direction.magnitude < 2 && angle < 30)
        {
            return true;
        }

        return false;

    }

    public Vector3 PlayerDistance()
    {
        Vector3 direction = player.position - npc.transform.position; //Getting vector from NPC to player

        return direction;
    }




}