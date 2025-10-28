using UnityEngine;

public class GhostMark : BaseMark
{
    public GhostMark(int intensity) : base(intensity)
    {
    }

    protected override void OnPlayerNearby()
    {
        // Implement behavior when player is nearby
    }

    protected override void OnPicked()
    {
        // Implement behavior when mark is picked
        if(!TryTriggerMark()) return;

        SpawnGhost();
    }

    protected override void OnImpactHit()
    {
        // Implement behavior when mark is hit by impact
    }


    private void SpawnGhost()
    {
        RoomController.Instance.SpawnGhost();
    }
}
