using System;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public event Action<DetectableObject, IInteractable> EventObjectPicked;
    public event Action EventObjectUnpicked;
    public event Action EventSecureEnabled;
    public event Action<BaseMark> EventAbnormalSecured;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask placableLayer;

    [SerializeField]
    private Transform holdPoint;
    private DetectableObject holdingObject;
    private IInteractable interactable;

    [SerializeField] private float rotateSpeed = 3f;

    [SerializeField] AudioClip placeObj;

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

    public bool CanSecure { get; private set; }

    public bool HaveObject => holdingObject != null;
    public bool IsObjectAbnormal
    {
        get
        {
            var obj = holdingObject as AbnormalObject;
            return obj != null && obj.Mark != null;
        }
    }

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

        EventObjectPicked?.Invoke(obj, interactable);
    }

    private void UnregisterObject()
    {
        holdingObject.gameObject.layer = LayerMask.NameToLayer("Default");

        if (holdingObject == null) return;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject.transform.SetParent(null);
        holdingObject = null;

        EventObjectUnpicked?.Invoke();
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

        SoundManager.PlaySound(placeObj);

        DisableSecureObject();
        UnregisterObject();
    }

    public void TryInteract()
    {
        interactable?.OnInteracted();
    }

    public void EnableSecureObject()
    {
        CanSecure = true;
        EventSecureEnabled?.Invoke();
    }

    public void DisableSecureObject()
    {
        CanSecure = false;
    }

    public void SecureObject()
    {
        var obj = holdingObject as AbnormalObject;

        EventAbnormalSecured?.Invoke(obj?.Mark);

        obj?.Mark.Disable();
        holdingObject.gameObject.SetActive(false);
        UnregisterObject();

        //TODO: spawn secured container on player's hand (object holder).
    }
}
