using UnityEngine;

public abstract class BaseMark
{
    private const float DEFAULT_CD = 20f;

    protected DetectableObject obj;
    protected int intensity;

    protected bool isActive;
    protected float lastTriggerTime;

    public virtual void Init(DetectableObject _obj, int _intensity)
    {
        obj = _obj;
        intensity = _intensity;

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

    public bool TryTriggerMark()
    {
        if (Time.time > lastTriggerTime + DEFAULT_CD) return false;

        lastTriggerTime = Time.time;

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
