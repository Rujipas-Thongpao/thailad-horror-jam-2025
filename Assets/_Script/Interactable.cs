using UnityEngine;
using UnityEngine.Playables; // Required for PlayableDirector

[RequireComponent(typeof(Collider))] // Ensures the object can be hit by a raycast
public class Interactable : MonoBehaviour, IInteractable, IDetectable
{
    [SerializeField] private PlayableDirector openAnim;
    [SerializeField] private PlayableDirector closeAnim;
    [SerializeField] private Interactable closeAnother;
    [SerializeField] private AudioClip clip;

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
        }
        else
        {
            closeAnim?.Play();

            if (closeAnother) closeAnother.ForceClose();
        }

        isOpen = !isOpen;

        SoundManager.PlaySound(clip);
    }
}