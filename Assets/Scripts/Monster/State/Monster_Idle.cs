using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter()
    {

    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        if(monster.isDead)
        {
            return;
        }

        // ºÏ≤‚æ‡¿Î
        var dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            // ¥Ú
            monster.UpdateState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        else if (dis >= 1 && dis < 6)
        {
            // ◊∑£¨ƒ‹À—À˜µΩ
            monster.UpdateState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
