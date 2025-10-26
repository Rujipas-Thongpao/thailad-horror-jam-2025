using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField]
    private Dizzyness dizzyness;

    [SerializeField]
    private HandTremors handTremors;

    public static PlayerEffectController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    async public void Dizzyness(float intensity)
    {
        await DizzyRoutine(intensity);
    }

    public async Task HandTremor(float intensity)
    {
        await HandTremorsRoutine(intensity);
    }

    async UniTask<string> DizzyRoutine(float intensity)
    {
        dizzyness.PlayEffect(intensity);
        await UniTask.WaitForSeconds(2);
        dizzyness.StopEffect();
        return "Done";
    }

    public async Task<UniTask<string>> HandTremorsRoutine(float intensity)
    {
        handTremors.PlayEffect(intensity);
        await UniTask.WaitForSeconds(5);
        handTremors.StopEffect();
        return UniTask.FromResult("Done");
    }
}
