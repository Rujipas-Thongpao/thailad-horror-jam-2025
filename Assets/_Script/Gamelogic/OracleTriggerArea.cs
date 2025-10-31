using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OracleTriggerArea : MonoBehaviour
{
    public event Action<BaseMark> EventAbnormalSecured;
    public event Action EventIncorrectChecked;
    public event Action EventLeaveStage;

    [SerializeField] private StageLeaveArea stageLeaveArea;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] oracleClips;
    [SerializeField] private AudioClip secureClip;

    protected PlayerManager player;
    private bool isActive;
    private int lastClipIndex;

    public void Init()
    {
        player = PlayerManager.Instance;

        stageLeaveArea.Init(OnLeaveStage);

        player.ObjectHolder.EventAbnormalSecured += OnAbnormalSecured;
        DialogueManager.Instance.EventOracleSpeak += OnOracleSpeak;
    }

    public void Dispose()
    {
        stageLeaveArea.Dispose();

        player.ObjectHolder.EventAbnormalSecured -= OnAbnormalSecured;
        DialogueManager.Instance.EventOracleSpeak -= OnOracleSpeak;

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
        audioSource.PlayOneShot(secureClip);
    }

    private void OnLeaveStage()
    {
        EventLeaveStage?.Invoke();
    }

    private void OnOracleSpeak()
    {
        if(audioSource != null)
        {
            var rand = Random.Range(0, oracleClips.Length);
            if (rand == lastClipIndex) rand = (rand + 1) % oracleClips.Length;
            audioSource.PlayOneShot(oracleClips[rand]);
            lastClipIndex = rand;
        }
    }
    #endregion
}
