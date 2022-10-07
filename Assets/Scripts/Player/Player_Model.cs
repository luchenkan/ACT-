using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģ����صĶ�����
/// </summary>
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider boxCollider;

    // ��ǰ��������
    private Conf_SkillData skillData;

    // �Ƿ�����л��չ��׶�(Ĭ��Ϊtrue�ǴӴ���������Ĭ���ǿ���ת����)
    public bool canSwitch { get; private set; } = true;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        boxCollider.Init(this);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        player.PlayAudio(audioClip);
    }

    public void UpdateMove(float x,float y)
    {

        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
    }

    public void Attack(Conf_SkillData conf)
    {
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
            transform.eulerAngles = player.transform.eulerAngles + spawn.rotation; // �����λ��Ϊ��׼
            PlayAudio(spawn.audioClip);
        }
    }

    #region �����¼�����

    private void SkillOver(string skillName)
    {
        if(skillName != skillData.name)
        {
            return;
        }
        // ���ڽ������ã�����һЩ����
        SpawnObject(skillData.endModel.spawnObj);
        canSwitch = true;
        animator.SetTrigger(skillData.overTriggerName);
        player.CurrAttackIndex = 0;
        player.UpdateState<Player_Move>(PlayerState.Player_Move);
    }

    private void StartSkillHit()
    {
        // �����˺����
        boxCollider.StartSkillHit(skillData.hitModel);

        // �����ͷ�ʱ����Ϸ����/����
        SpawnObject(skillData.releaseModel.spawnObj);

        // ��Ч
        PlayAudio(skillData.releaseModel.audioClip);
    }

    private void StopSkillHit()
    {
        boxCollider.StopSkillHit();
    }

    /// <summary>
    /// �չ��׶��л�
    /// </summary>
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }

    /// <summary>
    /// ����ƶ�������indexλ��λ��Ч��
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

    #endregion

    /// <summary>
    /// ��Ļ��
    /// </summary>
    public void ScreenImpulse()
    {
        player.ScreenImpluse();
    }
}
