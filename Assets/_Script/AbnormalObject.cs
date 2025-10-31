using UnityEngine;

public class AbnormalObject : DetectableObject
{
    [Header("Abnormal")]
    [SerializeField]
    private GameObject[] abnormalPrefabs;

    [SerializeField]
    private Transform[] cursePoints;

   

    public BaseMark Mark { get; private set; }

    private AbnormalRandomSound randomSound;

    public void ApplyMark(BaseMark mark)
    {
        Mark = mark;
        SpawnAbnormal();
        randomSound = gameObject.GetComponent<AbnormalRandomSound>();
        randomSound.StartRandomSound();
    }

    private void SpawnAbnormal()
    {
        int amount = Random.Range(1, cursePoints.Length / 2 + 1);

        var shufflePoints = LogicHelper.ShuffleArray(cursePoints);
        var abnormalPrefab = abnormalPrefabs[Random.Range(0, abnormalPrefabs.Length)];

        for (int i = 0; i < amount; i++)
        {
            var point = shufflePoints[i];
            var abnormalInstance = Instantiate(abnormalPrefab, point.position, point.rotation);
            abnormalInstance.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            abnormalInstance.transform.SetParent(point);
        }
    }
}
