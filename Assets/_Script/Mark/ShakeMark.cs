using UnityEngine;

public class ShakeMark : BaseMark
{
    public ShakeMark(int intensity) : base(intensity)
    {
        
    }

    public override void Init(DetectableObject obj)
    {
        base.Init(obj);
        Debug.Log("Shake", obj.gameObject);
    }

    private void Shake()
    {
        if (!TryTriggerMark()) return;

        RoomController.Instance.ShakeRoom();
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
