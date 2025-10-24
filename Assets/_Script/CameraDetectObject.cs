using UnityEngine;

public class CameraDetectObject : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask detectableLayer;

    // Track the last detected object so we can call OnUnCasted when it's no longer hit
    private DetectableObject lastDetected;

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

        RaycastHit hit;
        // Does the ray intersect any objects in detectableLayer?
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, detectableLayer))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            var detected = hit.transform.GetComponent<DetectableObject>();

            if (detected != null)
            {
                // If we hit a new detectable object, uncast the previous and cast the new one.
                if (detected != lastDetected)
                {
                    lastDetected?.OnUnCasted();
                    detected.OnCasted();
                    lastDetected = detected;
                }
                // If it's the same object as last frame, do nothing.
            }
            else
            {
                // Hit something in the layer mask that isn't DetectableObject
                if (lastDetected != null)
                {
                    lastDetected.OnUnCasted();
                    lastDetected = null;
                }
            }
        }
        else
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            // No hit: ensure any previously casted object is uncasted.
            if (lastDetected != null)
            {
                lastDetected.OnUnCasted();
                lastDetected = null;
            }
        }
    }

    public void SetEnable(bool enable)
    {
        isEnable = enable;
        //if (!isEnable && lastDetected != null)
        //{
        //    lastDetected.OnUnCasted();
        //    lastDetected = null;
        //}
    }

    public Transform GetLastDetectedObject()
    {
        return lastDetected?.transform;
    }
}
