using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Damage(float damage);
    
}
public class EnemyHealth : MonoBehaviour, IEnemy
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    public void Damage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {

          
           
            this.gameObject.GetComponent<AI>().Dead();
            gameObject.SetActive(false);
        }
         
        
    }

    public float GetHealth()
    {
        float h = _health;

        return h;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
