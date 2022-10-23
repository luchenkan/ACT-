using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Model : Character_Model<MonsterState>
{
    private Monster_Controller monster;
    public override void Init(Character_Controller<MonsterState> character)
    {
        base.Init(character);
        monster = character as Monster_Controller;
    }

    protected override void OnSkillOver()
    {
        monster.UpdateState<Monster_Idle>(MonsterState.Monster_Idle);
    }
}
