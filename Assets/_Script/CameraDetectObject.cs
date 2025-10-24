using UnityEngine;

public class CameraDetectObject : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask detectableLayer;


    void Start()
    {
        if( camera == null)
        {
            camera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, detectableLayer))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.transform.name);
            hit.transform.GetComponent<DetectableObject>()?.OnCasted();
        }
        else
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }
}
