using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Dizzyness : PlayerEffect
{
    [SerializeField] private Volume volume;
    [SerializeField] private Camera camera;

    public override void PlayEffect(float intensity)
    {
        if(volume == null || camera == null)
        {
            Debug.LogError("Volume or Camera is not assigned in Dizzyness effect.");
            return;
        }

        int intensityLevel = Mathf.Clamp(Mathf.RoundToInt(intensity), 0, 5);

        if (intensity >= 0)
        {
            volume.profile.TryGet<Vignette>(out var vignette);
            vignette.active = true;
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.5f, 1f);
            //vignette.intensity.value = .5f;
        }
        if (intensity >= 1)
        {
            volume.profile.TryGet<DepthOfField>(out var dof);
            dof.active = true;
            dof.mode.value = DepthOfFieldMode.Bokeh;
            DOTween.To(() => dof.focalLength.value, x => dof.focalLength.value = x, 100f, 1f);
            dof.focalLength.value = 100f;

            //camera.fieldOfView = 15f;
            DOTween.To(() => camera.fieldOfView, x => camera.fieldOfView = x, 40f, .5f);
        }
    }



    public override void StopEffect()
    {
        volume.profile.TryGet<Vignette>(out var vignette);
        //vignette.intensity.value = 0f;
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.0f, 1f).OnComplete(() =>
        {
            vignette.active = false;
        });

        volume.profile.TryGet<DepthOfField>(out var dof);
        dof.mode.value = DepthOfFieldMode.Bokeh;
        DOTween.To(() => dof.focalLength.value, x => dof.focalLength.value = x, 0.0f, 1f).OnComplete(()=> {
            dof.active = false;
        });

        //camera.fieldOfView = 60f;
        DOTween.To(() => camera.fieldOfView, x => camera.fieldOfView = x, 60f, 1f);
    }
}
