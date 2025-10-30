using UnityEngine;
using System.Collections.Generic;

public class PropSound : MonoBehaviour
{
    [Header("Sound Settings")]
    [Tooltip("The sound to play on collision.")]
    public AudioClip hitSound;

    [Tooltip("The minimum collision speed (magnitude) required to trigger the sound.")]
    [SerializeField] private float minCollisionVelocity = 1.0f;

    [Header("Sound Properties")]
    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.8f;

    [Range(0.8f, 1.2f)]
    [SerializeField] private float minPitch = 0.9f;

    [Range(0.8f, 1.2f)]
    [SerializeField] private float maxPitch = 1.1f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < minCollisionVelocity)
        {
            return; 
        }

        if (hitSound == null)
        {
            Debug.LogWarning($"PropSound on {gameObject.name} has no 'hitSound' assigned.");
            return;
        }

        Vector3 impactPoint = collision.contacts[0].point;

        float randomPitch = Random.Range(minPitch, maxPitch);

        if (AudioPoolManager.instance != null)
        {
            AudioPoolManager.instance.PlayClipAtPoint(hitSound, impactPoint, volume, randomPitch);
        }
        else
        {
            Debug.LogWarning("AudioPoolManager instance not found. Playing clip at point as fallback.");
            AudioSource.PlayClipAtPoint(hitSound, impactPoint, volume);
        }
    }
}