using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Hurt : Monster_StateBase
{
    // �������(�������˳̶�)
    private Vector3 hurtForce;
    // ���ʱ��
    private float hurtTime;
    // ��ǰʱ��
    private float curHurtTime;
    
    public void SetData(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        // ���˺ͻ���
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
            // ����ʱ������ƶ�
            monster.moveMotion = hurtForce / hurtTime;
        }
        else
        {
            monster.StopRepel();
            isStop = true;
        }
    }
}
