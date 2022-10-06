using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "��������",menuName ="����/��������/��������")]
public class Conf_SkillData : ScriptableObject
{
    // ��������
    public new string name;

    // ���ܴ����Ķ���
    public string triggerName;

    // ���ܽ��������Ķ���
    public string overTriggerName;

    // ���λ�Ƶ�����-����
    public CameraMoveModel[] cameraMoveModels;

    // �ͷ�����
    public Skill_ReleaseModel releaseModel;

    // ��������
    public Skill_HitModel hitModel;

    // ��������
    public Skill_EndModel endModel;
}

/// <summary>
/// �����ͷ�
/// </summary>
[Serializable]
public class Skill_ReleaseModel
{
    // �������� / ������Ϸ����
    public Skill_SpawnObj spawnObj;
    // ������Ч
    public AudioClip audioClip;
    // �ܷ���ת
    public bool canRotate;
}

[Serializable]
public class Skill_HitModel
{
    // �˺���ֵ
    public int damageVal;
    // Ӳֱʱ��
    public float hardTime;
    // ���ɡ�����
    public Vector3 repelVelocity;
    // ���ɡ����˵Ĺ���ʱ��
    public float repelTransitionTime;
    // �Ƿ���Ҫ��Ļ��
    public bool needScreenImpulse;
    // �Ƿ���Ҫ��Ļɫ��Ч���������ڻ���һ˲�����Ļ��
    public bool needChromaticAberration;
    // ����Ч��
    public Conf_SkillHitEF skillHitEF;
}

[Serializable]
public class Skill_EndModel
{
    // �������� / ������Ϸ����
    public Skill_SpawnObj spawnObj;
}

/// <summary>
/// ���ܲ�������
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    // ���ɵ�Ԥ����
    public GameObject prefab;
    // ���ɵ���Ч
    public AudioClip audioClip;
    // λ��
    public Vector3 position;
    // ��ת
    public Vector3 rotation;
}

[Serializable]
public class CameraMoveModel
{
    // ���ƫ�Ƴ̶�
    public Vector3 target;

    // ���ƫ�ƣ�ƽ��ʱ��
    public float time;

    // �ع�ʱ��
    public float backTime;
}