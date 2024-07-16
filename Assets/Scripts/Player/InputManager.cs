using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public static PlayerInput Input { get; }

    static InputManager()
    {
        Input = new PlayerInput();
        EnableInput();
    }

    public InputManager()
    {
        
    }

    public static void EnableInput() => Input.Enable();
    public static void DisableInput() => Input.Disable();
}
