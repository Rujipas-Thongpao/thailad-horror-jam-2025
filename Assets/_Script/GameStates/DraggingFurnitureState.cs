using UnityEngine;

public class DraggingFurnitureState : GameState
{
    private readonly PlayerManager playerManager;

    public DraggingFurnitureState(PlayerManager gameplayManager)
    {
        playerManager = gameplayManager;
    }

    public override void Enter()
    {
        Debug.Log("START DRAGGING");
    }

    public override void Exit()
    {
        Debug.Log("STOP DRAGGING");
    }

    public override void OnMouseMoved(Vector2 screenPos)
    {
    }

    public override void OnSelect(Vector2 screenPos)
    {
    }
    
    public override void OnDeselect(Vector2 screenPos)
    {
        playerManager.ChangeState(E_PlayerState.Normal);
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        // Optionally handle right click to cancel dragging.
        playerManager.ChangeState(E_PlayerState.Normal);
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
    }
}
    