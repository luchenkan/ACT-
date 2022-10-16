using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class SkillModel
{
    public Conf_SkillData skillData;
    public KeyCode keyCode;
    public float CDTime;        // 释放一次需要冷却时间
    private float currTime = 0; // 当前剩余冷却时间
    public Image CDImage;

    public bool CanRelease { get; private set; } = true;
    public void Update()
    {
        if(!CanRelease && currTime >= 0)
        {
            // 不能释放
            currTime -= Time.deltaTime;
            if(currTime < 0)
            {
                currTime = 0;
                CanRelease = true;
            }

            CDImage.fillAmount = currTime / CDTime;
        }
    }

    public void OnRelease()
    {
        CanRelease = false;
        currTime = CDTime;
    }
}
