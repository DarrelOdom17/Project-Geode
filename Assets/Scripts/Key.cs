using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tag tripped");
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        Debug.Log("Tag tripped1");

        if (other.gameObject.name.Equals("Player"))
        {
            playerInventory.KeysCollected();
            gameObject.SetActive(false);
        }
    }
}
