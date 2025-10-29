using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TutoriaManager tutoriaManager;

    private UIManager ui;

    private enum GameState { Menu, Gameplay, Result }
    private GameState state = GameState.Gameplay;

    private int level;

    private void Start()
    {
        ui = UIManager.Instance;
        dialogueManager.Init(ui.DialoguePanel);
        ui.ButtonPrompt.Init(playerManager);

        levelManager.EventStageEnd += OnStageEnd;

        //StartNextLevel();
        StartTutorial();
    }

    private void OnDestroy()
    {
        dialogueManager.Dispose();
        ui.ButtonPrompt.Dispose();

        levelManager.EventStageEnd -= OnStageEnd;
    }

    private void StartNextLevel()
    {
        var abnormalIndex = levelManager.Init(level);
        dialogueManager.StartIntroDialogue(level, abnormalIndex);
        level++;
    }

    #region game flow

    private void OnStageEnd(PerformanceStatsData stats)
    {
        levelManager.Dispose();
        ui.ResultPanel.Init(stats, OnCloseResult);
        ui.ButtonPrompt.Hide();
        ToggleCursor(true);
    }

    private void OnCloseResult()
    {
        ui.ResultPanel.Dispose();
        StartNextLevel();
        ToggleCursor(false);
    }

    #endregion

    #region

    private void StartTutorial()
    {
        tutoriaManager.Init(this);
    }

    #endregion

    private static void ToggleCursor(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }
}
