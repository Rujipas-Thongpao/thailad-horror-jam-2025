using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private enum GameState { Menu, Gameplay, Result }
    private GameState state = GameState.Gameplay;

    private int difficulty = 1;

    private void Start()
    {
        StartNextLevel();
    }

    private void StartNextLevel()
    {
        difficulty++;
        levelManager.Init(difficulty);
    }
}
