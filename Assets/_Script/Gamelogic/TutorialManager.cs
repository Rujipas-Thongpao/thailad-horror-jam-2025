using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public event Action EventTutorialEnd;
    private GameplayController gameplayController;
    [SerializeField] private OracleTriggerAreaTutorial door;
    [SerializeField] private GameObject phonePrefab;
    [SerializeField] private StageDialogueSO introSO;

    public void Init(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
        door.Init();
        Subscribe();

        var phone = Instantiate(phonePrefab);
        PlayerManager.Instance.ObjectHolder.RegisterObject(phone.GetComponent<DetectableObject>(), null);
        PlayerManager.Instance.ChangeState(E_PlayerState.Holding) ;

        DialogueManager.Instance.StartIntroTutorialDialogue(introSO);
    }

    [ContextMenu("Enable Door")]
    private void EnableDoor()
    {
        door.EnableCheckArea();
    }

    private void OnDestroy()
    {
        Unsubscibe();
    }

    public void Subscribe()
    {
        door.EventPlayerSecuredTutorialObject += PlayEndTutorialRoutine;
        DialogueManager.Instance.EventIntroEnd += OnIntroEnd;
    }

    public void Unsubscibe()
    {
        door.EventPlayerSecuredTutorialObject -= PlayEndTutorialRoutine;
        DialogueManager.Instance.EventIntroEnd -= OnIntroEnd;
    }

    private void PlayEndTutorialRoutine()
    {
        Debug.Log("Tutorial Ended");
    }

    private void OnIntroEnd()
    {
        EnableDoor();
    }
}
