using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] polaroidImage;

    Image polaroid;
    Sprite[] rand;
    
    private void Start()
    {
        rand = LogicHelper.ShuffleArray(polaroidImage);
        polaroid = GetComponent<Image>();
    }

    public void UseImage(int index)
    {
        float randX = Random.Range(-100, 100);
        float randY = Random.Range(-20, 20);
        transform.localPosition = new Vector3(randX, randY);
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-20, 20));

        polaroid.sprite = polaroidImage[index];
    }
}
