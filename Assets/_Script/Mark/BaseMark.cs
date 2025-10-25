using UnityEngine;

public abstract class BaseMark : MonoBehaviour
{
    protected DetectableObject obj;

    protected bool isActive;

    public virtual void Init(DetectableObject _obj)
    {
        obj = _obj;
    }

    public virtual void Disable()
    {
        isActive = false;
    }
}
