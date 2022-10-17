using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制模型相关的动作等
/// </summary>
public class Player_Model : Character_Model<PlayerState>
{
    private Player_Controller player;

    public void UpdateMove(float x,float y)
    {

        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }

    public override void Init(Character_Controller<PlayerState> character)
    {
        base.Init(character);
        player = character as Player_Controller;
    }

    #region 动画事件调用

    /// <summary>
    /// 相机移动，基于index位的位移效果
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
    /// 屏幕震动
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
