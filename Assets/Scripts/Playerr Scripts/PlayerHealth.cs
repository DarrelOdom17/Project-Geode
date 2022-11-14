using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private bool damageable = true;
    [SerializeField] public int currentHealthAmount;
    [SerializeField] public int maxHealthAmount;
    [SerializeField] private int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //private ApplyDamage amount;
    // Start is called before the first frame update
    void Start()
    {
        currentHealthAmount = maxHealthAmount;
        //amount = GetComponent<ApplyDamage>().damageAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealthAmount > numOfHearts)
        {
            currentHealthAmount = numOfHearts;   
        }
        
        // Controls the health bar display
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealthAmount)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(1);
        }
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

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
