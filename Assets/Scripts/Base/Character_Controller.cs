using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Controller<T> : FSMController<T>
{
    public new AudioController audio { get; protected set; }
    public Character_Model<T> model { get; protected set; }
    public CharacterController characterController { get; protected set; }

    // ����ֵ
    protected int hp = 100;
    public abstract int Hp { set; get; }
    public bool isDead { get; protected set; } = false;

    // ��������
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
    /// ���¼���CDʱ��
    /// </summary>
    public void UpdateSkillCD()
    {
        for (int i = 0; i < skillModels.Length; ++i)
        {
            skillModels[i].Update();
        }
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="clip"></param>
    public void PlayAudio(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    #region ս�����

    // ��ǰ����
    public Conf_SkillData CurrSkillData { get; protected set; }

    /// <summary>
    /// ��ɫ�����ƶ�
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
            // ͣһ֡
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
            // ������
            model.PlayHurtAnimation(repelVelocity.y > 0.5f);
            CancelInvoke("HurtOver");
            Invoke("HurtOver", hardTime); // �ӳٵ���

            OnHurt(sourceTran, repelVelocity, repelTransition);
        }
       
        // ����ͷż��ܵ�һ���ˣ�������û��ߴ�������ĵ�����Ч�������ͷŵȵ�
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
