﻿using System;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public enum PlayerState
{
    None,
    Player_Move,
    Player_Attack,
}

public class Player_Controller : Character_Controller<PlayerState>
{
    public Player_Input input { get; private set; }

    // 屏幕震动
    private CinemachineImpulseSource impulseSource;

    // 相机目标
    private Transform cameraTarget;
    private Vector3 cameraPos;

    // 普攻配置
    public Conf_SkillData[] attackConfs;

    // 当前所属普攻配置
    private int currAttackIndex = 0;
    public int CurrAttackIndex { get => currAttackIndex;
        set {
            if(value >= attackConfs.Length)
            {
                currAttackIndex = 0;
            }
            else
            {
                currAttackIndex = value;
            }
    }}

    public override int Hp { get => hp;
        set {
            hp = value;
            if(value < 0)
            {
                hp = 0;
                // 死亡事件
            }

            HPBarImg.fillAmount = hp / 100f;
        }
    }

    public static Player_Controller Instance;

    public Image HPBarImg;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        input = new Player_Input();

        impulseSource = GetComponent<CinemachineImpulseSource>();
        cameraTarget = transform.Find("CameraTarget");
        cameraPos = cameraTarget.localPosition;

        // 初始化移动状态
        UpdateState<Player_Move>(PlayerState.Player_Move, true);
    }

    protected override void Update()
    {
        base.Update();
        UpdateSkillCD();
    }


    #region 战斗相关

    public bool CheckAttack()
    {
        if(input.GetAttackKey() && model.canSwitch)
        {
            CurrSkillData = attackConfs[CurrAttackIndex];
            attackAction = StandAttack;
            currSkillIndex = -1;
            return true;
        }

        for(int i = 0; i < skillModels.Length; ++i)
        {
            if(input.GetKeyDown(skillModels[i].keyCode) && model.canSwitch && skillModels[i].CanRelease)
            {
                CurrSkillData = skillModels[i].skillData;
                attackAction = SkillAttack;
                currSkillIndex = i;
                return true;
            }
        }

        return false;
    }

    private UnityAction attackAction;
    public void Attack()
    {
        attackAction?.Invoke();
        if(currSkillIndex != -1)
        {
            skillModels[currSkillIndex].OnRelease();
        }
    }

    private void StandAttack()
    {
        model.Attack(CurrSkillData);
        ++CurrAttackIndex;
    }

    /// <summary>
    /// 技能攻击
    /// </summary>
    private void SkillAttack()
    {
        model.Attack(CurrSkillData);
        // 技能是否会打断这个普攻的顺序
        CurrAttackIndex = 0;
    }

    /// <summary>
    /// 屏幕震动
    /// </summary>
    public void ScreenImpluse()
    {
        impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// 基于攻击的相机移动
    /// </summary>
    public void CameraMoveForAttack(Vector3 offset, float time, float backTime)
    {
        // 花费time时间去pos
        cameraTarget.DOLocalMove(cameraPos + offset, time).onComplete = () => {
            cameraTarget.DOLocalMove(cameraPos,backTime);
        };
    }

    #endregion
}
