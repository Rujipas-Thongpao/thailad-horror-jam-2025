using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : MonoBehaviour
{
    private Action eventCloseResult;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI resultText;

    private void Start()
    {
        ToggleVisible(false);
    }

    public void Init(PerformanceStatsData stats, Action _eventNext)
    {
        eventCloseResult = _eventNext;

        closeButton.onClick.AddListener(OnNext);

        resultText.text = $"Main Abnormal: {stats.MainAbnormal}\n" +
                          $"Sub Abnormal: {stats.SubAbnormal}\n" +
                          $"Incorrect: {stats.Incorrect}";

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