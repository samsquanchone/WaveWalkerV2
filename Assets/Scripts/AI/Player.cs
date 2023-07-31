using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField] float initialHealth;
    public float health;

    public HealthUpdate healthUI;
    // Start is called before the first frame update
    void Start()
    {
        initialHealth = 100;
        initialHealth = health;
        this.gameObject.transform.localScale = new Vector3(2, 2, 2);
    }

    public void PlayerHit(float damage)
    {
        healthUI.dealDamage((int)damage);

        /*if (health <= 0)
        {
            PlayerDead();
        }
        */
    }

    void PlayerDead()
    {
        GameManager.Instance.ResetGameScene();
    }

    private void Update()
    {
        //SandZone
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = new Vector3(247, 2, 180);
        }

        //IceZone
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            transform.position = new Vector3(247, 2, -50);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CrashEnemyZone")
        {
            Events.Instance.StartEnemyArea(AreaType.CrashArea);

        }
        if (other.gameObject.tag == "SnowZone")
        {
            Events.Instance.ChangeZone(AreaType.IceArea);
            Debug.Log("Entering Snow zone");
        }
    }
}
