using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager : MonoBehaviour
{
    public static AudioPoolManager instance;

    [Header("Pool Settings")]
    [SerializeField] private AudioSource ambientLoop;
    [SerializeField] private AudioSource bgmLoop;
    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private int initialPoolSize = 20;

    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    private Coroutine volumeLerpCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Duplicate AudioPoolManager found. Destroying new one.");
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateAndPoolAudioSource();
        }
    }

    private AudioSource CreateAndPoolAudioSource()
    {
        AudioSource newSource = Instantiate(audioSourcePrefab, transform);
        
        if (newSource.GetComponent<PooledAudioSource>() == null)
        {
            newSource.gameObject.AddComponent<PooledAudioSource>();
        }

        newSource.gameObject.SetActive(false); 
        audioSourcePool.Enqueue(newSource);
        return newSource;
    }

    private AudioSource GetFromPool()
    {
        if (audioSourcePool.Count > 0)
        {
            AudioSource source = audioSourcePool.Dequeue();
            source.gameObject.SetActive(true);
            return source;
        }
        
        AudioSource newSource = CreateAndPoolAudioSource();
        newSource.gameObject.SetActive(true);
        return newSource;
    }

    public void ReturnToPool(AudioSource source)
    {
        source.clip = null;
        source.volume = 1f;
        source.pitch = 1f;
        source.loop = false;
        
        source.gameObject.SetActive(false);
        audioSourcePool.Enqueue(source);
    }

    public void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f, float pitch = 1.0f)
    {
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }

        AudioSource source = GetFromPool();

        source.transform.position = position;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = false;
        source.spatialBlend = 1.0f; 

        source.Play();
    }

    public void SetAmbientVolume(float volume)
    {
        if (volumeLerpCoroutine != null)
        {
            StopCoroutine(volumeLerpCoroutine);
        }

        volumeLerpCoroutine = StartCoroutine(LerpVolumeCoroutine(volume, 1f));
    }

    private IEnumerator LerpVolumeCoroutine(float targetVolume, float duration)
    {
        var startVolume = ambientLoop.volume;
        var time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            var lerp = Mathf.Clamp01(time / duration);
            ambientLoop.volume = Mathf.Lerp(startVolume, targetVolume, lerp);
            yield return null;
        }

        ambientLoop.volume = targetVolume;
        volumeLerpCoroutine = null;
    }
}
