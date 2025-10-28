public class FlickerMark : BaseMark
{
    public FlickerMark(int intensity) : base(intensity)
    {
        
    }

    public override void Init(DetectableObject obj)
    {
        base.Init(obj);
    }

    private void Flicker()
    {
        if (!TryTriggerMark()) return;
        RoomController.Instance.StartFlickerLight();
    }

    protected override void OnPlayerNearby()
    {
        if (intensity == 0) return;

        Flicker();
    }

    protected override void OnPicked()
    {
        Flicker();
    }

    protected override void OnImpactHit()
    {
        if (intensity == 0) return;

        Flicker();
    }
}
