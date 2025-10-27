using UnityEngine;
using UnityEngine.Playables; // Required for PlayableDirector

[RequireComponent(typeof(Collider))] // Ensures the object can be hit by a raycast
public class Interactable : MonoBehaviour
{
    [SerializeField] private PlayableDirector openAnim;
    [SerializeField] private PlayableDirector closeAnim;
    [SerializeField] private Interactable closeAnother;

    bool isOpen;

    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    private Camera mainCamera;

    float lastInteract;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        if (mainCamera == null) return; 

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == this.gameObject && Input.GetKeyDown(interactionKey) && Time.time - lastInteract >= 1f)
            {
                Interact();
            }
        }
    }

    public void ForceClose()
    {
        if (isOpen)
        {
            closeAnim?.Play();

            if(closeAnother) closeAnother.ForceClose();
        }

        isOpen = false;
    }

    private void Interact()
    {
        lastInteract = Time.time;

        if (!isOpen)
        {
            openAnim?.Play();
        }
        else
        {
            closeAnim?.Play();

            if(closeAnother) closeAnother.ForceClose();
        }

        isOpen = !isOpen;
    }
}