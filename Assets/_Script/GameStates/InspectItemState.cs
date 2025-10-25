using UnityEngine;

public class InspectItemState : GameState
{
    private readonly PlayerManager playerManager;
    public InspectItemState(PlayerManager gameplayManager)
    {
        playerManager = gameplayManager;
    }

    public override void Enter()
    {
        playerManager.PlayerCam.AllowMove = false;
        playerManager.ObjectHolder.StartRotate();
    }

    public override void Exit()
    {
        playerManager.PlayerCam.AllowMove = true; 
        playerManager.ObjectHolder.StopRotate();
    }

    public override void OnMouseMoved(Vector2 screenPos)
    {
        playerManager.ObjectHolder.ApplyRotation(screenPos);
    }

    public override void OnSelect(Vector2 screenPos)
    {
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        playerManager.ObjectHolder.ApplyRotation(screenPos);
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        playerManager.ChangeState(E_PlayerState.Holding);
    }
}
