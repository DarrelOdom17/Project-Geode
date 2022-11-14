using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
}
