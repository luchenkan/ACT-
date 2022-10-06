using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider boxCollider;
    public MeleeWeaponTrail meleeWeaponTrail;

    private List<GameObject> monsterList = new List<GameObject>();

    private Player_Model model;

    // 当前技能的命中数据
    private Skill_HitModel hitModel;

    public void Init(Player_Model model)
    {
        this.model = model;
        StopSkillHit();
    }

    public void StartSkillHit(Skill_HitModel hitModel)
    {
        this.hitModel = hitModel;
        boxCollider.enabled = true;
        meleeWeaponTrail.Emit = true;
    }

    public void StopSkillHit()
    {
        boxCollider.enabled = false;
        monsterList.Clear();
        meleeWeaponTrail.Emit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster" && !monsterList.Contains(other.gameObject))
        {
            // 伤害决定
            monsterList.Add(other.gameObject);
            // 处理该次伤害逻辑
            other.GetComponent<Monster_Controller>().Hurt(hitModel.hardTime, model.transform, hitModel.repelVelocity, hitModel.repelTransitionTime, hitModel.damageVal);
            if (hitModel.skillHitEF != null)
            {
                // 生成粒子 ClosestPointOnBounds是获取这个坐标和触发器的碰撞点
                SpawnObjectByHit(hitModel.skillHitEF.spawn, other.ClosestPointOnBounds(transform.position));
                // 播放命中音效
                if(hitModel.skillHitEF.audioClip != null)
                {
                    model.PlayAudio(hitModel.skillHitEF.audioClip);
                }
            }

            // 震动
            if(hitModel.needScreenImpulse)
            {
                model.ScreenImpulse();
            }

            if(hitModel.needChromaticAberration)
            {
                PostProcessingManager.Instance.ChromaticAberrationEF(); // 这个地方我觉得直接调单例类是可以的
            }
        }
    }

    private void SpawnObjectByHit(Skill_SpawnObj spawn,Vector3 spawnPosition)
    {
        if(spawn != null && spawn.prefab != null)
        {
            var temp = GameObject.Instantiate(spawn.prefab,null);
            var tranform = temp.transform;
            tranform.position = spawnPosition + spawn.position;
            transform.LookAt(Camera.main.transform);
            transform.eulerAngles += spawn.rotation;
            model.PlayAudio(spawn.audioClip);
        }
    }
}
