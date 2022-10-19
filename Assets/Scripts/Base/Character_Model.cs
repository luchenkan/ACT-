using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Model : MonoBehaviour
{
    // ���������Ϊ������������һ��character_model��������ָ������ķ��ͣ����ǿ�����ô������е����棩
    public abstract void PlayAudio(AudioClip clip);
    public abstract void SpawnObject(Skill_SpawnObj spawn);
    public List<string> EnemyTargetNames;
}

public class Character_Model<T> : Character_Model
{
    protected Character_Controller<T> character;
    protected Animator animator;
    public WeaponCollider[] boxColliders;

    // ��ǰ��������
    protected Conf_SkillData skillData;

    // �Ƿ�����л��չ��׶�(Ĭ��Ϊtrue�ǴӴ���������Ĭ���ǿ���ת����)
    public bool canSwitch { get; protected set; } = true;

    public virtual void Init(Character_Controller<T> character)
    {
        this.character = character;
        animator = GetComponent<Animator>();
        for (int i = 0; i < boxColliders.Length; ++i)
        {
            boxColliders[i].Init(this);
        }
    }

    public override void PlayAudio(AudioClip audioClip)
    {
        character.PlayAudio(audioClip);
    }

    protected int currHitIndex = 0;
    public void Attack(Conf_SkillData conf)
    {
        currHitIndex = 0;
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(conf.triggerName);

        /////////ע��˴������ͷ�ʱ�ļ���
        // ���ι�������
        SpawnObject(skillData.releaseModel.spawnObj);
        // ��Ч
        PlayAudio(skillData.releaseModel.audioClip);
    }

    public override void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.prefab != null)
        {
            var temp = GameObject.Instantiate(spawn.prefab, null);
            var tranform = temp.transform;
            tranform.position = tranform.position + spawn.position;
            transform.eulerAngles = character.transform.eulerAngles + spawn.rotation; // �����λ��Ϊ��׼
            PlayAudio(spawn.audioClip);
        }
    }

    /// <summary>
    /// ������Ϸ���󣨾���ĳһ��������
    /// </summary>
    /// <param name="index"></param>
    private void SpawnObj(int index)
    {
        SpawnObject(skillData.spawnObjs[index]);
    }

    private int currHurtAnimationIndex = 1;
    public void PlayHurtAnimation()
    {
        animator.SetTrigger("����" + currHurtAnimationIndex);
        if (currHurtAnimationIndex == 1)
        {
            currHurtAnimationIndex = 2;
        }
        else
        {
            currHurtAnimationIndex = 1;
        }
    }

    public void StopHurtAnimation()
    {
        animator.SetTrigger("���˽���");
    }

    public void SetAnimation(string name,bool flag)
    {
        animator.SetBool(name, flag);
    }

    #region �����¼�����

    /// <summary>
    /// �չ��׶��л�
    /// </summary>
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }

    private void CharacterMoveForAttack(int index)
    {
        if (index >= skillData.characterMoveModels.Length)
            return;

        var model = skillData.characterMoveModels[index];
        character.CharacterAttackMove(model.target, model.time);
    }

    private void SkillOver(string skillName)
    {
        if (skillName != skillData.name)
        {
            return;
        }
        // ���ڽ������ã�����һЩ����
        SpawnObject(skillData.endModel.spawnObj);
        canSwitch = true;
        animator.SetTrigger(skillData.overTriggerName);
        OnSkillOver();
    }

    private void StartSkillHit(int weaponIndex)
    {
        // �����˺����
        boxColliders[weaponIndex].StartSkillHit(skillData.hitModels[currHitIndex]);

        // ���ι�������
        SpawnObject(skillData.hitModels[currHitIndex].spawnObj);

        // ��Ч
        PlayAudio(skillData.hitModels[currHitIndex].audioClip);

        ++currHitIndex;
    }

    private void StopSkillHit(int weaponIndex)
    {
        boxColliders[weaponIndex].StopSkillHit();
    }

    protected virtual void OnSkillOver() { }

    #endregion
}
