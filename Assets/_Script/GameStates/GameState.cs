using UnityEngine;

public abstract class GameState
{
    public abstract void Enter();
    public abstract void Exit();

    public abstract void OnMouseMoved(Vector2 screenPos);
    public abstract void OnSelect(Vector2 screenPos);
    public abstract void OnDeselect(Vector2 screenPos);
    public abstract void RightClickPerformed(Vector2 screenPos);
    public abstract void RightClickCanceled(Vector2 screenPos);
}
