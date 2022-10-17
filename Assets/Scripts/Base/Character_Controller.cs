using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Controller<T> : FSMController<T>
{
    public new AudioController audio { get; protected set; }
    public Character_Model<T> model { get; protected set; }
    public CharacterController characterController { get; protected set; }

    // 生命值
    protected int hp = 100;
    public abstract int Hp { set; get; }

    // 技能数据
    public SkillModel[] skillModels;

    protected virtual void Start()
    {
        audio = new AudioController(GetComponent<AudioSource>());
        characterController = GetComponent<CharacterController>();
        model = transform.Find("Model").GetComponent<Character_Model<T>>();
        model.Init(this);
    }

    /// <summary>
    /// 更新技能CD时间
    /// </summary>
    public void UpdateSkillCD()
    {
        for (int i = 0; i < skillModels.Length; ++i)
        {
            skillModels[i].Update();
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip"></param>
    public void PlayAudio(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    #region 战斗相关

    // 当前的技能编号
    protected int currSkillIndex = -1;

    // 当前技能
    public Conf_SkillData CurrSkillData { get; protected set; }

    /// <summary>
    /// 角色攻击移动
    /// </summary>
    public void CharacterAttackMove(Vector3 target, float time)
    {
        StartCoroutine(DoCharacterAttackMove(transform.TransformDirection(target), time));
    }
    IEnumerator DoCharacterAttackMove(Vector3 target, float time)
    {
        float currTime = 0;
        while (currTime < time)
        {
            var moveDir = target * Time.deltaTime / time;
            characterController.Move(moveDir);
            currTime += Time.deltaTime;
            // 停一帧
            yield return null;
        }
    }


    public void Hurt(float hardTime, Transform sourceTran, Vector3 repelVelocity, float repelTransition, int damageVal)
    {
        // 仅动画
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver");
        Invoke("HurtOver", hardTime); // 延迟调用

        OnHurt(sourceTran, repelVelocity, repelTransition);

        hp -= damageVal;
    }

    protected virtual void OnHurt(Transform sourceTran, Vector3 repelVelocity, float repelTransition) { }

    public void HurtOver()
    {
        model.StopHurtAnimation();
    }

    #endregion
}
