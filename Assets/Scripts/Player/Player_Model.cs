using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制模型相关的动作等
/// </summary>
public class Player_Model : MonoBehaviour
{
    private Player_Controller controller;
    private Animator animator;

    public void Init(Player_Controller controller)
    {
        this.controller = controller;
        animator = GetComponent<Animator>();
    }

    public void UpdateMove(float x,float y)
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }
}
