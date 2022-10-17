using UnityEngine;

public class Player_Attack : Player_StateBase
{
    public override void OnEnter()
    {
        // ����
        player.Attack();
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        // �����ι�
        if(player.CheckAttack())
        {
            player.Attack();
        }

        // ת��
        if(player.CurrSkillData.releaseModel.canRotate)
        {
            // ��ת
            var rot = new Vector3(0, player.input.Horizeontal, 0);
            player.transform.Rotate(rot * 60 * Time.deltaTime);
        }
    }
}
