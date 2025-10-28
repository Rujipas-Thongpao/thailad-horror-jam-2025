using UnityEngine;

public class FurnitureSetup : MonoBehaviour
{
    [SerializeField] private Transform[] ghostSpawnPoints;
    public Transform[] GhostSpawnPoints => ghostSpawnPoints;
    [SerializeField] private FurnitureObject[] objectSpawns;
    // [SerializeField] private FurnitureObject[] interactable;

    public FurnitureObject[] ObjSpawners => objectSpawns;
}
