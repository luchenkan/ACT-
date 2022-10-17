using UnityEngine;

public class Player_Move : Player_StateBase
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 40f;
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
                runTran += Time.deltaTime;
            }
            else if(!IsRun() && runTran > 0)
            {
                runTran -= Time.deltaTime;
            }
        }
        else if(runTran > 0)
        {
            runTran -= Time.deltaTime;
        }

        Move(h, v + runTran);

        // ��⹥�� �����Ұ����������л�������״̬
        // ����Ҫ����CD������
        if (player.CheckAttack())
        {
            player.UpdateState<Player_Attack>(PlayerState.Player_Attack);
        }
    }

    private bool IsRun()
    {
        var temp = player.input.GetRunKey() && player.input.Vertical > 0;
        moveSpeed = temp ? 6 : 3f;
        return temp;
    }

    private void Move(float h,float v)
    {
        // �ƶ�
        var dir = new Vector3(0, 0, v);
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * moveSpeed);

        // ��ת
        var rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot * rotateSpeed * Time.deltaTime);

        // ģ�͵Ķ���ͬ��
        model.UpdateMove(h, v);
    }
}
