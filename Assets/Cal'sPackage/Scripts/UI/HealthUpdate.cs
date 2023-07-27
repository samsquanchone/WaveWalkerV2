using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    
    public int health;
    private TextMesh text;
    private Button button;

    void Start()
    {
        

        text = gameObject.GetComponent<TextMesh>();

        health = 100;
        text.text = "Health; " + 100;
    }

    public void dealDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            int _health = Mathf.Clamp(health, 0, 100);
            text.text = "Health; " + _health;
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
            int newHealth = Mathf.Clamp(health, 0, 100);
            text.text = "Health; " + newHealth;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            dealDamage(10);
    }
}
