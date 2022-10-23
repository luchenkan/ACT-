using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Hurt : Monster_StateBase
{
    // 打击力量(描述击退程度)
    private Vector3 hurtForce;
    // 打击时间
    private float hurtTime;
    // 当前时间
    private float curHurtTime;
    
    public void SetData(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        // 击退和击飞
        hurtForce = sourceTran.TransformDirection(repelVelocity);
        hurtTime = repelTransition;
        curHurtTime = 0;
    }

    public override void OnEnter()
    {
        isStop = false;
    }

    public override void OnLeave()
    {
        
    }

    private bool isStop = false;
    public override void OnUpdate()
    {
        if (isStop) return;

        if(curHurtTime < hurtTime)
        {
            curHurtTime += Time.deltaTime;
            // 利用时间进行移动
            monster.moveMotion = hurtForce / hurtTime;
        }
        else
        {
            monster.StopRepel();
            isStop = true;
        }
    }
}
