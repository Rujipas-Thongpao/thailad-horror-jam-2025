using UnityEngine;

public class LampObject : AbnormalObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lampLight;
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
            lampLight.enabled = true;
        }
        else
        {
            renderer.material = OffMaterial;
            lampLight.enabled = false;
        }
    }
}
