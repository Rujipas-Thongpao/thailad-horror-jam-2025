using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DialogueManager dialogueManager;

    private UIManager ui;

    private enum GameState { Menu, Gameplay, Result }
    private GameState state = GameState.Gameplay;

    private int level;

    private void Start()
    {
        ui = UIManager.Instance;
        dialogueManager.Init(ui.DialoguePanel);

        StartNextLevel();
    }

    private void OnDestroy()
    {
        dialogueManager.Dispose();
    }

    private void StartNextLevel()
    {
        levelManager.Init(level);
        dialogueManager.StartIntroDialogue(level);
        level++;
    }
}
