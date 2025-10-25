using System.Collections;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask placableLayer;

    [SerializeField]
    private Transform holdPoint;
    private Transform holdingObject;

    [SerializeField] private float rotateSpeed = 3f;
    Vector2 rotation;
    bool rotateAllowed = false;
    private InputActions inputActions;

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    public void RegisterObject(Transform obj)
    {
        holdingObject = obj;
        holdingObject.SetParent(holdPoint);
        holdingObject.localPosition = Vector3.zero;
        holdingObject.gameObject.layer = LayerMask.NameToLayer("HoldingItem");
        holdingObject.GetComponent<Collider>().enabled = false;
    }

    public void UnregisterObject()
    {
        holdingObject.gameObject.layer = LayerMask.NameToLayer("Default");
        if (holdingObject != null)
        {
            holdingObject.GetComponent<Collider>().enabled = true;
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
        //StartCoroutine(RotateObject());


        inputActions = new InputActions();

        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Axis.performed += ctx => {  rotation = ctx.ReadValue<Vector2>(); };
    }

    public void StopRotate()
    {
        SetRotateAllowed(false);
        //StopCoroutine(RotateObject());

        inputActions.Gameplay.Axis.performed -= ctx => {  rotation = ctx.ReadValue<Vector2>(); };
        inputActions.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        if(!rotateAllowed || holdingObject == null) return;

        rotation *= rotateSpeed;
        holdingObject.Rotate(Vector3.up, rotation.x, Space.World);
        holdingObject.Rotate(camera.transform.right, rotation.y, Space.World);
        
    }

    //public IEnumerator RotateObject()
    //{
    //    while (rotateAllowed)
    //    {

    //        yield return null;
    //    }
    //}

    public void PlaceItem()
    {
        RaycastHit hit;
        // Does the ray intersect any objects in detectableLayer?
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 10, placableLayer))
        {
            holdingObject.position = hit.point;
            holdingObject.rotation = Quaternion.identity;

            UnregisterObject();
        }
    }
}
