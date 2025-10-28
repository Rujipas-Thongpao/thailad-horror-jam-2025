using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public event Action<PerformanceStatsData> EventStageEnd;

    [SerializeField] private RoomController roomController;
    [SerializeField] private OracleTriggerArea oracle;
    [SerializeField] private LevelConfigSO config;

    private readonly List<BaseMark> marks = new();
    private readonly List<BaseMark> mainAbnormal = new();

    private GameObject hallway;
    private int mainAbnormalType;
    private PerformanceStatsData stats;

    public int Init(int level)
    {
        var main = config.Levels[level].MainIntensity;
        var sub = config.Levels[level].SubIntensity;

        mainAbnormalType = Random.Range(0, AbnormalConfig.Count);

        foreach (var intensity in main)
        {
            var mark = AbnormalConfig.Create(mainAbnormalType, intensity);
            marks.Add(mark);
            mainAbnormal.Add(mark);
        }

        foreach (var intensity in sub)
        {
            var rand = Random.Range(0, AbnormalConfig.Count);

            if (rand == mainAbnormalType) rand = (rand + 1) % AbnormalConfig.Count;
            marks.Add(AbnormalConfig.Create(rand, intensity));
        }

        roomController.Init(marks);
        oracle.Init(OnAbnormalSecured, OnIncorrectChecked);
        hallway = Instantiate(config.Hallways[level]);

        stats = new PerformanceStatsData();

        return mainAbnormalType;
    }

    public void Dispose()
    {
        roomController.Dispose();
        oracle.Dispose();

        Destroy(hallway);
    }

    #region event listener

    private void OnAbnormalSecured(BaseMark mark)
    {
        stats.AbnormalSecured(mainAbnormal.Contains(mark));
        Debug.Log(stats.MainAbnormal);

        if (stats.MainAbnormal < mainAbnormal.Count) return;

        EventStageEnd?.Invoke(stats);
    }

    private void OnIncorrectChecked()
    {
        stats.IncorrectChecked();
    }
    #endregion
}

public class PerformanceStatsData
{
    public int MainAbnormal { get; private set;}
    public int SubAbnormal { get ; private set; }
    public int Incorrect { get ; private set; }

    public void AbnormalSecured(bool isMain)
    {
        if (isMain) MainAbnormal++;
        else SubAbnormal++;
    }

    public void IncorrectChecked()
    {
        Incorrect++;
    }
}