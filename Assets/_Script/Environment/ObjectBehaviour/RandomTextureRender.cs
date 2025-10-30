using UnityEngine;

public class RandomTextureRender : MonoBehaviour
{
    [SerializeField] private int width = 2;
    [SerializeField] private int height = 2;
    [SerializeField] private bool setTilling;
    private MeshRenderer mesh;
    private Material material;

    private void Start()
    {
        if (width <= 0) width = 1;
        if (height <= 0) height = 1;

        mesh = GetComponent<MeshRenderer>();
        material = new Material(mesh.material);
        mesh.material = material;
        var x = Random.Range(0, width) / (float)width;
        var y = Random.Range(0, height) / (float)height;
        material.mainTextureOffset = new Vector2(x, y);
        if (setTilling)
        {
            material.mainTextureScale = new Vector2(1f / width, 1f / height);
        }
    }
}
