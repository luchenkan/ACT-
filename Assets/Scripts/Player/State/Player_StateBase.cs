using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_StateBase : StateBase<PlayerState>
{
    protected Player_Controller player;
    protected Player_Model model;

    public abstract override void OnEnter();

    public abstract override void OnLeave();

    public abstract override void OnUpdate();

    protected override void OnInit(PlayerState state, FSMController<PlayerState> controller)
    {
        base.OnInit(state, controller);
        player = controller as Player_Controller;
        model = player.model as Player_Model;
    }
}
