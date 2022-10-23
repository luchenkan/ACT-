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
    public bool isDead { get; protected set; } = false;

    // 技能数据
    public SkillModel[] skillModels;

    protected virtual void Start()
    {
        audio = new AudioController(GetComponent<AudioSource>());
        characterController = GetComponent<CharacterController>();
        model = transform.Find("Model").GetComponent<Character_Model<T>>();
        model.Init(this);
        gameObject.AddComponent<HurtEnter>().action = Hurt;
    }

    protected override void Update()
    {
        base.Update();
        UpdateSkillCD();
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

    // 当前技能
    public Conf_SkillData CurrSkillData { get; protected set; }

    /// <summary>
    /// 角色攻击移动
    /// </summary>
    public void CharacterAttackMove(Vector3 target, float time)
    {
        StartCoroutine(DoCharacterAttackMove(transform.TransformDirection(target), time));
    }
    protected IEnumerator DoCharacterAttackMove(Vector3 target, float time)
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
        if (isDead) return;

        Hp -= damageVal;
        if(Hp <= 0)
        {
            Dead();
        }
        else
        {
            // 仅动画
            model.PlayHurtAnimation(repelVelocity.y > 0.5f);
            CancelInvoke("HurtOver");
            Invoke("HurtOver", hardTime); // 延迟调用

            OnHurt(sourceTran, repelVelocity, repelTransition);
        }
       
        // 如果释放技能到一半了，如何重置或者打断武器的刀光特效，技能释放等等
        model.SkillCanSwitch();
        model.ResetWeapon();
    }

    protected abstract void OnHurt(Transform sourceTran, Vector3 repelVelocity, float repelTransition);

    public void HurtOver()
    {
        model.StopHurtAnimation();
        OnHurtOver();
    }

    protected abstract void OnHurtOver();

    private void Dead()
    {
        isDead = true;
        model.PlayDeadAnimation();
        OnDead();
    }
    protected abstract void OnDead();

    #endregion
}
