using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    [SerializeField] private Transform[] places;

    public int GetAmount()
    {
        int maxAmount = places.Length;
        return Random.Range(maxAmount / 2, maxAmount);
    }

    public void PlaceItems(DetectableObject[] objects)
    {
        var shuffledPlaces = (Transform[])places.Clone();

        for (int i = 0; i < shuffledPlaces.Length; i++)
        {
            var rand = Random.Range(i, shuffledPlaces.Length);
            (shuffledPlaces[i], shuffledPlaces[rand]) = (shuffledPlaces[rand], shuffledPlaces[i]);
        }

        for (int i = 0; i < objects.Length; i++)
        {
            var angleY = Random.Range(0f, 360f);
            var rotation = Quaternion.Euler(new Vector3(0, angleY, 0));
            objects[i].transform.SetPositionAndRotation(shuffledPlaces[i].position, rotation);
        }
    }
}
