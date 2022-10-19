using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster_StateBase : StateBase<MonsterState>
{
    protected Monster_Controller monster;
    protected Monster_Model model;
    protected Player_Controller player;

    protected override void OnInit(MonsterState state, FSMController<MonsterState> controller)
    {
        base.OnInit(state, controller);
        monster = controller as Monster_Controller;
        model = monster.model as Monster_Model;
        player = Player_Controller.Instance;
    }
}
