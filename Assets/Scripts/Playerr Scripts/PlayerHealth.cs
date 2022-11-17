using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Variables")]
    //[SerializeField] private bool damageable = true;
    [SerializeField] public int currentHealthAmount;
    [SerializeField] public int maxHealthAmount;
    [SerializeField] private int numOfHearts;
    [SerializeField] private float invincibilityTime;
    [SerializeField] private bool isInvincible = false;

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
        if (!isInvincible && currentHealthAmount > 0)
            {
                currentHealthAmount -= amount;
                Debug.Log("Damage being done!");
            }
        
        if (currentHealthAmount <= 0)
        {
            Die();
        }

        StartCoroutine(TempInvicibility());
    }

    private IEnumerator TempInvicibility()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision detected!");
            TakeDamage(1);
        }
    }
    public void Die()
    {
        // Add animator stuff here
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
