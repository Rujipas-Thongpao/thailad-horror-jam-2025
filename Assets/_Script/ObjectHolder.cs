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
    private IInteractable interactable;

    [SerializeField] private float rotateSpeed = 3f;
    public float RotateSpeed
    {
        get => rotateSpeed;
        set => rotateSpeed = value;
    }

    [SerializeField] private bool invert = false;
    public bool Invert
    {
        get => invert;
        set => invert = value;
    }

    private Vector2 rotation;
    private bool rotateAllowed;
    private InputActions inputActions;

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    public void RegisterObject(DetectableObject obj, IInteractable _interactable)
    {
        holdingObject = obj;
        interactable = _interactable;

        holdingObject.GetComponent<Collider>().enabled = false;
        holdingObject.transform.SetParent(holdPoint);
        holdingObject.transform.localPosition = Vector3.zero;
        holdingObject.gameObject.layer = LayerMask.NameToLayer("HoldingItem");
    }

    private void UnregisterObject()
    {
        holdingObject.gameObject.layer = LayerMask.NameToLayer("Default");

        if (holdingObject == null) return;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject.transform.SetParent(null);
        holdingObject = null;
    }

    public void ApplyRotation(Vector2 rotate)
    {
        rotation = rotate;
    }

    public void StartRotate()
    {
        rotateAllowed = true;
    }

    public void StopRotate()
    {
        rotateAllowed = false;
    }

    private void FixedUpdate()
    {
        if (!rotateAllowed || !holdingObject) return;

        holdingObject.transform.localPosition = Vector3.zero;

        rotation *= rotateSpeed;

        var obj = holdingObject.transform;
        var center = holdingObject.Center;

        obj.RotateAround(center.position, Vector3.up * (invert ? -1 : 1), rotation.x);
        obj.RotateAround(center.position, camera.transform.right * (invert ? -1 : 1), rotation.y);
    }

    public void PlaceItem()
    {
        RaycastHit hit;
        // Does the ray intersect any objects in detectableLayer?
        if (!Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit,
                10, placableLayer)) return;

        holdingObject.OnPlaced(hit.point);

        UnregisterObject();
    }

    public void TryInteract()
    {
        interactable?.OnInteracted();
    }
}
