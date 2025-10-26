using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] private UIDialoguePanel dialogue;
    public UIDialoguePanel DialoguePanel => dialogue;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
