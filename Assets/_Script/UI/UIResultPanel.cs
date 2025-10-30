using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIResultPanel : MonoBehaviour
{
    private Action eventCloseResult;
    private Action eventStamp;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;
    [SerializeField] private ApproveStampHandler stamp;

    [Header("Result Text")]
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private TextMeshProUGUI caseText;
    [SerializeField] private TextMeshProUGUI owner;
    [SerializeField] private TextMeshProUGUI location;
    [SerializeField] private TextMeshProUGUI mainAbnormal;
    [SerializeField] private TextMeshProUGUI subAbnormal;
    [SerializeField] private TextMeshProUGUI incorrect;
    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private TextMeshProUGUI detail;
    [SerializeField] private TextMeshProUGUI other;
    [SerializeField] private TextMeshProUGUI comment;

    [Header("PoolSO")]
    [SerializeField] private DialoguePoolSO detailSO;
    [SerializeField] private DialoguePoolSO otherSO;
    [SerializeField] private DialoguePoolSO commentSO;

    private void Start()
    {
        ToggleVisible(false);
    }

    public void Init(PerformanceStatsData stats, Action _eventNext)
    {
        eventCloseResult = _eventNext;

        closeButton.onClick.AddListener(OnStamp);

        date.text = $"10-{stats.Date}-2025";
        caseText.text = $"#{Random.Range(100000, 999999)}";
        owner.text = new string('■', Random.Range(6, 9)) + ('■', Random.Range(8, 12));
        location.text = new string('■', Random.Range(12, 15));
        mainAbnormal.text = stats.MainAbnormal.ToString();
        subAbnormal.text = stats.SubAbnormal.ToString();
        incorrect.text = stats.Incorrect.ToString();
        status.text = "Secured";
        int detailRand = Random.Range(0, detailSO.Dialogues.Length - 1);
        detail.text = detailSO.Dialogues[detailRand];
        int otherRand = Random.Range(0, otherSO.Dialogues.Length - 1);
        other.text = otherSO.Dialogues[otherRand];
        int commentRand = Random.Range(0, commentSO.Dialogues.Length - 1);
        comment.text = commentSO.Dialogues[commentRand];

        ToggleVisible(true);
    }

    public void Dispose()
    {
        eventCloseResult = null;

        ToggleVisible(false);
    }

    private void ToggleVisible(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.blocksRaycasts = visible;
    }

    #region event listeners

    private void OnStamp()
    {
        stamp.StampSecure();
        //Stamp sound
        closeButton.gameObject.SetActive(false);

        Invoke("invokeEndLevel", 1f);
    }

    private void invokeEndLevel()
    {
        eventCloseResult?.Invoke();
    }

    #endregion
}