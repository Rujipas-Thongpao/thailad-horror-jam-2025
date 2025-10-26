using UnityEngine;

public class UIButtonPrompt : MonoBehaviour
{
    [SerializeField] private GameObject pickup;
    [SerializeField] private GameObject drag;
    [SerializeField] private GameObject interact;

    private CameraDetectObject detect;

    public void Init(CameraDetectObject cameraDetectObject)
    {
        detect = cameraDetectObject;

        detect.EventFindDetectableObject += OnFindDetectableObject;
        detect.EventFindInteractableObject += OnFindInteractableObject;
        detect.EventQuitDetect += Hide;

        Hide();
    }

    public void Dispose()
    {
        detect.EventFindDetectableObject -= OnFindDetectableObject;
        detect.EventFindInteractableObject -= OnFindInteractableObject;
        detect.EventQuitDetect -= Hide;

        detect = null;
    }

    private void OnFindDetectableObject(IDetectable detectable)
    {
        if (detectable as DetectableObject)
        {
            ShowPickup();
            return;
        }

        if (detectable as DraggableFurnitureObject)
        {
            ShowDrag();
        }
    }

    private void OnFindInteractableObject(IInteractable interactable)
    {
        ShowInteract();
    }

    private void ShowPickup()
    {
        pickup.SetActive(true);
    }

    private void ShowDrag()
    {
        drag.SetActive(true);
    }

    private void ShowInteract()
    {
        interact.SetActive(true);
    }

    private void Hide()
    {
        pickup.SetActive(false);
        drag.SetActive(false);
        interact.SetActive(false);
    }
}
