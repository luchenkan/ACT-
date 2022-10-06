using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController controller;

    private int hp = 100;

    // 是否被打
    private bool isHurt;
    // 打击力量(描述击退程度)
    private Vector3 hurtForce;
    // 打击时间
    private float hurtTime;
    // 当前时间
    private float curHurtTime;

    private void Start()
    {
        model = transform.Find("Model").GetComponent<Monster_Model>();
        model.Init();

        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(isHurt)
        {
            curHurtTime += Time.deltaTime;
            controller.Move(hurtForce * Time.deltaTime / hurtTime);
            if(curHurtTime >= hurtTime)
            {
                isHurt = false;
            }
        }
        else
        {
            // 此处只是为了模拟重力
            controller.Move(new Vector3(0, -9, 0) * Time.deltaTime);
        }
    }

    public void Hurt(float hardTime, Transform sourceTran, Vector3 repelVelocity, float repelTransition,int damageVal)
    {
        // 仅动画
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver");
        Invoke("HurtOver", hardTime); // 延迟调用

        isHurt = true;
        hurtForce = sourceTran.TransformDirection(repelVelocity);
        hurtTime = repelTransition;
        curHurtTime = 0;

        hp -= damageVal;
    }
    
    public void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
