using Cysharp.Threading.Tasks;
using SmoothShakeFree;
using UnityEngine;

public class HandTremors : PlayerEffect
{
    [SerializeField] private ObjectHolder objectHolder;
    [SerializeField] private SmoothShake shaker;
    private float defaultRotateSpeed;

    public override void PlayEffect(float intensity)
    {
        defaultRotateSpeed = objectHolder.RotateSpeed;
        objectHolder.RotateSpeed = intensity * .1f;
        objectHolder.Invert = true;
        shaker.timeSettings.holdDuration = 5f;
        shaker.StartShake();
    }

    public override void StopEffect()
    {
        objectHolder.RotateSpeed = defaultRotateSpeed;
        objectHolder.Invert = false;
        shaker.StopShake();
    }
}
