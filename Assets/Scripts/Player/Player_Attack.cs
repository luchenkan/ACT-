using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateBase<PlayerState>
{
    public Player_Controller player;

    public override void OnEnter()
    {
        // ¹¥»÷
        Attack();
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public override void Init(PlayerState state, FSMController<PlayerState> controller)
    {
        base.Init(state, controller);

        player = controller as Player_Controller;
    }

    public void Attack()
    {
        player.model.Attack();
    }
}
