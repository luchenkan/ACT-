using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillModel
{
    public Conf_SkillData skillData;
    public KeyCode keyCode;
    public float CDTime;
    public float currTime;
}
