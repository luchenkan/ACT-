using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{

}

public class Monster_Controller : Character_Controller<MonsterState>
{
    // �Ƿ񱻴�
    private bool isHurt;
    // �������(�������˳̶�)
    private Vector3 hurtForce;
    // ���ʱ��
    private float hurtTime;
    // ��ǰʱ��
    private float curHurtTime;

    public override int Hp { get => hp; set => hp = value; }

    protected override void Update()
    {
        if(isHurt)
        {
            curHurtTime += Time.deltaTime;
            characterController.Move(hurtForce * Time.deltaTime / hurtTime);
            if(curHurtTime >= hurtTime)
            {
                isHurt = false;
            }
        }
        else
        {
            // �˴�ֻ��Ϊ��ģ������
            characterController.Move(new Vector3(0, -9, 0) * Time.deltaTime);
        }
    }

    #region ս���߼�

    protected override void OnHurt(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        base.OnHurt(sourceTran, repelVelocity, repelTransition);

        // ���� ����
        isHurt = true;
        hurtForce = sourceTran.TransformDirection(repelVelocity);
        hurtTime = repelTransition;
        curHurtTime = 0;
    }

    #endregion
}
