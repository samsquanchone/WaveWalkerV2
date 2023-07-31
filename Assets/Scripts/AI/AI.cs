using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AttackType {Melee, Range };
public enum PatrolType {Guard, Patrol };

public enum EnemyState {Normal, EndGame };
public class AI : MonoBehaviour
{

    public AttackType attackType;
    public PatrolType patrolType;
    public float damage;
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public Transform safeSpot;
    public Transform resestSpotRanged;

    public Transform firePoint;
    public GameObject muzzleFlash;
    public Vector3 position;
    public Quaternion rotation;
    State currentState;
    public bool isEnemyDead = false;
    public PatrolHandler patHandler;
    public List<Transform> transf = new();

    public EnemyState enemyState { get; private set; } = EnemyState.Normal;
    // Start is called before thfe first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        currentState = new IdleState(this.gameObject, agent, anim, player, patHandler.patrolList, false);

        this.position = transform.position;
        this.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.Normal)
        {
            agent.isStopped = false;
            currentState = currentState.Process();
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public void SetPlayerDestination()
    {
        enemyState = EnemyState.EndGame;
        agent.SetDestination(player.position);
    }

    public bool isDead()
    {
        return isEnemyDead;
    }

    public void Dead()
    {
        Destroy(this);
        isEnemyDead = true;
        Events.Instance.KilledEnemy(attackType);

    }
}