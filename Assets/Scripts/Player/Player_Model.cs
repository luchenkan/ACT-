using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ����صĶ�����
/// </summary>
public class Player_Model : Character_Model<PlayerState>
{
    private Player_Controller player;

    public void UpdateMove(float x,float y)
    {

        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
    }

    public override void Init(Character_Controller<PlayerState> character)
    {
        base.Init(character);
        player = character as Player_Controller;
    }

    #region �����¼�����

    /// <summary>
    /// ����ƶ�������indexλ��λ��Ч��
    /// </summary>
    /// <param name="index"></param>
    private void CameraMoveForAttack(int index)
    {
        if (index >= skillData.cameraMoveModels.Length)
            return;

        var model = skillData.cameraMoveModels[index];
        player.CameraMoveForAttack(model.target, model.time, model.backTime);
    }

    #endregion

    /// <summary>
    /// ��Ļ��
    /// </summary>
    public void ScreenImpulse()
    {
        player.ScreenImpluse();
    }

    protected override void OnSkillOver()
    {
        player.CurrAttackIndex = 0;
        player.UpdateState<Player_Move>(PlayerState.Player_Move);
    }
}
