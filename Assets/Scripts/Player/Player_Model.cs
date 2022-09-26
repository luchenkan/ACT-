using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ����صĶ�����
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
        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
    }
}
