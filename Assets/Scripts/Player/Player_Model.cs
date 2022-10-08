using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制模型相关的动作等
/// </summary>
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider[] boxColliders;

    // 当前技能数据
    private Conf_SkillData skillData;

    // 是否可以切换普攻阶段(默认为true是从待机到攻击默认是可以转换的)
    public bool canSwitch { get; private set; } = true;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        for(int i = 0; i < boxColliders.Length; ++i)
        {
            boxColliders[i].Init(this);
        }
    }

    public void PlayAudio(AudioClip audioClip)
    {
        player.PlayAudio(audioClip);
    }

    public void UpdateMove(float x,float y)
    {

        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }

    public void Attack(Conf_SkillData conf)
    {
        currHitIndex = 0;
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(conf.triggerName);
    }

    private void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.prefab != null)
        {
            var temp = GameObject.Instantiate(spawn.prefab, null);
            var tranform = temp.transform;
            tranform.position = tranform.position + spawn.position;
            transform.eulerAngles = player.transform.eulerAngles + spawn.rotation; // 以玩家位置为基准
            PlayAudio(spawn.audioClip);
        }
    }

    #region 动画事件调用

    private void SkillOver(string skillName)
    {
        if(skillName != skillData.name)
        {
            return;
        }
        // 基于结束配置，生成一些事物
        SpawnObject(skillData.endModel.spawnObj);
        canSwitch = true;
        animator.SetTrigger(skillData.overTriggerName);
        player.CurrAttackIndex = 0;
        player.UpdateState<Player_Move>(PlayerState.Player_Move);
    }

    private int currHitIndex = 0;
    private void StartSkillHit(int weaponIndex)
    {
        // 开启伤害检测
        boxColliders[weaponIndex].StartSkillHit(skillData.hitModels[currHitIndex]);
        ++currHitIndex;

        // 生成释放时的游戏物体/粒子
        SpawnObject(skillData.releaseModel.spawnObj);

        // 音效
        PlayAudio(skillData.releaseModel.audioClip);
    }

    private void StopSkillHit(int weaponIndex)
    {
        boxColliders[weaponIndex].StopSkillHit();
    }

    /// <summary>
    /// 普攻阶段切换
    /// </summary>
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }

    /// <summary>
    /// 相机移动，基于index位的位移效果
    /// </summary>
    /// <param name="index"></param>
    private void CameraMoveForAttack(int index)
    {
        var model = skillData.cameraMoveModels[index];
        player.CameraMoveForAttack(model.target, model.time, model.backTime);
    }

    private void CharacterMoveForAttack(int index)
    {
        var model = skillData.characterMoveModels[index];
        player.CharacterAttackMove(model.target, model.time);
    }

    private void CharacterMoveForAttack()
    {

    }

    #endregion

    /// <summary>
    /// 屏幕震动
    /// </summary>
    public void ScreenImpulse()
    {
        player.ScreenImpluse();
    }
}
