using UnityEngine;

public class LightObject : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private ParticleSystem lightEffect;

    private MeshRenderer mesh;
    private Material lightOn;
    private Material lightOff;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Init(Material _lightOn, Material _lightOff)
    {
        lightOn = _lightOn;
        lightOff = _lightOff;
        mesh.material = lightOn;
    }

    public void SetLight(bool isOn, bool withEffect = false)
    {
        light.enabled = isOn;
        mesh.material = isOn ? lightOn : lightOff;

        var r = Random.Range(0f, 1f);
        if(isOn && r < 0.5f && withEffect)
        {
            PlayEffect();
        }
    }

    public void PlayEffect()
    {
        if (lightEffect != null)
        {
            lightEffect.Play();
        }
    }
}
