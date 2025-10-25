using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField]
    private Dizzyness dizzyness;

    public void Dizzyness(float intensity)
    {
        dizzyness.PlayEffect(2);
    }

    public void StopDizzyness()
    {
        dizzyness.StopEffect();
    }
}
