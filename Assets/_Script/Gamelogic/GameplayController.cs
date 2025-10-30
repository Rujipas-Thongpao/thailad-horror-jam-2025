using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private GameObject tutoriaManagerPrefab;
    private TutorialManager tutorialManager;

    [SerializeField] private bool StartWithTutorial = false;

    private UIManager ui;
    public UIManager UI => ui;

    private enum GameState { Menu, Gameplay, Result }
    private GameState state = GameState.Gameplay;

    private int level;

    private void Start()
    {
        ui = UIManager.Instance;
        dialogueManager.Init(ui.DialoguePanel);
        ui.ButtonPrompt.Init(playerManager);

        levelManager.EventStageEnd += OnStageEnd;


        if (StartWithTutorial)
        {
            StartTutorial();
        }
        else
        {
            StartNextLevel();
        }
    }

    private void OnDestroy()
    {
        dialogueManager.Dispose();
        ui.ButtonPrompt.Dispose();

        levelManager.EventStageEnd -= OnStageEnd;
    }

    private void StartNextLevel()
    {
        ui.BlinkEyeController.ToOpenEye(3);
        var abnormalIndex = levelManager.Init(level);
        dialogueManager.StartIntroStageDialogue(level, abnormalIndex);
        level++;
    }

    private void RestartLevel()
    {
        level = 0;
        StartNextLevel();
    }

    #region game flow

    private void OnStageEnd(PerformanceStatsData stats)
    {
        levelManager.Dispose();
        ui.BlinkEyeController.ToCloseEye(2);
        ui.ResultPanel.Init(stats, OnCloseResult);
        ui.ButtonPrompt.Hide();
        AudioPoolManager.instance.SetAmbientVolume(0f);
        ToggleCursor(true);
    }

    private void OnTutorialEnd()
    {
        ui.BlinkEyeController.ToCloseEye(4f, () =>
        {
            levelManager.DisposeForTutorial();
            ui.ButtonPrompt.Hide();

            tutorialManager.Dispose();
            tutorialManager.EventTutorialEnd -= OnTutorialEnd;

            Destroy(tutorialManager.gameObject);

            StartNextLevel();
        });
    }

    private void OnCloseResult()
    {
        ui.ResultPanel.Dispose();
        StartNextLevel();
        AudioPoolManager.instance.SetAmbientVolume(1f);
        ToggleCursor(false);
    }

    #endregion

    #region
    private void StartTutorial()
    {
        tutorialManager = Instantiate(tutoriaManagerPrefab).GetComponent<TutorialManager>();
        tutorialManager.Init(this);
        tutorialManager.EventTutorialEnd += OnTutorialEnd;
    }

    #endregion

    private static void ToggleCursor(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }
}
