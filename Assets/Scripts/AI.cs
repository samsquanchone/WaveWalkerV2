using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AttackType {Melee, Range };
public class AI : MonoBehaviour
{

    public AttackType attackType;
    public float damage;
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public Transform safeSpot;
    State currentState;

    public PatrolHandler patHandler;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        currentState = new IdleState(this.gameObject, agent, anim, player, patHandler.patrolList);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}