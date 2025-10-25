using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField]
    private Dizzyness dizzyness;

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

    async UniTask<string> DizzyRoutine(float intensity)
    {
        dizzyness.PlayEffect(intensity);
        await UniTask.WaitForSeconds(2);
        dizzyness.StopEffect();
        return "Done";
    }
}
