using UnityEngine;

public class FurnitureSetup : MonoBehaviour
{
    [SerializeField] private FurnitureObject[] objectSpawns;
    // [SerializeField] private FurnitureObject[] interactable;

    public FurnitureObject[] ObjSpawners => objectSpawns;
}
