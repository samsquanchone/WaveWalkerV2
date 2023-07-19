using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    public int health;
    private TextMesh text;

    void Start()
    {
        text = gameObject.GetComponent<TextMesh>();

        health = 100;
        text.text = "Health; " + 100;
    }

    void dealDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            text.text = "Health; " + health;
        }
        else if(health <= 0)
        {
            GameManager.Instance.ResetGameScene();
        }
    }

    public void heal(int healthAmount)
    {
        if(health <= 100)
        {
            health += healthAmount;
            text.text = "Health; " + health;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            dealDamage(10);
    }
}
