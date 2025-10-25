using UnityEngine;

public class AttachAbnormalController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] abnormalPrefabs;

    [SerializeField]
    private Transform[] cursePoints; 

    [ContextMenu("SpawnAndAttachAbnormalToTarget")]
    public void SpawnAndAttachAbnormalToTarget()
    {
        int amount = Random.Range(1, cursePoints.Length);

        for (int i = 0; i < amount; i++)
        {
            Transform selectedPoint = cursePoints[Random.Range(0, cursePoints.Length)];

            var abnormalPrefab = abnormalPrefabs[Random.Range(0, abnormalPrefabs.Length)];

            GameObject abnormalInstance = Instantiate(abnormalPrefab, selectedPoint.position, selectedPoint.rotation);
            abnormalInstance.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            abnormalInstance.transform.SetParent(selectedPoint);
        }
    }
}
