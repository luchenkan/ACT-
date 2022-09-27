using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制模型相关的动作等
/// </summary>
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider boxCollider;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        boxCollider.Init();
    }

    public void UpdateMove(float x,float y)
    {

        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }

    public void Attack()
    {
        animator.SetBool("攻击", true);
    }

    #region 动画事件调用

    private void SkillOver()
    {
        animator.SetBool("攻击", false);
        player.UpdateState<Player_Move>(PlayerState.Player_Move);
    }

    private void StartSkillHit()
    {
        boxCollider.StartSkillHit();
    }

    private void StopSkillHit()
    {
        boxCollider.StopSkillHit();
    }

    #endregion
}
