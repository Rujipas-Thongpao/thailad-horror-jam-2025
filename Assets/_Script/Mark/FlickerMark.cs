using UnityEngine;

public class FlickerMark : BaseMark
{
    public override void Init(DetectableObject obj, int intensity)
    {
        base.Init(obj, intensity);
    }

    private void Flicker()
    {
        if (!TryTriggerMark()) return;

        Debug.Log("Flicker");
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
