using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateBase<PlayerState>
{
    public Player_Controller player;

    public override void OnEnter()
    {
        // ¹¥»÷
        player.Attack();
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        // ¼ì²â¶þ¶Î¹¥
        if(player.CheckAttack())
        {
            player.Attack();
        }

        // ×ªÏò
        if(player.CurrSkillData.releaseModel.canRotate)
        {
            // Ðý×ª
            var rot = new Vector3(0, player.input.Horizeontal, 0);
            player.transform.Rotate(rot * 60 * Time.deltaTime);
        }
    }

    public override void Init(PlayerState state, FSMController<PlayerState> controller)
    {
        base.Init(state, controller);

        player = controller as Player_Controller;
    }
}
