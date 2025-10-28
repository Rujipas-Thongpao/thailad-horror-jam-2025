using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _wasPlaying = false;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        _wasPlaying = false; 
    }

    void Update()
    {
        if (_audioSource.isPlaying)
        {
            _wasPlaying = true;
            return;
        }

        if (_wasPlaying)
        {
            _wasPlaying = false; 
            
            if (AudioPoolManager.instance != null)
            {
                AudioPoolManager.instance.ReturnToPool(_audioSource);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
