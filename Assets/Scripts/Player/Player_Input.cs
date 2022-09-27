using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input
{
    public float Horizeontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }

    private KeyCode runKeyCode = KeyCode.LeftShift;
    private KeyCode attackKeyCode = KeyCode.J;

    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool GetRunKey()
    {
        return GetKey(runKeyCode); 
    }

    public bool GetAttackKey()
    {
        return GetKeyDown(attackKeyCode);
    }
}
