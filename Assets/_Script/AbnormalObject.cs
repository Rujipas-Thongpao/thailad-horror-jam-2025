using UnityEngine;

public class AbnormalObject : DetectableObject
{
    [SerializeField]
    private GameObject[] abnormalPrefabs;

    [SerializeField]
    private Transform[] cursePoints; 

    public BaseMark Mark { get; private set; }

    public void ApplyMark(BaseMark mark)
    {
        Mark = mark;
        SpawnAbnormal();
    }

    [ContextMenu("SpawnAbnormal")]
    public void SpawnAbnormal()
    {
        int amount = Random.Range(1, cursePoints.Length);

        for (int i = 0; i < amount; i++)
        {
            var selectedPoint = cursePoints[Random.Range(0, cursePoints.Length)];

            var abnormalPrefab = abnormalPrefabs[Random.Range(0, abnormalPrefabs.Length)];

            var abnormalInstance = Instantiate(abnormalPrefab, selectedPoint.position, selectedPoint.rotation);
            abnormalInstance.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            abnormalInstance.transform.SetParent(selectedPoint);
        }
    }
}
