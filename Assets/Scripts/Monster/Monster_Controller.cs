using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController controller;

    private int hp = 100;

    // �Ƿ񱻴�
    private bool isHurt;
    // �������(�������˳̶�)
    private Vector3 hurtForce;
    // ���ʱ��
    private float hurtTime;
    // ��ǰʱ��
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
            // �˴�ֻ��Ϊ��ģ������
            controller.Move(new Vector3(0, -9, 0) * Time.deltaTime);
        }
    }

    public void Hurt(float hardTime, Transform sourceTran, Vector3 repelVelocity, float repelTransition,int damageVal)
    {
        // ������
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver");
        Invoke("HurtOver", hardTime); // �ӳٵ���

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
