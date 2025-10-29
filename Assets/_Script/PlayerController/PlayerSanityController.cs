using UnityEngine;

public class PlayerSanityController : MonoBehaviour
{
    [SerializeField] private Material material;

    private float sanity = 100f;
    private float maxSanity = 100f;
    [SerializeField] private float regenRate = 5f; // sanity per second
    [Tooltip("Fraction of drained sanity that reduces maxSanity (0 = max never changes, 1 = max reduces same as sanity)")]
    [SerializeField] private float maxSanityReductionFactor = 0.1f;

    private PlayerManager playerManager;

    public void Init(PlayerManager _playerManager)
    {
        playerManager = _playerManager;
        maxSanity = 100f;
        sanity = maxSanity;
        UpdateMaterial();
    }

    // drain current sanity and reduce maxSanity at a smaller rate (controlled by maxSanityReductionFactor)
    public void DrainSanity(float amount)
    {
        // reduce current sanity first
        sanity = Mathf.Clamp(sanity + amount, 0f, maxSanity);

        // reduce maxSanity by a fraction of the drain amount
        var maxReduction = amount * Mathf.Clamp01(maxSanityReductionFactor);
        maxSanity = Mathf.Max(0f, maxSanity + maxReduction);

        // ensure current sanity does not exceed the new maximum
        sanity = Mathf.Min(sanity, maxSanity);

        UpdateMaterial();

        if(maxSanity == 0f)
        {
            // died
        }
    }

    private void Update()
    {
        if (sanity < maxSanity)
        {
            sanity += regenRate * Time.deltaTime;
            sanity = Mathf.Min(sanity, maxSanity);
            UpdateMaterial();
        }
    }

    private void UpdateMaterial()
    {
        // Avoid division by zero
        float sanityPercent = maxSanity > 0f ? sanity / maxSanity : 0f;
        var matLevel = Mathf.Lerp(-8, 5, sanityPercent);
        material.SetFloat("_power", matLevel);
    }

    private void OnDisable()
    {
        material.SetFloat("_power", 5f);
    }
}
