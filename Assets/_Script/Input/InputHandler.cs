using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public event Action<Vector2> EventMouseMoved;
    public event Action<Vector2> EventSelectPerformed;
    public event Action<Vector2> EventDraggingStart;
    public event Action EventDraggingStop;

    public event Action<Vector2> EventRightClickPerformed;
    public event Action<Vector2> EventRightClickCanceled;

    private InputActions inputActions;

    public void Initialize()
    {
        //input =  new Input().Gameplay;
        inputActions = new InputActions();

        inputActions.Gameplay.Enable();
        inputActions.Gameplay.LeftClick.performed += OnLeftClickPerformed;

        inputActions.Gameplay.RightClick.performed += OnRightClickPerformed;
        inputActions.Gameplay.RightClick.canceled += OnRightClickCanceled;

        //input.FindAction("RightClick").performed += OnRightClickPerformed;
        //input.FindAction("RightClick").canceled += OnRightClickCanceled;

        //input.FindAction("Camera").performed += OnCameraPerformed;
        //input.FindAction("Camera").canceled += OnCameraCanceled;
        //input.FindAction("Move").performed += OnMouseMoved;
    }

    public void Dispose()
    {
        inputActions.Gameplay.LeftClick.performed -= OnLeftClickPerformed;

        inputActions.Gameplay.RightClick.performed -= OnRightClickPerformed;
        inputActions.Gameplay.RightClick.canceled -= OnRightClickCanceled;
        //input.FindAction("LeftClick").performed -= OnLeftClickPerformed;
        //input.FindAction("RightClick").performed -= OnRightClickPerformed;
        //input.FindAction("Camera").performed -= OnCameraPerformed;
        //input.FindAction("Camera").canceled -= OnCameraCanceled;
        //input.FindAction("Move").performed -= OnMouseMoved;
        inputActions.Disable();
        inputActions = null;
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        EventMouseMoved?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnLeftClickPerformed(InputAction.CallbackContext context)
    {
        EventSelectPerformed?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnRightClickPerformed(InputAction.CallbackContext context)
    {
        EventRightClickPerformed?.Invoke(Mouse.current.position.ReadValue());
    }

    private void OnRightClickCanceled(InputAction.CallbackContext context)
    {
        EventRightClickCanceled?.Invoke(Mouse.current.position.ReadValue());
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
