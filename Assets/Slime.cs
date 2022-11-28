using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public EnemyAttack enemyAttack;

    [Header("Movement Variables")]
    [Tooltip("These variables need to be assigned on the slime script to work")]
    public float detectRange;
    public float slimeSpeed;
    
    private Animator animator;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyHealth>().currentHealthAmount <= 0)
        {
            animator.SetBool("Dead", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyAttack.attackRange);
    }
}
