using UnityEngine;

public enum Startmode { 
    On, Off, Random
}
public class GlowFurnitureObject : DraggableFurnitureObject, IInteractable
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lightSource;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] AudioClip clip;

    [SerializeField]
    private Startmode startMode = Startmode.Random;

    private bool isOn;

    private void Start()
    {
        switch (startMode)
        {
            case Startmode.On:
                isOn = true;
                break;
            case Startmode.Off:
                isOn = false;
                break;
            case Startmode.Random:
                isOn = Random.Range(0f, 1f) > 0.5f;
                break;
        }
        //isOn = Random.Range(0f, 1f) > 0.5f;
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
