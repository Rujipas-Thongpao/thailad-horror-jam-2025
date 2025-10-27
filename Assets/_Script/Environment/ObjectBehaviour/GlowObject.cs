using UnityEngine;

public class GlowObject : AbnormalObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lightSource;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;

    [SerializeField] AudioClip clip;

    private bool isOn;

    private void Start()
    {
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
