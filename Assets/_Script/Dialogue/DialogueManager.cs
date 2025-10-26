using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private enum DialogueState
    {
        OUTRO = 0,
        TUTORIAL = 1,
        INTRO = 2,
        
        EXAMINE = 3,
        WARN = 10,

        IDLE = 100,
    }

    [SerializeField] private GameDialogueSO dialogueSO;

    private int lastRandomIndex;

    private UIDialoguePanel dialoguePanel;
    private DialogueState currentState;
    private StageDialogueSO currentStage;

    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init(UIDialoguePanel _dialoguePanel)
    {
        dialoguePanel = _dialoguePanel;
        dialoguePanel.Initialize();
        dialoguePanel.EventDialogueEnd += () => TryChangeState(DialogueState.IDLE);

        lastRandomIndex = 0;

        currentState = DialogueState.IDLE;
    }

    public void Dispose()
    {
        dialoguePanel.EventDialogueEnd -= () => TryChangeState(DialogueState.IDLE);
        dialoguePanel.Dispose();
        dialoguePanel = null;
    }

    #region state dialogue
    public void StartIntroDialogue(int level, int abnormalIndex)
    {
        currentStage = dialogueSO.stages[level];

        TryChangeState(DialogueState.INTRO);

        PlayDialogue(currentStage.Intro);
        PlayDialogue(AbnormalConfig.Infos[abnormalIndex]);
    }

    public void OutroDialogue()
    {
        TryChangeState(DialogueState.OUTRO);
        
        PlayDialogue(currentStage.Outro);
    }

    public void PlayCorrectExamine()
    {
        if (!TryChangeState(DialogueState.EXAMINE)) return;

        PlayDialogue(currentStage.CorrectExamine.GetRandomDialogue());
    }

    public void PlayIncorrectExamine()
    {
        if (!TryChangeState(DialogueState.EXAMINE)) return;

        PlayDialogue(currentStage.IncorrectExamine.GetRandomDialogue());
    }

    public void PlayWarnDialogue()
    {
        if (!TryChangeState(DialogueState.WARN)) return;

        PlayDialogue(currentStage.Warn.GetRandomDialogue());
    }

    public void PlayIdleDialogue()
    {
        if (!TryChangeState(DialogueState.IDLE)) return;

        PlayDialogue(currentStage.Idle.GetRandomDialogue());
    }

    public void StopDialogue()
    {
        dialoguePanel.Stop();
        TryChangeState(DialogueState.IDLE);
    }
    #endregion

    #region reuseable methods
    private void PlayDialogue(string dialogue)
    {
        List<string> dialogueList = new() { dialogue };
        PlayDialogue(dialogueList);
    }

    private void PlayDialogue(List<string> dialogueList)
    {
        dialoguePanel.Play(dialogueList);
    }

    private bool TryChangeState(DialogueState state)
    {
        if (currentState < state && state != DialogueState.IDLE) return false;

        currentState = state;

        return true;
    }
    #endregion
}
