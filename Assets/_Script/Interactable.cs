using UnityEngine;
using UnityEngine.Playables; // Required for PlayableDirector

[RequireComponent(typeof(Collider))] // Ensures the object can be hit by a raycast
public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayableDirector openAnim;
    [SerializeField] private PlayableDirector closeAnim;
    [SerializeField] private Interactable closeAnother;

    bool isOpen;

    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    private Camera mainCamera;

    float lastInteract;

    public void OnInteracted()
    {
        if(Time.time - lastInteract >= 2f) Interact();
    }

    void Start()
    {
        mainCamera = Camera.main;
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