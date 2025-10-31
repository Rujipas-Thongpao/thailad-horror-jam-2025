using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeeklyPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] UIWeeklyNode[] nodes;
    [SerializeField] private ApproveStampHandler stamp;
    [SerializeField] private AudioClip stampSfx;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject thanksText;

    public void Init(List<PerformanceStatsData> perfs)
    {
        closeButton.onClick.AddListener(OnStamp);

        var m = Mathf.Min(perfs.Count, nodes.Length);
        for (int i = 0; i < m; i++)
        {
            nodes[i].Init(perfs[i]);
        }

        if(nodes.Length > perfs.Count)
        {
            for (int i = perfs.Count; i < nodes.Length; i++)
            {
                nodes[i].Dispose();
            }
        }

        ToggleVisible(true);
        thanksText.SetActive(false);
    }

    private void ToggleVisible(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.blocksRaycasts = visible;
    }

    public void Dispose()
    {
        foreach (var node in nodes)
        {
            if (node != null)
            {
                node.Dispose();
            }
        }
    }
    private void OnStamp()
    {
        stamp.StampSecure();
        AudioPoolManager.instance.PlayClipAtPoint(stampSfx, this.transform.position);
        closeButton.gameObject.SetActive(false);

        Invoke("invokeEndLevel", 1f);
    }
    private void invokeEndLevel()
    {
        thanksText.SetActive(true);
    }
}
