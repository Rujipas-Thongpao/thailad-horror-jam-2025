using System.Collections.Generic;
using UnityEngine;


public enum E_PlayerState
{
    Normal,
    Holding,
    Inspecting
}

public class PlayerManager : MonoBehaviour
{
    //[SerializeField]
    //private FirstPersonController movementController;
    //public FirstPersonController MovementController => movementController;

    [SerializeField]
    private PlayerMovementAdvanced movementController;
    public PlayerMovementAdvanced MovementController => movementController;

    [SerializeField]
    private PlayerCam playerCam;
    public PlayerCam PlayerCam => playerCam;

    [SerializeField]
    private CameraDetectObject cameraDetectObject;
    public CameraDetectObject CameraDetectObject => cameraDetectObject;

    [SerializeField]
    private ObjectHolder objectHolder;
    public ObjectHolder ObjectHolder => objectHolder;

    private GameState currentState;
    private NormalState normalState;
    private HoldingItemState holdingItemState;
    private InspectItemState inspectingState;

    private InputHandler inputHandler;

    private Dictionary<E_PlayerState, GameState> stateDictionary = new();

    private void Awake()
    {
        inputHandler = new InputHandler();
        inputHandler.Initialize();

        normalState = new NormalState(this);
        holdingItemState = new HoldingItemState(this);
        inspectingState = new InspectItemState(this);

        stateDictionary = new()
        {
            {E_PlayerState.Normal , normalState },
            {E_PlayerState.Holding , holdingItemState   },
            {E_PlayerState.Inspecting , inspectingState }
        };

        ChangeState(E_PlayerState.Normal);

        AddListener();
    }

    private void OnDestroy()
    {
        inputHandler.Dispose();
        
        normalState = null;
        holdingItemState = null;

        RemoveListener();
    }

    private void AddListener()
    {
        inputHandler.EventSelectPerformed += OnSelectPerformed;

        inputHandler.EventRightClickPerformed += OnRightClickPerformed;
        inputHandler.EventRightClickCanceled += OnRightClickCanceled;
    }

    private void RemoveListener()
    {
        inputHandler.EventSelectPerformed -= OnSelectPerformed;

        inputHandler.EventRightClickPerformed -= OnRightClickPerformed;
        inputHandler.EventRightClickCanceled -= OnRightClickCanceled;
    }

    #region player input
    private void OnMouseMoved(Vector2 screenPos)
    {
        currentState.OnMouseMoved(screenPos);
    }

    private void OnSelectPerformed(Vector2 screenPos)
    {
        currentState.OnSelect(screenPos);
    }

    private void OnRightClickPerformed(Vector2 screenPos)
    {
        currentState.RightClickPerformed(screenPos);
    }

    private void OnRightClickCanceled(Vector2 screenPos)
    {
        currentState.RightClickCanceled(screenPos);
    }

    private void OnDraggingStart(Vector2 screenPos)
    {
        
    }

    private void OnDraggingStop()
    {
        
    }
    #endregion

    #region state
    public void ChangeState(E_PlayerState nextStateEnum)
    {
        var nextState = stateDictionary[nextStateEnum];

        if (currentState == nextState) return;

        currentState?.Exit();
        nextState?.Enter();

        currentState = nextState;
    }
    #endregion
}
