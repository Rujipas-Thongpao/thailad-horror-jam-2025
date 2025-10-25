using UnityEngine;

public class ShakeMark : BaseMark
{
    public override void Init(DetectableObject obj, int intensity)
    {
        base.Init(obj, intensity);
    }

    private void Shake()
    {
        if (!TryTriggerMark()) return;

        Debug.Log("Shake");
    }

    protected override void OnPlayerNearby()
    {
        if (intensity == 0) return;

        Shake();
    }

    protected override void OnPicked()
    {
        Shake();
    }

    protected override void OnImpactHit()
    {
        Shake();
    }
}
