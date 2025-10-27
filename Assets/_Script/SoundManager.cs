using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource soundPlayer;

    public static AudioSource audioSource;

    void Awake()
    {
        audioSource = soundPlayer;
    }

    public static void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
