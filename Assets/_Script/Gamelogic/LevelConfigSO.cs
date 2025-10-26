using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "SO/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    public LevelData[] Levels;

    public GameObject[] Hallways;
}

[Serializable]
public class LevelData
{
    [Header ("Abnormal Intensity")]
    public int[] MainIntensity;
    public int[] SubIntensity;
}
