using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    Monster_None,
    Monster_Move,
    Monster_Idle,
    Monster_Attack,
    Monster_Hurt,
    Monster_Dead,
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
    // �������
    private NavMeshAgent navMeshAgent;

    public override int Hp { get => hp; set => hp = value; }

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // ��ʼ���ƶ�״̬
        UpdateState<Monster_Idle>(MonsterState.Monster_Idle, true);
    }

    protected override void Update()
    {
        base.Update();

        if (navMeshAgent == true)
            return;

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

    #region ����

    public void StartMove()
    {
        navMeshAgent.enabled = true;
    }

    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }

    public void SetNavigationTarget(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    #endregion

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
