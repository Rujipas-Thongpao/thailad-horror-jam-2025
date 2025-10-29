using UnityEngine;
using DG.Tweening; // Make sure DOTween is imported and installed in your project

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

    // Animate to open the eye over 'sec' seconds
    public void ToOpenEye(float sec)
    {
        KillTween();
        float current = blinkEye.GetFloat(FloatField);
        blinkTween = DOTween.To(() => current, x =>
        {
            current = x;
            blinkEye.SetFloat(FloatField, x);
            }, 1f, sec);
        }

    // Animate to close the eye over 'sec' seconds
    public void ToCloseEye(float sec)
    {
        KillTween();
        float current = blinkEye.GetFloat(FloatField);
        blinkTween = DOTween.To(() => current, x => {
            current = x;
            blinkEye.SetFloat(FloatField, x);
        }, 0f, sec);
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
