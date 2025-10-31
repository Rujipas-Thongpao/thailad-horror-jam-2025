using UnityEngine;

public class RadioObject : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip switchSound;
    bool isPlaying;

    private void Start()
    {
        isPlaying = Random.Range(0f, 1f) > 0.5f;
    }
    public void OnInteracted()
    {
        isPlaying = !isPlaying;

        AudioPoolManager.instance.PlayClipAtPoint(switchSound, transform.position);

        if (audioSource != null)
        {
            audioSource.enabled = isPlaying;
        }
    }
}
