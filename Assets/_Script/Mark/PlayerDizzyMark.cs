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
        PlayerEffectController.Instance.Dizzyness(intensity);
    }

    protected override void OnPlayerNearby()
    {
        PlayerEffectController.Instance.Dizzyness(intensity);
    }
}
