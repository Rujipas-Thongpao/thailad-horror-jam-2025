using UnityEngine;

public class CameraDetectObject : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask detectableLayer;

    // Track the last detected object so we can call OnUnCasted when it's no longer hit
    private IDetectable lastDetected;

    private bool isEnable = true;

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        if (!isEnable) return;

        // Does the ray intersect any objects in detectableLayer?
        var dir = camera.transform.TransformDirection(Vector3.forward);

        if (!Physics.Raycast(camera.transform.position, dir, out var hit, Mathf.Infinity, detectableLayer))
        {
            // No hit: ensure any previously cast object is uncasted.
            if (lastDetected == null) return;

            lastDetected.OnUnhovered();
            lastDetected = null;
            return;
        }

        if (!hit.transform.TryGetComponent<IDetectable>(out var detected))
        {
            // Hit something in the layer mask that isn't IDetectable
            if (lastDetected == null) return;

            lastDetected.OnUnhovered();
            lastDetected = null;
        }

        // If it's the same object as last frame, do nothing.
        if (detected == lastDetected) return;

        // If we hit a new detectable object, uncast the previous and cast the new one.
        lastDetected?.OnUnhovered();
        detected.OnHovered();
        lastDetected = detected;
    }

    public void SetEnable(bool enable)
    {
        isEnable = enable;

        if (isEnable || lastDetected == null) return;

        lastDetected.OnUnhovered();
        lastDetected = null;
    }

    public DetectableObject GetLastDetectedObject()
    {
        return lastDetected as DetectableObject;
    }

    public FurnitureObject GetLastFurnitureObject()
    {
        return lastDetected as FurnitureObject;
    }
}
