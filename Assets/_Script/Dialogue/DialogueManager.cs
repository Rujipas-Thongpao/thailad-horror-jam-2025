using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private enum DialogueState
    {
        OUTRO = 0,
        INTRO = 1,

        EXAMINE = 3,
        TASK = 4,
        WARN = 10,
        IDLE = 15,

        NONE = 101,
    }

    [SerializeField] private DialogueConfigSO dialogueConfigSo;

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
        dialoguePanel.EventDialogueEnd += OnDialogueEnd;

        lastRandomIndex = 0;

        currentState = DialogueState.NONE;
    }

    public void Dispose()
    {
        dialoguePanel.EventDialogueEnd -= OnDialogueEnd;
        dialoguePanel.Dispose();
        dialoguePanel = null;
    }

    #region state dialogue
    public void StartIntroDialogue(int level, int abnormalIndex)
    {
        currentStage = dialogueConfigSo.stages[level];

        TryChangeState(DialogueState.INTRO);

        PlayDialogue(currentStage.Intro);
        PlayDialogue(AbnormalConfig.Infos[abnormalIndex]);
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

    public void PlayTaskAllComplete()
    {
        if (!TryChangeState(DialogueState.TASK)) return;

        PlayDialogue(currentStage.TaskAllComplete.GetRandomDialogue());
    }

    public void PlayTaskComplete()
    {
        if (!TryChangeState(DialogueState.TASK)) return;

        PlayDialogue(currentStage.TaskComplete.GetRandomDialogue());
    }

    public void PlayTaskIncomplete()
    {
        if (!TryChangeState(DialogueState.TASK)) return;

        PlayDialogue(currentStage.TaskIncomplete.GetRandomDialogue());
    }

    public void StopDialogue()
    {
        dialoguePanel.Stop();
    }

    private void OnDialogueEnd()
    {
        TryChangeState(DialogueState.NONE);
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
        if (currentState < state && state != DialogueState.NONE) return false;

        currentState = state;

        return true;
    }
    #endregion
}
