using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "SO/RoomConfig")]
public class FurnitureConfigSO : ScriptableObject
{
    public DetectableObject[] NormalObj;
    public AbnormalObject[] AbnormalObj;

    public DetectableObject GetNormalObject()
    {
        var rand = Random.Range(0, NormalObj.Length);
        return NormalObj[rand];
    }

    public AbnormalObject GetAbnormalObject()
    {
        var rand = Random.Range(0, AbnormalObj.Length);
        return AbnormalObj[rand];
    }
}
