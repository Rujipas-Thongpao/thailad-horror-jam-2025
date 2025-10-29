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
        door.EnableCheckForTutorial();
        Subscribe();

        var phone = Instantiate(phonePrefab);
        // Have to Disable input.
        PlayerManager.Instance.ObjectHolder.RegisterObject(phone.GetComponent<DetectableObject>(), null);
        PlayerManager.Instance.ChangeState(E_PlayerState.Holding) ;

        DialogueManager.Instance.PlayDialogue(introSO.Intro);
    }

    private void OnDestroy()
    {
        Unsubscibe();
    }

    public void Subscribe()
    {
        door.EventPlayerSecuredTutorialObject += PlayEndTutorialRoutine;
    }

    public void Unsubscibe()
    {
        door.EventPlayerSecuredTutorialObject -= PlayEndTutorialRoutine;
    }

    private void PlayEndTutorialRoutine()
    {
        Debug.Log("Tutorial Ended");
    }
}
