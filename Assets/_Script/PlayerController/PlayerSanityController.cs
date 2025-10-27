using UnityEngine;

public class PlayerSanityController : MonoBehaviour
{
    float sanity = 100f;
    private PlayerManager playerManager;
    [SerializeField] private Material material;

    public void Init(PlayerManager playerManager)
    {
        sanity = 100f;
        ModifySanity(100f);
    }

    public void ModifySanity(float amount)
    {
        sanity = Mathf.Clamp(sanity + amount, 0f, 100f);
        var matLevel = Mathf.Lerp(1, 5, sanity / 100f);
        material.SetFloat("_power", matLevel);
    }

    private void OnDisable()
    {
        material.SetFloat("_power", 5f);
    }
}
