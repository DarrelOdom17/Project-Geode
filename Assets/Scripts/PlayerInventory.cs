using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   public int NumberOfKeys { get; private set; }

   public void KeysCollected()
   {
        NumberOfKeys++;
   }
}
