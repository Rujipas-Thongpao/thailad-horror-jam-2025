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

        var furnitureSetup = config.Levels[level].furniture;

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

        roomController.Init(marks, furnitureSetup);
        oracle.Init();
        oracle.EventAbnormalSecured += OnAbnormalSecured;
        oracle.EventIncorrectChecked += OnIncorrectChecked;
        oracle.EventLeaveStage += OnPlayerLeaveStage;

        hallway = Instantiate(config.Levels[level].Hallway);

        stats = new PerformanceStatsData(level);

        DialogueManager.Instance.EventIntroEnd += OnDialogueIntroEnd;

        return mainAbnormalType;
    }

    public void Dispose()
    {
        marks.Clear();
        mainAbnormal.Clear();

        roomController.Dispose();
        oracle.Dispose();
        oracle.EventAbnormalSecured -= OnAbnormalSecured;
        oracle.EventIncorrectChecked -= OnIncorrectChecked;
        oracle.EventLeaveStage -= OnPlayerLeaveStage;

        DialogueManager.Instance.EventIntroEnd -= OnDialogueIntroEnd;

        Destroy(hallway);
    }

    #region event listener

    private void OnAbnormalSecured(BaseMark mark)
    {
        var isMain = mainAbnormal.Contains(mark);
        stats.AbnormalSecured(isMain);

        if (stats.MainAbnormal + stats.SubAbnormal == marks.Count)
        {
            DialogueManager.Instance.PlayTaskAllComplete();
        }
        else if (isMain)
        {
            if (stats.MainAbnormal == mainAbnormal.Count)
            {
                DialogueManager.Instance.PlayTaskComplete();
            }
            else
            {
                DialogueManager.Instance.PlayTaskIncomplete();
            }
        }

        if (stats.MainAbnormal < mainAbnormal.Count) return;

        oracle.EnableLeaveArea();
    }

    private void OnIncorrectChecked()
    {
        stats.IncorrectChecked();
    }

    private void OnPlayerLeaveStage()
    {
        DialogueManager.Instance.StopDialogue();
        EventStageEnd?.Invoke(stats);
    }

    private void OnDialogueIntroEnd()
    {
        oracle.EnableCheckArea();
    }
    #endregion
}

public class PerformanceStatsData
{
    public int Date;
    public int MainAbnormal { get; private set;}
    public int SubAbnormal { get ; private set; }
    public int Incorrect { get ; private set; }

    public PerformanceStatsData(int level)
    {
        Date = 26 + level;
    }

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