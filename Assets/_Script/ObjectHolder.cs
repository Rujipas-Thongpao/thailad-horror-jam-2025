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
    private DetectableObject holdingObject;

    [SerializeField] private float rotateSpeed = 3f;
    private Vector2 rotation;
    private bool rotateAllowed = false;
    private InputActions inputActions;

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    public void RegisterObject(DetectableObject obj)
    {
        holdingObject = obj;
        holdingObject.transform.SetParent(holdPoint);
        holdingObject.transform.localPosition = Vector3.zero;
        holdingObject.gameObject.layer = LayerMask.NameToLayer("HoldingItem");
        holdingObject.GetComponent<Collider>().enabled = false;
    }

    private void UnregisterObject()
    {
        holdingObject.gameObject.layer = LayerMask.NameToLayer("Default");

        if (holdingObject == null) return;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject.transform.SetParent(null);
        holdingObject = null;
    }

    private void SetRotateAllowed(bool allowed)
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

        inputActions = new InputActions();

        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Axis.performed += ctx => {  rotation = ctx.ReadValue<Vector2>(); };
    }

    public void StopRotate()
    {
        SetRotateAllowed(false);

        inputActions.Gameplay.Axis.performed -= ctx => {  rotation = ctx.ReadValue<Vector2>(); };
        inputActions.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        if(!rotateAllowed || !holdingObject) return;

        rotation *= rotateSpeed;
        holdingObject.transform.Rotate(Vector3.up, rotation.x, Space.World);
        holdingObject.transform.Rotate(camera.transform.right, rotation.y, Space.World);
        
    }

    public void PlaceItem()
    {
        RaycastHit hit;
        // Does the ray intersect any objects in detectableLayer?
        if (!Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit,
                10, placableLayer)) return;
    
        holdingObject.transform.position = hit.point;
        holdingObject.transform.rotation = Quaternion.identity;
        holdingObject.OnPlaced();

        UnregisterObject();
    }
}
