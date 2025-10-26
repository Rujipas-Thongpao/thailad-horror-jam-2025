using UnityEngine;

public class OracleTriggerArea : MonoBehaviour
{
    private PlayerManager player;

    private int incorrectCount;

    public void Init()
    {
        incorrectCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (player == null)
        {
            player = other.GetComponentInParent<PlayerManager>();
        }

        CheckHoldingObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        player.DisableSecureObject();
    }

    private void CheckHoldingObject()
    {
        if (!player.ObjectHolder.HaveObject)
        {
            DialogueManager.Instance.PlayIdleDialogue();
            return;
        }

        if (player.ObjectHolder.IsObjectAbnormal)
        {
            DialogueManager.Instance.PlayCorrectExamine();
            player.EnableSecureObject();
        }
        else
        {
            DialogueManager.Instance.PlayIncorrectExamine();
            incorrectCount++;
        }
    }
}
