using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StageLeaveArea : MonoBehaviour, IInteractable
{
    private event Action eventInteracted;

    private Collider col;

    public void Init(Action _eventInteracted)
    {
        eventInteracted = _eventInteracted;

        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void Dispose()
    {
        eventInteracted = null;
    }

    public void Enable()
    {
        col.enabled = true;
    }

    public void OnInteracted()
    {
        eventInteracted?.Invoke();
    }
}
