using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "SO/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    public LevelData[] Levels;
    public int Length;
}

[Serializable]
public class LevelData
{
    [Header ("Abnormal Intensity")]
    public int[] MainIntensity;
    public int[] SubIntensity;
    public FurnitureSetup furniture;
    public GameObject Hallway;
}
