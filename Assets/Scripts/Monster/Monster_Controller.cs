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
    Monster_Hurt
}

public class Monster_Controller : Character_Controller<MonsterState>
{
    // �������
    private NavMeshAgent navMeshAgent;

    // �ƶ��ı���
    public Vector3 moveMotion = new Vector3(0, -9, 0);

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

        if (isDead)
        {
            characterController.Move(moveMotion * Time.deltaTime);
            return;
        }

        if (navMeshAgent.enabled == true)
            return;

        characterController.Move(moveMotion * Time.deltaTime);
    }

    public void StopRepel()
    {
        moveMotion.Set(0, -9, 0);
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

    protected override void OnHurtOver()
    {
        UpdateState<Monster_Idle>(MonsterState.Monster_Idle);
    }

    protected override void OnHurt(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        UpdateState<Monster_Hurt>(MonsterState.Monster_Hurt, true);
        (curStateObject as Monster_Hurt).SetData(sourceTran, repelVelocity, repelTransition);
    }

    protected override void OnDead()
    {
        StopRepel();
        UpdateState<Monster_Idle>(MonsterState.Monster_Idle);
        StopMove();
        Destroy(GetComponent<CapsuleCollider>());
        //Destroy(GetComponent<CharacterController>());
        Invoke("Destroy", 3);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public bool Attack()
    {
        if (!model.canSwitch)
            return false;

         // ��ʲô���ܷ�ʲô����
         for(int i = 0; i < skillModels.Length; ++i)
        {
            if (skillModels[i].CanRelease)
            {
                CurrSkillData = skillModels[i].skillData;
                model.Attack(CurrSkillData);
                skillModels[i].OnRelease();
                return true;
            }
        }
        return false;
    }

    #endregion
}
