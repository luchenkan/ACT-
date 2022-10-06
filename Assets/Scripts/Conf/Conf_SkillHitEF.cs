using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="命中效果配置",menuName ="配置/技能配置/命中效果")]
public class Conf_SkillHitEF : ScriptableObject
{
    public Skill_SpawnObj spawn;

    public AudioClip audioClip;
}
