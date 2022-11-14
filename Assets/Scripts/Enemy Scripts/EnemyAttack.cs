using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public int attackDamage;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (timer >= timeBetweenAttacks && playerHealth.currentHealthAmount > 0)
        {
            Debug.Log("Collider working");
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealthAmount > 0)
        {
            if (player)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
