using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter()
    {
        //model.SetAnimation("��", false);
        //monster.StopMove();
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        // ������
        var dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            // ��
        }
        else if (dis >= 1 && dis < 6)
        {
            // ׷����������
            monster.UpdateState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
