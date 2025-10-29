using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action EventIntroEnd;

    [SerializeField] private DialogueConfigSO dialogueConfigSo;

    private UIDialoguePanel dialoguePanel;
    private StageDialogueSO currentStage;

    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    private bool IsIntro;

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
    }

    public void Dispose()
    {
        dialoguePanel.Dispose();
        dialoguePanel = null;
    }

    #region state dialogue
    public void StartIntroDialogue(int level, int abnormalIndex)
    {
        currentStage = dialogueConfigSo.stages[level];

        PlayDialogue(currentStage.Intro);
        PlayDialogue(AbnormalConfig.Infos[abnormalIndex]);

        IsIntro = true;
        dialoguePanel.EventDialogueEnd += OnDialogueEnd;
    }

    public void PlayCorrectExamine()
    {
        PlayDialogue(currentStage.CorrectExamine.GetRandomDialogue());
    }

    public void PlayIncorrectExamine()
    {
        PlayDialogue(currentStage.IncorrectExamine.GetRandomDialogue());
    }

    public void PlayWarnDialogue()
    {
        PlayDialogue(currentStage.Warn.GetRandomDialogue());
    }

    public void PlayIdleDialogue()
    {
        PlayDialogue(currentStage.Idle.GetRandomDialogue());
    }

    public void PlayTaskAllComplete()
    {
        PlayDialogue(currentStage.TaskAllComplete.GetRandomDialogue());
    }

    public void PlayTaskComplete()
    {
        PlayDialogue(currentStage.TaskComplete.GetRandomDialogue());
    }

    public void PlayTaskIncomplete()
    {
        PlayDialogue(currentStage.TaskIncomplete.GetRandomDialogue());
    }

    public void PlaySecureSubDialogue()
    {
        PlayDialogue(currentStage.SecureSub.GetRandomDialogue());
    }

    public void StopDialogue()
    {
        dialoguePanel.Stop();
    }

    private void OnDialogueEnd()
    {
        if (!IsIntro) return;

        IsIntro = false;
        EventIntroEnd?.Invoke();
        dialoguePanel.EventDialogueEnd -= OnDialogueEnd;
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
    #endregion
}
