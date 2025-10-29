using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : MonoBehaviour
{
    private Action eventCloseResult;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;

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

    private void Start()
    {
        ToggleVisible(false);
    }

    public void Init(PerformanceStatsData stats, Action _eventNext)
    {
        eventCloseResult = _eventNext;

        closeButton.onClick.AddListener(OnNext);

        //change text

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

    private void OnNext()
    {
        eventCloseResult?.Invoke();
    }

    #endregion
}