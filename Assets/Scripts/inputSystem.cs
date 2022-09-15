using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Heal(Input.Action.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Heal");
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if(keyboard != null)
        {
            if(keyboard.spaceKey.wasReleasedThisFrame)
            {
                Debug.Log("Space");
            }
        }

        Gamepad gamepad = Gamepad.current;

        if(gamepad == null)
            return;
        
        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            Debug.Log("right shoulder pressed!");
        }
    }
}
