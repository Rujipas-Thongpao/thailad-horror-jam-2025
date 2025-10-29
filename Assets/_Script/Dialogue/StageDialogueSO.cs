using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDialogue", menuName = "SO/Dialogue/Stage")]
public class StageDialogueSO : ScriptableObject
{
    [TextArea]
    public List<string> Intro;

    [Header ("Examine")]
    public DialoguePoolSO CorrectExamine;
    public DialoguePoolSO IncorrectExamine;
    public DialoguePoolSO Warn;
    public DialoguePoolSO Idle;

    [Header ("Tasks Status")]
    public DialoguePoolSO TaskAllComplete;
    public DialoguePoolSO TaskComplete;
    public DialoguePoolSO TaskIncomplete;
    public DialoguePoolSO SecureSub;
}
