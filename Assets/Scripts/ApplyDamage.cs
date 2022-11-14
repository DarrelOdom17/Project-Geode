using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Damage(1);
    }

    public void Damage(int damageAmount)
    {
        playerHealth.currentHealthAmount -= damageAmount;
        return;
    }
}
