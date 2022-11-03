using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRandom : MonoBehaviour
{
    public List<RandomItem> items;

    // Start is called before the first frame update
    void Awake()
    {
        if (items == null)
            items = new List<RandomItem>();

    }

    public Sound PickSound()
    {
        if (items.Count == 0)
            return null;
        
        // Get number of valid points to randomize through
        int numOfValidPoints = 0;
        for (int i = 0; i < items.Count; i++)
            if (items[i].count > 0)
                numOfValidPoints += items[i].count;

        // Check for reset
        if (numOfValidPoints < 1)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].count = items[i].defaultCount;
                numOfValidPoints += items[i].count;
            }
        }

        // random
        int randomIndex = Random.Range(0, numOfValidPoints);

        int validPoints = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].count > 0)
            {
                validPoints += items[i].count;
            }

            if (validPoints > randomIndex)
            {
                items[i].count--;
                return items[i].sound;
            }
        }

        return null;
    }
}

[System.Serializable]
public class RandomItem
{
    public int count;
    public int defaultCount;
    public Sound sound;
}
