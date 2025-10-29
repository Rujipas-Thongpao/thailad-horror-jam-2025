using System;
using UnityEngine;

public class OracleTriggerAreaTutorial : OracleTriggerArea
{
    public event Action EventPlayerSecuredTutorialObject;

    protected override void CheckHoldingObject()
    {
        if (!player.ObjectHolder.HaveObject)
        {
            DialogueManager.Instance.PlayIdleDialogue();
            return;
        }
        if(player.ObjectHolder.IsObjectTutorial)
        {
            EnableLeaveArea();
            Debug.Log("Tutorial Object Secured");
            EventPlayerSecuredTutorialObject?.Invoke();
            return;
        }
    }
}
