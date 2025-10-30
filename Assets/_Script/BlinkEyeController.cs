using DG.Tweening;
using System; // Make sure DOTween is imported and installed in your project
using UnityEngine;

public class BlinkEyeController : MonoBehaviour
{
    [SerializeField] private Material blinkEye;

    private const string FloatField = "_Float";
    private Tween blinkTween;

    // Instantly open the eye (set _Float to 1)
    public void OpenEyeInstance()
    {
        KillTween();
        blinkEye.SetFloat(FloatField, 1f);
    }

    // Instantly close the eye (set _Float to 0)
    public void CloseEyeInstance()
    {
        Debug.Log("CloseEyeInstance called");
        KillTween();
        blinkEye.SetFloat(FloatField, 0f);
    }

    public void ToOpenEye(float sec)
    {
        ToOpenEye(sec, null);
    }
    // Animate to open the eye over 'sec' seconds
    public void ToOpenEye(float sec, Action OnFinish)
    {
        KillTween();
        float current = blinkEye.GetFloat(FloatField);
        blinkTween = DOTween.To(() => current, x =>
            {
                current = x;
                blinkEye.SetFloat(FloatField, x);
            },
            1f,
            sec
        ).OnComplete(() => OnFinish());
    }

    public void ToCloseEye(float sec)
    {
        ToCloseEye(sec, null);
    }
    // Animate to close the eye over 'sec' seconds
    public void ToCloseEye(float sec, Action OnFinish)
    {
        KillTween();
        float current = blinkEye.GetFloat(FloatField);
        blinkTween = DOTween.To(() => current, x =>
        {
            current = x;
            blinkEye.SetFloat(FloatField, x);
        }, 0f, sec)
        .OnComplete(() => OnFinish());
    }

    // Helper to kill any running tween
    private void KillTween()
    {
        if (blinkTween != null && blinkTween.IsActive())
        {
            blinkTween.Kill();
            blinkTween = null;
        }
    }
}
