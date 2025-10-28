using UnityEngine;

[CreateAssetMenu(fileName = "DialogueConfig", menuName = "SO/Dialogue/DialogueConfig")]
public class DialogueConfigSO : ScriptableObject
{
    public StageDialogueSO[] stages;
}
