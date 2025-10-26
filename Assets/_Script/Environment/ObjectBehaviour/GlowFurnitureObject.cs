using UnityEngine;

public class GlowFurnitureObject : DraggableFurnitureObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lightSource;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;

    private bool isOn;

    private void Start()
    {
        UpdateDisplay();
    }

    public void OnInteracted()
    {
        isOn = !isOn;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        renderer.material = isOn ? OnMaterial : OffMaterial;

        if (lightSource == null) return;

        lightSource.enabled = isOn;
    }
}
