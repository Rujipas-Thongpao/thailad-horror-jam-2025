using System.Collections.Generic;
using UnityEngine;

public enum E_PlayerState
{
    Normal,
    Holding,
    Inspecting,
    Dragging,
}

public class PlayerManager : MonoBehaviour
{
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

    [SerializeField]
    private PlayerSanityController sanityController;
    public PlayerSanityController SanityController => sanityController;

    private GameState currentState;
    private NormalState normalState;
    private HoldingItemState holdingItemState;
    private InspectItemState inspectingState;
    private DraggingFurnitureState draggingState;

    private InputHandler inputHandler;

    private Dictionary<E_PlayerState, GameState> stateDictionary = new();

    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;

        inputHandler = new InputHandler();
        inputHandler.Initialize();

        sanityController.Init(this);

        normalState = new NormalState(this);
        holdingItemState = new HoldingItemState(this);
        inspectingState = new InspectItemState(this);
        draggingState = new DraggingFurnitureState(this); 

        stateDictionary = new Dictionary<E_PlayerState, GameState>
        {
            { E_PlayerState.Normal, normalState },
            { E_PlayerState.Holding, holdingItemState },
            { E_PlayerState.Inspecting, inspectingState },
            { E_PlayerState.Dragging, draggingState }
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
        inputHandler.EventMouseMoved += OnMouseMoved;
        inputHandler.EventSelectPerformed += OnSelectPerformed;
        inputHandler.EventSelectCanceled += OnSelectCanceled;
        inputHandler.EventRightClickPerformed += OnRightClickPerformed;
        inputHandler.EventRightClickCanceled += OnRightClickCanceled;
        inputHandler.EventInteractPerformed += OnInteractPerformed;
    }

    private void RemoveListener()
    {
        inputHandler.EventMouseMoved -= OnMouseMoved;
        inputHandler.EventSelectPerformed -= OnSelectPerformed;
        inputHandler.EventSelectCanceled -= OnSelectCanceled;
        inputHandler.EventRightClickPerformed -= OnRightClickPerformed;
        inputHandler.EventRightClickCanceled -= OnRightClickCanceled;
        inputHandler.EventInteractPerformed -= OnInteractPerformed;
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

    private void OnSelectCanceled(Vector2 screenPos)
    {
        currentState.OnDeselect(screenPos);
    }

    private void OnRightClickPerformed(Vector2 screenPos)
    {
        currentState.RightClickPerformed(screenPos);
    }

    private void OnRightClickCanceled(Vector2 screenPos)
    {
        currentState.RightClickCanceled(screenPos);
    }

    private void OnInteractPerformed()
    {
        currentState.InteractPerformed();
    }

    private void OnDraggingStart(Vector2 screenPos)
    {
        
    }

    private void OnDraggingStop()
    {
        
    }
    #endregion

    public void EnableSecureObject()
    {
        ObjectHolder.EnableSecureObject();
    }

    public void DisableSecureObject()
    {
        ObjectHolder.DisableSecureObject();
    }

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
