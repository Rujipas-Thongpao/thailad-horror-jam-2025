using System;
using UnityEngine;

public class OracleTriggerArea : MonoBehaviour
{
    private Action<BaseMark> eventAbnormalSecured;
    private Action eventIncorrectChecked;

    private PlayerManager player;

    public void Init(Action<BaseMark> _eventAbnormalSecured, Action _eventIncorrectChecked)
    {
        player = PlayerManager.Instance;

        eventAbnormalSecured = _eventAbnormalSecured;
        eventIncorrectChecked = _eventIncorrectChecked;
        player.ObjectHolder.EventAbnormalSecured += OnAbnormalSecured;
    }

    public void Dispose()
    {
        eventAbnormalSecured = null;
        eventIncorrectChecked = null;
        player.ObjectHolder.EventAbnormalSecured -= OnAbnormalSecured;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

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
            eventIncorrectChecked?.Invoke();
        }
    }

    private void OnAbnormalSecured(BaseMark mark)
    {
        eventAbnormalSecured?.Invoke(mark);
    }
}
