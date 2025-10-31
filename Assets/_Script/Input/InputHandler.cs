using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public event Action<Vector2> EventMouseMoved;
    public event Action<Vector2> EventSelectPerformed;
    public event Action<Vector2> EventSelectCanceled;
    public event Action<Vector2> EventDraggingStart;
    public event Action EventDraggingStop;

    public event Action<Vector2> EventRightClickPerformed;
    public event Action<Vector2> EventRightClickCanceled;
    public event Action EventInteractPerformed;

    private InputActions inputActions;

    public bool Interactable { get; set; } = true;

    public void Initialize()
    {
        inputActions = new InputActions();

        inputActions.Gameplay.Axis.performed += OnMouseMoved;
        inputActions.Gameplay.LeftClick.performed += OnLeftClickPerformed;
        inputActions.Gameplay.LeftClick.canceled += OnLeftClickCanceled;
        inputActions.Gameplay.RightClick.performed += OnRightClickPerformed;
        inputActions.Gameplay.RightClick.canceled += OnRightClickCanceled;
        inputActions.Gameplay.Interact.performed += OnInteractPerformed;

        inputActions.Gameplay.Enable();
    }

    public void Dispose()
    {
        inputActions.Gameplay.Axis.performed -= OnMouseMoved;
        inputActions.Gameplay.LeftClick.performed -= OnLeftClickPerformed;
        inputActions.Gameplay.LeftClick.canceled -= OnLeftClickCanceled;
        inputActions.Gameplay.RightClick.performed -= OnRightClickPerformed;
        inputActions.Gameplay.RightClick.canceled -= OnRightClickCanceled;
        inputActions.Gameplay.Interact.performed -= OnInteractPerformed;

        inputActions.Disable();
        inputActions = null;
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        EventMouseMoved?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnLeftClickPerformed(InputAction.CallbackContext context)
    {
        if(!Interactable) return;
        EventSelectPerformed?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnLeftClickCanceled(InputAction.CallbackContext context)
    {
        if(!Interactable) return;
        EventSelectCanceled?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnRightClickPerformed(InputAction.CallbackContext context)
    {
        EventRightClickPerformed?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnRightClickCanceled(InputAction.CallbackContext context)
    {
        EventRightClickCanceled?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!Interactable) return;
        PerformInteract();
    }

    // New perform function
    public void PerformInteract()
    {
        EventInteractPerformed?.Invoke();
    }

    private void OnCameraPerformed(InputAction.CallbackContext context)
    {
        EventDraggingStart?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnCameraCanceled(InputAction.CallbackContext context)
    {
        EventDraggingStop?.Invoke();
    }
}
