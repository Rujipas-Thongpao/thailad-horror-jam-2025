public class PoltergeistMark : BaseMark
{
    // Constructor to pass required intensity parameter to BaseMark
    public PoltergeistMark(int intensity) : base(intensity)
    {
    }

    protected override void OnPlayerNearby()
    {
        // TODO: Add logic for when the player is nearby
        Poltergeist(intensity);
    }

    private async void Poltergeist(float intensity)
    {
        if (!TryTriggerMark()) return;
        await RoomController.Instance.PoltergeistRoutine(1+intensity);
    }

    protected override void OnPicked()
    {
        // TODO: Add logic for when the mark is picked
    }

    protected override void OnImpactHit()
    {
        // TODO: Add logic for when the mark is hit by an impact
    }
}
