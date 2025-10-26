using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDialogue", menuName = "SO/Dialogue/Stage")]
public class StageDialogueSO : ScriptableObject
{
    [TextArea]
    public List<string> Intro;

    public DialoguePoolSO CorrectExamine;
    public DialoguePoolSO IncorrectExamine;
    public DialoguePoolSO TaskComplete;
    public DialoguePoolSO Warn;
    public DialoguePoolSO Idle;

    [TextArea]
    public List<string> Outro;
}
