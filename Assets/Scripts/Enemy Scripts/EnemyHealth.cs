using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private bool damageable = true;
    public int maxHealth;
    public int currentHealthAmount;

    EnemyAttack damage;

    // Start is called before the first frame update
    void Start()
    {
       currentHealthAmount = maxHealth;
       damage = GetComponent<EnemyAttack>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (damageable && currentHealthAmount > 0)
            {
                currentHealthAmount -= amount;
                damageable = true;
                Debug.Log("Damage being done!");
            }
        
        else
        {
            damageable = false;
            Die();
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            TakeDamage(1);
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
