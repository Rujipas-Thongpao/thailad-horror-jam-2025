using UnityEngine;
using UnityEngine.Playables; // Required for PlayableDirector

[RequireComponent(typeof(Collider))] // Ensures the object can be hit by a raycast
public class Interactable : MonoBehaviour
{
    [SerializeField] private PlayableDirector openAnim;
    [SerializeField] private PlayableDirector closeAnim;

    bool isOpen;

    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        if (mainCamera == null) return; 

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        if (!isOpen)
        {
            openAnim?.Play();
        }
        else
        {
            closeAnim?.Play();
        }

        isOpen = !isOpen;
    }
}