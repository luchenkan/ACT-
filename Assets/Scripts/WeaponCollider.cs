using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider boxCollider;

    private List<GameObject> monsterList = new List<GameObject>();

    public void Init()
    {
        StopSkillHit();
    }

    public void StartSkillHit()
    {
        boxCollider.enabled = true;
    }

    public void StopSkillHit()
    {
        boxCollider.enabled = false;
        monsterList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster" && !monsterList.Contains(other.gameObject))
        {
            // �˺�����
            monsterList.Add(other.gameObject);
            // ����ô��˺��߼�

        }
    }
}
