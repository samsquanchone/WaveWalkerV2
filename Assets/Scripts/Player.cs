using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float initialHealth;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        initialHealth = 100;
        initialHealth = health;
    }

    public void PlayerHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        GameManager.Instance.ResetGameScene();
    }
  
}
