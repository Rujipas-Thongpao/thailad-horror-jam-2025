using UnityEngine;

public class GlowFurnitureObject : DraggableFurnitureObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lightSource;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;

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

        SoundManager.PlaySound(clip);
    }

    private void UpdateDisplay()
    {
        renderer.material = isOn ? OnMaterial : OffMaterial;

        if (lightSource == null) return;

        lightSource.enabled = isOn;
    }
}
