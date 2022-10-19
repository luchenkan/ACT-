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
    // 是否被打
    private bool isHurt;
    // 打击力量(描述击退程度)
    private Vector3 hurtForce;
    // 打击时间
    private float hurtTime;
    // 当前时间
    private float curHurtTime;
    // 导航组件
    private NavMeshAgent navMeshAgent;

    public override int Hp { get => hp; set => hp = value; }

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 初始化移动状态
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
            // 此处只是为了模拟重力
            characterController.Move(new Vector3(0, -9, 0) * Time.deltaTime);
        }
    }

    #region 导航

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

    #region 战斗逻辑

    protected override void OnHurt(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        base.OnHurt(sourceTran, repelVelocity, repelTransition);

        // 击退 击飞
        isHurt = true;
        hurtForce = sourceTran.TransformDirection(repelVelocity);
        hurtTime = repelTransition;
        curHurtTime = 0;
    }

    #endregion
}
