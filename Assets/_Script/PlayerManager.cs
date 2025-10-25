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

    //[SerializeField] private CameraController cameraController;
    //[SerializeField] private UIManager uiManager;
    //[SerializeField] private MapManager mapManager;
    //[SerializeField] private VisualManager visualManager;

    private GameState currentState;
    private NormalState normalState;
    private HoldingItemState holdingItemState;
    private InspectItemState inspectingState;


    private InputHandler inputHandler;

    private Dictionary<E_PlayerState, GameState> stateDictionary = new();

    private void Awake()
    {
        //cameraController.Initialize();
        //uiManager.Initialize();

        inputHandler = new InputHandler();
        inputHandler.Initialize();

        //mapManager.Initialize(visualManager.Tilemap);
        //visualManager.Initialize(mapManager.MapSize);

        normalState = new NormalState(this);
        holdingItemState = new HoldingItemState(this);
        inspectingState = new InspectItemState(this);

        stateDictionary = new()
        {
            {E_PlayerState.Normal , normalState },
            {E_PlayerState.Holding , holdingItemState   },
            {E_PlayerState.Inspecting , inspectingState }
        };

        //var gatherObjects = mapManager.SpawnStartingGatherObjects();
        //visualManager.DisplayGatherObjects(gatherObjects);

        ChangeState(E_PlayerState.Normal);

        AddListener();
    }

    private void OnDestroy()
    {
        //uiManager.Dispose();
        inputHandler.Dispose();
        //mapManager.Dispose();
        //visualManager.Dispose();
        
        normalState = null;
        holdingItemState = null;

        RemoveListener();
    }

    private void AddListener()
    {
        //inputHandler.EventMouseMoved += OnMouseMoved;
        inputHandler.EventSelectPerformed += OnSelectPerformed;

        inputHandler.EventRightClickPerformed += OnRightClickPerformed;
        inputHandler.EventRightClickCanceled += OnRightClickCanceled;

        //inputHandler.EventDraggingStart += OnDraggingStart;
        //inputHandler.EventDraggingStop += OnDraggingStop;

        //uiManager.EventBuildIconClicked += OnBuildIconClicked;
        //uiManager.EventBuildPrevButtonClicked += OnBuildPrevButtonClicked;
        //uiManager.EventBuildNextButtonClicked += OnBuildNextButtonClicked;
        //uiManager.EventNavigatePerformed += OnNavigatePerformed;
        //uiManager.EventLensModeSelected += OnLensModeSelected;

        //mapManager.EventGatherObjectDepleted += OnGatherObjectDepleted;
    }

    private void RemoveListener()
    {
        //inputHandler.EventMouseMoved -= OnMouseMoved;
        inputHandler.EventSelectPerformed -= OnSelectPerformed;

        inputHandler.EventRightClickPerformed -= OnRightClickPerformed;
        inputHandler.EventRightClickCanceled -= OnRightClickCanceled;

        //inputHandler.EventDraggingStart -= OnDraggingStart;
        //inputHandler.EventDraggingStop -= OnDraggingStop;

        //uiManager.EventBuildIconClicked -= OnBuildIconClicked;
        //uiManager.EventBuildPrevButtonClicked -= OnBuildPrevButtonClicked;
        //uiManager.EventBuildNextButtonClicked -= OnBuildNextButtonClicked;
        //uiManager.EventNavigatePerformed -= OnNavigatePerformed;
        //uiManager.EventLensModeSelected -= OnLensModeSelected;

        //mapManager.EventGatherObjectDepleted -= OnGatherObjectDepleted;
    }


    #region player input
    private void OnMouseMoved(Vector2 screenPos)
    {
        //cameraController.UpdateMousePosition(screenPos);
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
        //cameraController.StartDragging(screenPos);
    }

    private void OnDraggingStop()
    {
        //cameraController.StopDragging();
    }
    #endregion

    //#region UI
    //private void OnBuildIconClicked(BuildingSO buildingSO)
    //{
    //    buildState.SelectBuilding(buildingSO);
    //    ChangeState(buildState);
    //}

    //private void OnBuildPrevButtonClicked()
    //{
    //    var buildingData = buildState.SelectPreviousBuildingPage();
    //    uiManager.SelectBuildingPanel(buildingData);
    //}

    //private void OnBuildNextButtonClicked()
    //{
    //    var buildingData = buildState.SelectNextBuildingPage();
    //    uiManager.SelectBuildingPanel(buildingData);
    //}

    //private void OnNavigatePerformed(Transform target)
    //{
    //    cameraController.SetTargetPosition(target.position);
    //}

    //private void OnLensModeSelected(int mode)
    //{
    //    visualManager.DisplayLensMode(mode);

    //    if (mode == 1)
    //    {
    //        ChangeState(lensState);
    //    }
    //    else
    //    {
    //        ChangeState(idleState);
    //    }
    //}
    //#endregion

    //#region map
    //private void OnGatherObjectDepleted(GatherObject gatherObject)
    //{
    //    visualManager.RemoveGatherObject(gatherObject);
    //}
    //#endregion

    //#endregion

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
