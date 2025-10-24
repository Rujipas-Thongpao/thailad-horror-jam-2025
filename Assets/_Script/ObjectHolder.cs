using System.Collections;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField]
    private Transform holdPoint;
    private Transform holdingObject;

    [SerializeField] private float rotateSpeed = 3f;
    Vector2 rotation;
    bool rotateAllowed = false;
    private InputActions inputActions;

    public void RegisterObject(Transform obj)
    {
        holdingObject = obj;
        holdingObject.SetParent(holdPoint);
        holdingObject.localPosition = Vector3.zero;
    }

    public void UnregisterObject()
    {
        if (holdingObject != null)
        {
            holdingObject.SetParent(null);
            holdingObject = null;
        }
    }

    public void SetRotateAllowed(bool allowed)
    {
        rotateAllowed = allowed;
    }
    public void SetRotate(Vector2 rotate)
    {
        rotation = rotate;
    }

    public void StartRotate()
    {
        SetRotateAllowed(true);
        StartCoroutine(RotateObject());


        inputActions = new InputActions();

        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Axis.performed += ctx => {  rotation = ctx.ReadValue<Vector2>(); };
    }

    public void StopRotate()
    {
        SetRotateAllowed(false);
        StopCoroutine(RotateObject());

        inputActions.Gameplay.Axis.performed -= ctx => {  rotation = ctx.ReadValue<Vector2>(); };
        inputActions.Gameplay.Disable();
    }

    public IEnumerator RotateObject()
    {
        while (rotateAllowed)
        {
            rotation *= rotateSpeed;
            holdingObject.Rotate(Vector3.up, rotation.x * Time.deltaTime, Space.World);
            holdingObject.Rotate(Vector3.right, rotation.y * Time.deltaTime, Space.World);

            yield return null;
        }
    }
}
