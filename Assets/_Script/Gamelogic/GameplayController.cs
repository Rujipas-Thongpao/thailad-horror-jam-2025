using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private PlayerManager playerManager;

    private UIManager ui;

    private enum GameState { Menu, Gameplay, Result }
    private GameState state = GameState.Gameplay;

    private int level;

    private void Start()
    {
        ui = UIManager.Instance;
        dialogueManager.Init(ui.DialoguePanel);
        ui.ButtonPrompt.Init(playerManager);

        StartNextLevel();
    }

    private void OnDestroy()
    {
        dialogueManager.Dispose();
        ui.ButtonPrompt.Dispose();
    }

    private void StartNextLevel()
    {
        levelManager.Init(level);
        dialogueManager.StartIntroDialogue(level);
        level++;
    }
}
