using UnityEngine;

public class GlowFurnitureObject : DraggableFurnitureObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lightSource;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] AudioClip clip;

    private bool isOn;

    private void Start()
    {
        isOn = Random.Range(0f, 1f) > 0.5f;
        UpdateDisplay();
    }

    public void OnInteracted()
    {
        isOn = !isOn;
        UpdateDisplay();

        AudioPoolManager.instance.PlayClipAtPoint(clip, transform.position);
    }

    private void UpdateDisplay()
    {
        renderer.material = isOn ? OnMaterial : OffMaterial;

        if (audioSource != null)
        {
            audioSource.enabled = isOn;
        }

        if (lightSource != null)
        {
            lightSource.enabled = isOn;
        }
    }
}
