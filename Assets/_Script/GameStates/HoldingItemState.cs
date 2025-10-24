using UnityEngine;

public class HoldingItemState : GameState
{
    private PlayerManager playerManager;

    public HoldingItemState(PlayerManager gameplayManager)
    {
        this.playerManager = gameplayManager;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void OnMouseMoved(Vector2 screenPos)
    {
    }

    public override void OnSelect(Vector2 screenPos)
    {
        // Place item on ray
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        playerManager.ChangeState(E_PlayerState.Inspecting);
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        // Optionally handle right click cancel logic here
    }
}
