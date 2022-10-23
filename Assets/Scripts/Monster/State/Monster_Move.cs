using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : Monster_StateBase
{
    public override void OnEnter()
    {
        monster.StartMove();
        model.SetAnimation("≈‹", true);
    }

    public override void OnLeave()
    {
        monster.StopMove();
        model.SetAnimation("≈‹", false);
    }

    public override void OnUpdate()
    {
        if (player.isDead)
        {
            monster.UpdateState<Monster_Idle>(MonsterState.Monster_Idle);
            return;
        }

        monster.SetNavigationTarget(player.transform.position);
        // ºÏ≤‚æ‡¿Î
        var dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            // «–ªªπ•ª˜◊¥Ã¨
            monster.UpdateState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        else if (dis > 6)
        {
            monster.UpdateState<Monster_Idle>(MonsterState.Monster_Idle);
        }
    }
}
