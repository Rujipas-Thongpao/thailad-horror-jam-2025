using UnityEngine;

public class FurnitureSetup : MonoBehaviour
{
    [SerializeField] private Transform ghostPrefabs;
    public Transform GhostPrefabs => ghostPrefabs;

    [SerializeField] private Transform[] ghostSpawnPoints;
    public Transform[] GhostSpawnPoints => ghostSpawnPoints;

    [SerializeField] private FurnitureObject[] objectSpawns;
    public FurnitureObject[] ObjSpawners => objectSpawns;

}
