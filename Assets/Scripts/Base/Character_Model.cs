using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Model : MonoBehaviour
{
    // 这个作用是为了武器层能有一个character_model，而不用指定具体的泛型（但是可以这么解决，有点神奇）
    public abstract void PlayAudio(AudioClip clip);
    public abstract void SpawnObject(Skill_SpawnObj spawn);
    public List<string> EnemyTargetNames;
}

public class Character_Model<T> : Character_Model
{
    protected Character_Controller<T> character;
    protected Animator animator;
    public WeaponCollider[] boxColliders;

    // 当前技能数据
    protected Conf_SkillData skillData;

    // 是否可以切换普攻阶段(默认为true是从待机到攻击默认是可以转换的)
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

        /////////注意此处属于释放时的技能
        // 单次攻击生成
        SpawnObject(skillData.releaseModel.spawnObj);
        // 音效
        PlayAudio(skillData.releaseModel.audioClip);
    }

    public override void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.prefab != null)
        {
            var temp = GameObject.Instantiate(spawn.prefab, null);
            var tranform = temp.transform;
            tranform.position = tranform.position + spawn.position;
            transform.eulerAngles = character.transform.eulerAngles + spawn.rotation; // 以玩家位置为基准
            PlayAudio(spawn.audioClip);
        }
    }

    /// <summary>
    /// 生成游戏对象（具体某一个索引）
    /// </summary>
    /// <param name="index"></param>
    private void SpawnObj(int index)
    {
        SpawnObject(skillData.spawnObjs[index]);
    }

    private int currHurtAnimationIndex = 1;
    public void PlayHurtAnimation()
    {
        animator.SetTrigger("受伤" + currHurtAnimationIndex);
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
        animator.SetTrigger("受伤结束");
    }

    public void SetAnimation(string name,bool flag)
    {
        animator.SetBool(name, flag);
    }

    #region 动画事件调用

    /// <summary>
    /// 普攻阶段切换
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
        // 基于结束配置，生成一些事物
        SpawnObject(skillData.endModel.spawnObj);
        canSwitch = true;
        animator.SetTrigger(skillData.overTriggerName);
        OnSkillOver();
    }

    private void StartSkillHit(int weaponIndex)
    {
        // 开启伤害检测
        boxColliders[weaponIndex].StartSkillHit(skillData.hitModels[currHitIndex]);

        // 单次攻击生成
        SpawnObject(skillData.hitModels[currHitIndex].spawnObj);

        // 音效
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
