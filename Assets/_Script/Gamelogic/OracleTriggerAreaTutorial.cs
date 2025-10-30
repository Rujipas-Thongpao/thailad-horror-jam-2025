using System;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

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
            EventPlayerSecuredTutorialObject?.Invoke();
            return;
        }
    }
}
