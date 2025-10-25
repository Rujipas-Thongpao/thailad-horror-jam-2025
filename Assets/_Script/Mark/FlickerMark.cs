using UnityEngine;

public class FlickerMark : BaseMark
{
    public FlickerMark(int intensity) : base(intensity)
    {
        
    }

    public override void Init(DetectableObject obj)
    {
        base.Init(obj);
        Debug.Log("Flicker", obj.gameObject);
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
