using UnityEngine;

public class Player_Attack : Player_StateBase
{
    public override void OnEnter()
    {
        // ¹¥»÷
        player.Attack();
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        // ¼ì²â¶þ¶Î¹¥
        if(player.CheckAttack())
        {
            player.Attack();
        }

        // ×ªÏò
        if(player.CurrSkillData.releaseModel.canRotate)
        {
            // Ðý×ª
            var rot = new Vector3(0, player.input.Horizeontal, 0);
            player.transform.Rotate(rot * 60 * Time.deltaTime);
        }
    }
}
