using UnityEngine;

public class UIButtonPrompt : MonoBehaviour
{
    [SerializeField] private GameObject pickup;
    [SerializeField] private GameObject place;
    [SerializeField] private GameObject drag;
    [SerializeField] private GameObject interact;
    [SerializeField] private GameObject secure;
    [SerializeField] private GameObject leave;

    private CameraDetectObject detect;
    private ObjectHolder holder;

    public void Init(PlayerManager playerManager)
    {
        detect = playerManager.CameraDetectObject;
        holder = playerManager.ObjectHolder;

        detect.EventFindDetectableObject += OnFindDetectableObject;
        detect.EventFindInteractableObject += OnFindInteractableObject;
        detect.EventQuitDetect += Hide;

        holder.EventObjectPicked += OnObjectPicked;
        holder.EventObjectUnpicked += OnObjectUnpicked;
        holder.EventSecureEnabled += OnSecureEnabled;

        Hide();
    }

    public void Dispose()
    {
        detect.EventFindDetectableObject -= OnFindDetectableObject;
        detect.EventFindInteractableObject -= OnFindInteractableObject;
        detect.EventQuitDetect -= Hide;

        holder.EventObjectPicked -= OnObjectPicked;
        holder.EventObjectUnpicked -= OnObjectUnpicked;
        holder.EventSecureEnabled -= OnSecureEnabled;

        detect = null;
        holder = null;
    }

    private void OnFindDetectableObject(IDetectable detectable)
    {
        if (detectable as DetectableObject)
        {
            ShowPickup();
            return;
        }

        // if (detectable as DraggableFurnitureObject)
        // {
        //     ShowDrag();
        // }
    }

    private void OnFindInteractableObject(IInteractable interactable)
    {
        if (interactable as StageLeaveArea)
        {
            ShowLeave();
            return;
        }

        ShowInteract();
    }

    private void OnObjectPicked(DetectableObject detectable, IInteractable interactable)
    {
        Hide();
        ShowPlace();

        if (interactable == null) return;

        ShowInteract();
    }

    private void OnObjectUnpicked()
    {
        Hide();
    }

    private void OnSecureEnabled()
    {
        Hide();
        ShowSecure();
    }

    private void ShowPickup()
    {
        pickup.SetActive(true);
    }

    private void ShowPlace()
    {
        place.SetActive(true);
    }

    private void ShowDrag()
    {
        drag.SetActive(true);
    }

    private void ShowInteract()
    {
        interact.SetActive(true);
    }

    private void ShowSecure()
    {
        secure.SetActive(true);
    }

    private void ShowLeave()
    {
        leave.SetActive(true);
    }

    public void Hide()
    {
        pickup.SetActive(false);
        place.SetActive(false);
        drag.SetActive(false);
        interact.SetActive(false);
        secure.SetActive(false);
        leave.SetActive(false);
    }
}
