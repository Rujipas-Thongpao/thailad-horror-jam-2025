using UnityEngine;

public class MonitorObject : DraggableFurnitureObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
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
        if (isOn)
        {
            renderer.material = OnMaterial;
        }
        else
        {
            renderer.material = OffMaterial;
        }
    }
}
