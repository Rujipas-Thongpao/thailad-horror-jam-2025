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
        Debug.Log("Dizzy Triggered On pick", obj.gameObject);
        PlayerEffectController.Instance.Dizzyness(intensity);
    }

    protected override void OnPlayerNearby()
    {
        if (!TryTriggerMark()) return;
        Debug.Log("Dizzy Triggered Nearby", obj.gameObject);
        PlayerEffectController.Instance.Dizzyness(Mathf.Clamp(intensity, 0, 1));
    }
}
