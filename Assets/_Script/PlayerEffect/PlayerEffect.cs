using UnityEngine;

public abstract class PlayerEffect : MonoBehaviour
{
    public abstract void PlayEffect(float intensity);
    public abstract void StopEffect();
}
