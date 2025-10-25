using System.Collections;
using UnityEngine;

public class InspectItemState : GameState
{
    private PlayerManager playerManager;
    public InspectItemState(PlayerManager gameplayManager)
    {
        this.playerManager = gameplayManager;
    }

    public override void Enter()
    {
        playerManager.MovementController.cameraCanMove = false;
        playerManager.ObjectHolder.StartRotate();
    }

    public override void Exit()
    {
        playerManager.MovementController.cameraCanMove = true;
        playerManager.ObjectHolder.StopRotate();
    }

    public override void OnMouseMoved(Vector2 screenPos)
    {
    }

    public override void OnSelect(Vector2 screenPos)
    {
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        playerManager.ObjectHolder.SetRotate(screenPos);
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        playerManager.ChangeState(E_PlayerState.Holding);
    }
}
