using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public int attackDamage;

    //public GameObject player;
    //public PlayerHealth playerHealth;
    //public EnemyHealth enemyHealth;

   public PlayerMovement playerMovement;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame

    private void OnCollisonEnter2D(Collision2D collison)
    {
        if (collison.gameObject.tag == "Player")
        {
            playerMovement.knockBackCounter = playerMovement.knockBackTotalTime;
            if (collison.transform.position.x <= transform.position.x)
            {
                playerMovement.knockFromRight = true;
            }
            if (collison.transform.position.x > transform.position.x)
            {
                playerMovement.knockFromRight = false;
            }
            Debug.Log("KnockBack Detected");
            //playerHealth.TakeDamage(attackDamage);
        }
    }

   /* void Attack()
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
    */

    
}
