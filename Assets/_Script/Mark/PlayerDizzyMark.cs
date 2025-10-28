using UnityEngine;

public class PlayerDizzyMark : BaseMark
{
    public PlayerDizzyMark(int _intensity) : base(_intensity)
    {
    }

    protected override void OnImpactHit()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnPicked()
    {
        if (!TryTriggerMark()) return;
        PlayerEffectController.Instance.Dizzyness(intensity);
    }

    protected override void OnPlayerNearby()
    {
        if (!TryTriggerMark()) return;
        PlayerEffectController.Instance.Dizzyness(Mathf.Clamp(intensity, 0, 1));
    }
}
