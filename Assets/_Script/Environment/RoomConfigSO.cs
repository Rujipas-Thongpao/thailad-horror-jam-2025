using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "SO/RoomConfig")]
public class RoomConfigSO : ScriptableObject
{
    public DetectableObject[] nonMarkObj;
    public DetectableObject[] markableObj;

    public DetectableObject GetNonMarkObject()
    {
        int nonMarkLength = nonMarkObj.Length;
        int rand = Random.Range(0, nonMarkLength + markableObj.Length);

        return rand < nonMarkLength ? nonMarkObj[rand] : markableObj[rand - nonMarkLength];
    }
}
