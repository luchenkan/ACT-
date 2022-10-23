using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : Monster_StateBase
{
    // ÊÇ·ñ¹¥»÷
    private bool isAttack;
    public override void OnEnter()
    {
        monster.StopMove();
        isAttack = false;
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        if(!isAttack && !player.isDead)
        {
            isAttack = monster.Attack();
        }
    }
}
