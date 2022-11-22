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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerMovement.knockBackCounter = playerMovement.knockBackTotalTime;
            if (collider.transform.position.x <= transform.position.x)
            {
                playerMovement.knockFromRight = true;
            }
            else
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
