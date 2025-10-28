using UnityEngine;

public class PlayerSanityController : MonoBehaviour
{
    [SerializeField] private Material material;

    private float sanity = 100f;
    private PlayerManager playerManager;

    public void Init(PlayerManager _playerManager)
    {
        playerManager = _playerManager;
        sanity = 100f;
        UpdateMaterial();
    }

    public void DrainSanity(float amount)
    {
        sanity = Mathf.Clamp(sanity + amount, 0f, 100f);
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        var matLevel = Mathf.Lerp(1, 5, sanity / 100f);
        material.SetFloat("_power", matLevel);
    }

    private void OnDisable()
    {
        material.SetFloat("_power", 5f);
    }
}
