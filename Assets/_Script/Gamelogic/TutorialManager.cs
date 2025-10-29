using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public event Action EventTutorialEnd;
    private GameplayController gameplayController;
    [SerializeField] private OracleTriggerAreaTutorial door;
    [SerializeField] private GameObject phonePrefab;

    public void Init(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
        door.Init();
        door.EnableCheckArea();
        Subscribe();

        var phone = Instantiate(phonePrefab);
        PlayerManager.Instance.ObjectHolder.RegisterObject(phone.GetComponent<DetectableObject>(), null);
        PlayerManager.Instance.ChangeState(E_PlayerState.Holding) ;
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
