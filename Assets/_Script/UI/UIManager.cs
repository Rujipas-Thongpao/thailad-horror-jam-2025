using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [SerializeField] private UIDialoguePanel dialogue;
    public UIDialoguePanel DialoguePanel => dialogue;

    [SerializeField] private UIButtonPrompt buttonPrompt;
    public UIButtonPrompt ButtonPrompt => buttonPrompt;

    [SerializeField] private UIResultPanel result;
    public UIResultPanel ResultPanel => result;

    [SerializeField] private UIWeeklyPanel weeklyPanel;
    public UIWeeklyPanel WeeklyPanel => weeklyPanel;

    [SerializeField] private BlinkEyeController blinkEyeController;
    public BlinkEyeController BlinkEyeController => blinkEyeController;


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
