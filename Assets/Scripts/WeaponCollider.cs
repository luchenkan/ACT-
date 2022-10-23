using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider boxCollider;
    public MeleeWeaponTrail meleeWeaponTrail;

    private List<GameObject> enemyList = new List<GameObject>();

    private Character_Model model;

    // ��ǰ���ܵ���������
    private Skill_HitModel hitModel;

    public void Init(Character_Model model)
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
        enemyList.Clear();
        meleeWeaponTrail.Emit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(model.EnemyTargetNames.Contains(other.tag) && !enemyList.Contains(other.gameObject))
        {
            // �˺�����
            enemyList.Add(other.gameObject);
            // ����ô��˺��߼�
            other.GetComponent<HurtEnter>().Hurt(hitModel.hardTime, model.transform, hitModel.repelVelocity, hitModel.repelTransitionTime, hitModel.damageVal);
            if (hitModel.skillHitEF != null)
            {
                // �������� ClosestPointOnBounds�ǻ�ȡ�������ʹ���������ײ��
                SpawnObjectByHit(hitModel.skillHitEF.spawn, other.ClosestPointOnBounds(transform.position));
                // ����������Ч
                if(hitModel.skillHitEF.audioClip != null)
                {
                    model.PlayAudio(hitModel.skillHitEF.audioClip);
                }
            }

            if(model is Player_Model)
            {
                var player_model = model as Player_Model;
                // ��
                if (hitModel.needScreenImpulse)
                {
                    player_model.ScreenImpulse();
                }

                if (hitModel.needChromaticAberration)
                {
                    PostProcessingManager.Instance.ChromaticAberrationEF(); // ����ط��Ҿ���ֱ�ӵ��������ǿ��Ե�
                }
            }

            model.SpawnObject(hitModel.spawnObj);
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
