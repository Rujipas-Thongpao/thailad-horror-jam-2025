using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "SO/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    public LevelData[] Levels;
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
