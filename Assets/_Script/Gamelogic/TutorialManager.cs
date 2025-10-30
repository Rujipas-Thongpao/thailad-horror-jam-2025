using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public event Action EventTutorialEnd;
    private GameplayController gameplayController;
    [SerializeField] private OracleTriggerAreaTutorial door;
    [SerializeField] private GameObject phonePrefab;
    [SerializeField] private StageDialogueSO introSO;

    List<string> endingDialouge = new()
    {
        "good job, lets go to the site."
    };

    public void Init(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
        door.Init();
        Subscribe();

        var phone = Instantiate(phonePrefab);
        PlayerManager.Instance.ObjectHolder.RegisterObject(phone.GetComponent<DetectableObject>(), null);
        PlayerManager.Instance.ChangeState(E_PlayerState.Holding);

        DialogueManager.Instance.StartIntroTutorialDialogue(introSO);
    }
    public void Dispose()
    {

        var objHolder = PlayerManager.Instance.ObjectHolder;
        var currentHolingObject = objHolder.Holdingobject;
        objHolder.DisableSecureObject();

        if (currentHolingObject != null)
        {
            objHolder.UnregisterObject();
            Destroy(currentHolingObject.gameObject);
        }

        PlayerManager.Instance.ChangeState(E_PlayerState.Normal);
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
        StartCoroutine(EndTutorialRoutine());
    }

    IEnumerator EndTutorialRoutine()
    {
        DialogueManager.Instance.PlayDialogue(endingDialouge);

        yield return new WaitForSeconds(5f);

        gameplayController.UI.BlinkEyeController.ToCloseEye(2);
        EventTutorialEnd?.Invoke();
    }

    private void OnIntroEnd()
    {
        EnableDoor();
    }
}
