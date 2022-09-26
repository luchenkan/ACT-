using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : StateBase
{
    public Player_Controller player;
    public float moveSpeed = 1f;
    public float rotateSpeed = 0.2f;
    private float runTran = 0;

    public override void OnEnter()
    {

    }

    public override void OnLeave()
    {

    }

    public override void OnUpdate()
    {
        var h = player.input.Horizeontal;
        var v = player.input.Vertical;

        if (v >= 0)
        {
            if(IsRun() && runTran < 1)
            {
                runTran += Time.deltaTime / 2;
            }
            else if(!IsRun() && runTran > 0)
            {
                runTran -= Time.deltaTime / 2;
            }
        }
        else if(runTran > 0)
        {
            runTran -= Time.deltaTime / 2;
        }

        if(IsRun())
        {
            
        }
        Move(h, v + runTran);
    }

    private bool IsRun()
    {
        var temp = player.input.GetRunKey() && player.input.Vertical > 0;
        moveSpeed = temp ? 2f : 1f;
        return temp;
    }

    private void Move(float h,float v)
    {
        // 移动
        var dir = new Vector3(0, 0, v);
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * moveSpeed);

        // 旋转
        var rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot);

        // 模型的动画同步
        player.model.UpdateMove(h, v);
    }

    public override void Init(Enum state, FSMController controller)
    {
        base.Init(state, controller);

        player = controller as Player_Controller;
    }
}
