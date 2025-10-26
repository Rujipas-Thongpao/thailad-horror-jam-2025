using UnityEngine;

public abstract class BaseMark
{
    private const float DEFAULT_CD = 20f;

    protected readonly int intensity;

    private DetectableObject obj;
    private bool isActive;
    private float nextTriggerTime;

    protected BaseMark(int _intensity)
    {
        intensity = _intensity;
        nextTriggerTime = 0f;
        isActive = true;
    }

    public virtual void Init(DetectableObject _obj)
    {
        obj = _obj;

        obj.EventPlayerNearby += OnPlayerNearby;
        obj.EventPicked += OnPicked;
        obj.EventImpactHit += OnImpactHit;
    }

    public virtual void Dispose()
    {
        obj.EventPlayerNearby -= OnPlayerNearby;
        obj.EventPicked -= OnPicked;
        obj.EventImpactHit -= OnImpactHit;
    }

    protected bool TryTriggerMark()
    {
        if (!isActive) return false;
        if (Time.time <= nextTriggerTime) return false;

        nextTriggerTime = Time.time + DEFAULT_CD;

        return true;
    }

    public virtual void Disable()
    {
        isActive = false;
    }

    #region event listeners
    protected abstract void OnPlayerNearby();
    protected abstract void OnPicked();
    protected abstract void OnImpactHit();
    #endregion
}
