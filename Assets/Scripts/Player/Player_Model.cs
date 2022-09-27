using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ����صĶ�����
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

        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
    }

    public void Attack()
    {
        animator.SetBool("����", true);
    }

    #region �����¼�����

    private void SkillOver()
    {
        animator.SetBool("����", false);
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
