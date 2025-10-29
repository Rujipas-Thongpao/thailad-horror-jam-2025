using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialoguePanel : MonoBehaviour
{
    public event Action EventDialogueEnd;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;

    private readonly Queue<string> dialogueQueue = new();
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

        if (dialogueQueue.Count == 0)
        {
            if (!isDone && dialogueTimer > 0) return;

            dialogueBox.SetActive(false);
            EventDialogueEnd?.Invoke();
            isDone = true;
            return;
        }

        if (dialogueTimer > 0) return;

        var dialogue = dialogueQueue.Dequeue();
        dialogueText.text = dialogue;
        dialogueTimer = 1f + dialogue.Length * 0.03f;
    }

    public void Play(List<string> dialogues)
    {
        isDone = false;
        dialogueTimer = 0.3f;
        dialogueBox.SetActive(true);

        foreach (var dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }
    }

    public void Play(string dialogue)
    {
        Play(new List<string> { dialogue });
    }

    public void Stall(string dialogue)
    {
        Stop();

        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void Stop()
    {
        dialogueText.text = "";
        dialogueBox.SetActive(false);
        dialogueQueue.Clear();
        dialogueTimer = 0;
    }
}
