using System;
using UnityEngine;

public class OracleTriggerArea : MonoBehaviour
{
    public event Action<BaseMark> EventAbnormalSecured;
    public event Action EventIncorrectChecked;
    public event Action EventLeaveStage;

    [SerializeField] private StageLeaveArea stageLeaveArea;

    protected PlayerManager player;
    protected bool isActive;

    public void Init()
    {
        player = PlayerManager.Instance;

        stageLeaveArea.Init(OnLeaveStage);

        player.ObjectHolder.EventAbnormalSecured += OnAbnormalSecured;
    }

    public void Dispose()
    {
        stageLeaveArea.Dispose();

        player.ObjectHolder.EventAbnormalSecured -= OnAbnormalSecured;

        isActive = false;
    }

    public void EnableCheckArea()
    {
        isActive = true;
        Debug.Log("Oracle Trigger Area Enabled");
    }

    public void EnableLeaveArea()
    {
        stageLeaveArea.Enable();
    }

    #region trigger events
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entered Oracle Trigger Area");

        CheckHoldingObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        player.DisableSecureObject();
    }
    #endregion

    protected virtual void CheckHoldingObject()
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
            EventIncorrectChecked?.Invoke();
        }
    }

    #region subscribe events
    private void OnAbnormalSecured(BaseMark mark)
    {
        EventAbnormalSecured?.Invoke(mark);
    }

    private void OnLeaveStage()
    {
        EventLeaveStage?.Invoke();
    }
    #endregion
}
