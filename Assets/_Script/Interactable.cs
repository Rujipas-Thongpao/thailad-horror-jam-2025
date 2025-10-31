using UnityEngine;
using UnityEngine.Playables; // Required for PlayableDirector

[RequireComponent(typeof(Collider))] // Ensures the object can be hit by a raycast
public class Interactable : MonoBehaviour, IInteractable, IDetectable
{
    [SerializeField] private PlayableDirector openAnim;
    [SerializeField] private PlayableDirector closeAnim;
    [SerializeField] private Interactable closeAnother;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    private bool isOpen;
    private float lastInteract;

    public void OnInteracted()
    {
        if (Time.time - lastInteract < 1.25f) return;

        Interact();
    }

    public void OnHovered()
    {
        
    }

    public void OnUnhovered()
    {
        
    }

    public void ForceClose()
    {
        if (isOpen)
        {
            closeAnim?.Play();
            AudioPoolManager.instance.PlayClipAtPoint(closeClip, transform.position);

            if (closeAnother) closeAnother.ForceClose();
        }

        isOpen = false;
    }

    private void Interact()
    {
        lastInteract = Time.time;

        if (!isOpen)
        {
            openAnim?.Play();
            AudioPoolManager.instance.PlayClipAtPoint(openClip, transform.position);
        }
        else
        {
            closeAnim?.Play();
            AudioPoolManager.instance.PlayClipAtPoint(closeClip, transform.position);

            if (closeAnother) closeAnother.ForceClose();
        }

        isOpen = !isOpen;

    }
}