using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialoguePanel : MonoBehaviour
{
    public event Action EventOracleSpeak;
    public event Action EventGhostSpeak;
    public event Action EventDialogueEnd;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;

    private readonly Queue<string> oracleDialogueQueue = new();
    private float dialogueTimer;
    private bool isDone;

    public void Initialize()
    {
        gameObject.SetActive(true);
        dialogueBox.SetActive(false);
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        dialogueTimer -= Time.deltaTime;

        if (oracleDialogueQueue.Count == 0)
        {
            if (!isDone && dialogueTimer > 0) return;

            dialogueBox.SetActive(false);
            EventDialogueEnd?.Invoke();
            isDone = true;
            return;
        }

        if (dialogueTimer > 0) return;

        var dialogue = oracleDialogueQueue.Dequeue();
        dialogueText.text = dialogue;
        dialogueTimer = 1f + dialogue.Length * 0.03f;
        EventOracleSpeak?.Invoke();
    }

    public void PlayOracleDialogue(List<string> dialogues)
    {
        isDone = false;
        dialogueTimer = 0.3f;
        dialogueBox.SetActive(true);

        foreach (var dialogue in dialogues)
        {
            oracleDialogueQueue.Enqueue(dialogue);
        }
    }

    public void Stop()
    {
        dialogueText.text = "";
        dialogueBox.SetActive(false);
        oracleDialogueQueue.Clear();
        dialogueTimer = 0;
    }
}
