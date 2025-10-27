using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : MonoBehaviour
{
    private Action eventCloseResult;

    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI resultText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Init(PerformanceStatsData stats, Action _eventNext)
    {
        eventCloseResult = _eventNext;

        gameObject.SetActive(true);
        closeButton.onClick.AddListener(OnNext);

        resultText.text = $"Main Abnormal: {stats.MainAbnormal}\n" +
                          $"Sub Abnormal: {stats.SubAbnormal}\n" +
                          $"Incorrect: {stats.Incorrect}";
    }

    public void Dispose()
    {
        eventCloseResult = null;

        gameObject.SetActive(false);
    }

    #region event listeners

    private void OnNext()
    {
        eventCloseResult?.Invoke();
    }

    #endregion
}