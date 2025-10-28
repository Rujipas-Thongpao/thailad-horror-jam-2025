using UnityEngine;

public class NormalState : GameState
{
    private readonly PlayerManager playerManager;
    public NormalState(PlayerManager gameplayManager)
    {
        playerManager = gameplayManager;
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
        var detectedObj = playerManager.CameraDetectObject.GetLastDetectedObject();
        var interactable = playerManager.CameraDetectObject.GetLastInteractable();

        if (detectedObj == null)
        {
        //     var furniture = playerManager.CameraDetectObject.GetLastFurnitureObject();
        //     if (furniture == null) return;
        //
        //     playerManager.ChangeState(E_PlayerState.Dragging);
            return;
        }

        // pick item -> change state to holding item state.
        detectedObj.OnPicked();
        playerManager.ObjectHolder.RegisterObject(detectedObj, interactable);
        playerManager.ChangeState(E_PlayerState.Holding);
    }

    public override void OnDeselect(Vector2 screenPos)
    {
        // No action for cancel left click in NormalState
    }

    public override void RightClickPerformed(Vector2 screenPos)
    {
        // No action for right click in NormalState
    }

    public override void RightClickCanceled(Vector2 screenPos)
    {
        // No action for right click cancel in NormalState
    }

    public override void InteractPerformed()
    {
        var interactable = playerManager.CameraDetectObject.GetLastInteractable();
        interactable?.OnInteracted();
    }
}
