using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "技能配置",menuName ="配置/技能配置/技能数据")]
public class Conf_SkillData : ScriptableObject
{
    // 技能名称
    public new string name;

    // 技能触发的动画
    public string triggerName;

    // 技能结束触发的动画
    public string overTriggerName;

    // 相机位移的数据-集合
    public CameraMoveModel[] cameraMoveModels;

    // 释放数据
    public Skill_ReleaseModel releaseModel;

    // 命中数据
    public Skill_HitModel hitModel;

    // 结束数据
    public Skill_EndModel endModel;
}

/// <summary>
/// 技能释放
/// </summary>
[Serializable]
public class Skill_ReleaseModel
{
    // 播放粒子 / 产生游戏物体
    public Skill_SpawnObj spawnObj;
    // 播放音效
    public AudioClip audioClip;
    // 能否旋转
    public bool canRotate;
}

[Serializable]
public class Skill_HitModel
{
    // 伤害数值
    public int damageVal;
    // 硬直时间
    public float hardTime;
    // 击飞、鸡腿
    public Vector3 repelVelocity;
    // 击飞、击退的过渡时间
    public float repelTransitionTime;
    // 是否需要屏幕震动
    public bool needScreenImpulse;
    // 是否需要屏幕色差效果（体现在击打一瞬间的屏幕）
    public bool needChromaticAberration;
    // 命中效果
    public Conf_SkillHitEF skillHitEF;
}

[Serializable]
public class Skill_EndModel
{
    // 播放粒子 / 产生游戏物体
    public Skill_SpawnObj spawnObj;
}

/// <summary>
/// 技能产生物体
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    // 生成的预制体
    public GameObject prefab;
    // 生成的音效
    public AudioClip audioClip;
    // 位置
    public Vector3 position;
    // 旋转
    public Vector3 rotation;
}

[Serializable]
public class CameraMoveModel
{
    // 相机偏移程度
    public Vector3 target;

    // 多久偏移，平滑时间
    public float time;

    // 回归时间
    public float backTime;
}