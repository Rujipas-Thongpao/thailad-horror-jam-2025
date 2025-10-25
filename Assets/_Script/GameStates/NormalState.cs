using UnityEngine;

public class NormalState : GameState
{
    private PlayerManager playerManager;
    public NormalState(PlayerManager gameplayManager)
    {
        this.playerManager = gameplayManager;
    }

    public override void Enter()
    {
        playerManager.CameraDetectObject.SetEnable(true);
    }

    public override void Exit()
    {
        playerManager.CameraDetectObject.SetEnable(false);
    }

    public override void OnMouseMoved(Vector2 screenPos)
    {
    }

    public override void OnSelect(Vector2 screenPos)
    {
        var lastDetectedObject = playerManager.CameraDetectObject.GetLastDetectedObject();
        if (lastDetectedObject != null)
        {
            // pick item -> change state to holding item state.
            playerManager.ObjectHolder.RegisterObject(lastDetectedObject);
            playerManager.ChangeState(E_PlayerState.Holding);
        }
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        // No action for right click in NormalState
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        // No action for right click cancel in NormalState
    }
}
