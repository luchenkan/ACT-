using UnityEngine;

public class Player_Hurt : Player_StateBase
{
    public override void OnEnter()
    {

    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public void SetData(Transform sourceTran, Vector3 repelVelocity, float repelTransition)
    {
        // »÷ÍËºÍ»÷·É
        player.RepelMove(sourceTran, repelVelocity, repelTransition);
    }
}
