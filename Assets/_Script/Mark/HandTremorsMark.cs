public class HandTremorsMark : BaseMark
{
    public HandTremorsMark(int intensity, string name) : base(intensity, name)
    {
    }

    protected override void OnImpactHit()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnPicked()
    {
        if (!TryTriggerMark()) return;
        PlayerEffectController.Instance.HandTremor(intensity);
    }

    protected override void OnPlayerNearby()
    {
        if (intensity <= 0) return;
        if (!TryTriggerMark()) return;

        PlayerEffectController.Instance.HandTremor(1.0f);
    }
}
