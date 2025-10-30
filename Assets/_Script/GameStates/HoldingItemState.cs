using UnityEngine;

public class HoldingItemState : GameState
{
    private readonly PlayerManager playerManager;

    public HoldingItemState(PlayerManager gameplayManager)
    {
        playerManager = gameplayManager;
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
        if (playerManager.ObjectHolder.CanSecure)
        {
            playerManager.ObjectHolder.SecureObject();
            playerManager.ChangeState(E_PlayerState.Normal);
        }
        else
        {
            var isplaceSuccess = playerManager.ObjectHolder.PlaceItem();
            if(isplaceSuccess)
            {
                playerManager.ChangeState(E_PlayerState.Normal);
            }
        }

    }

    public override void OnDeselect(Vector2 screenPos)
    {
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        playerManager.ChangeState(E_PlayerState.Inspecting);
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        // Optionally handle right click cancel logic here
    }

    public override void InteractPerformed()
    {
        if (playerManager.ObjectHolder.CanSecure)
        {
            playerManager.ObjectHolder.SecureObject();
            playerManager.ChangeState(E_PlayerState.Normal);
        }
        else
        {
            playerManager.ObjectHolder.TryInteract();
        }
    }
}
