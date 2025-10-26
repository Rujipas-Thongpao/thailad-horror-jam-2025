using UnityEngine;

[CreateAssetMenu(fileName = "DialoguePool", menuName = "SO/Dialogue/Pool")]
public class DialoguePoolSO : ScriptableObject
{
    [TextArea]
    public string[] Dialogues;

    public string GetRandomDialogue()
    {
        return Dialogues[Random.Range(0, Dialogues.Length)];
    }
}
