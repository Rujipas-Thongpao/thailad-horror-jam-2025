using UnityEngine;

public class PartnerController : MonoBehaviour
{
    public void ExamineItem(DetectableObject detectableObject)
    {
        if (!TryGetComponent<BaseMark>(out var mark)) return;

        Debug.Log($"This item has a {mark}");
    }
}
