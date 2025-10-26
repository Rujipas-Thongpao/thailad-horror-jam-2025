using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomController roomController;
    [SerializeField] private OracleTriggerArea oracle;
    [SerializeField] private LevelConfigSO config;

    private readonly List<BaseMark> marks = new();
    private readonly List<BaseMark> mainAbnormal = new();

    private GameObject hallway;
    private int mainAbnormalType;
    private int progress;

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
        oracle.Init(OnAbnormalSecured);
        hallway = Instantiate(config.Hallways[level]);

        progress = 0;

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
        if (mainAbnormal.Contains(mark))
        {
            progress++;
        }

        if (progress >= mainAbnormal.Count)
        {
            Debug.Log("LEVEL COMPLETED");
            Dispose();
        }
    }

    #endregion
}
