using TMPro;
using UnityEngine;

public class UIWeeklyNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI mainAbnormalText;
    [SerializeField] private TextMeshProUGUI subAbnormalText;
    [SerializeField] private TextMeshProUGUI performanceText;

    public void Init(PerformanceStatsData perf)
    {
        dateText.SetText(perf.Date.ToString());
        mainAbnormalText.SetText(perf.MainAbnormalName.ToString());
        subAbnormalText.SetText(perf.SubAbnormalNames != null && perf.SubAbnormalNames.Count > 0
            ? string.Join(", ", perf.SubAbnormalNames)
            : "-");

        performanceText.text = perf.Incorrect switch
        {
            0 => "Works perfectly",
            3 => "Performing below standard",
            2 => "Work according to standards",
            _ => "■■■■■■■■"
        };
    }


    public void Dispose()
    {
        if (dateText != null) dateText.text = string.Empty;
        if (mainAbnormalText != null) mainAbnormalText.text = string.Empty;
        if (subAbnormalText != null) subAbnormalText.text = string.Empty;
        if (performanceText != null) performanceText.text = string.Empty;
    }
}
