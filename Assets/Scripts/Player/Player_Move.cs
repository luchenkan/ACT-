using UnityEngine;

public class Player_Move : StateBase<PlayerState>
{
    public Player_Controller player;
    public float moveSpeed = 10f;
    public float rotateSpeed = 30f;
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

        Move(h, v + runTran);
    }

    private bool IsRun()
    {
        var temp = player.input.GetRunKey() && player.input.Vertical > 0;
        moveSpeed = temp ? 20f : 10f;
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
        player.transform.Rotate(rot * rotateSpeed * Time.deltaTime);

        // 模型的动画同步
        player.model.UpdateMove(h, v);

        // 检测攻击事件
        if(player.CheckAttack())
        {
            player.UpdateState<Player_Attack>(PlayerState.Player_Attack);
        }
    }

    public override void Init(PlayerState state, FSMController<PlayerState> controller)
    {
        base.Init(state, controller);

        player = controller as Player_Controller;
    }
}
