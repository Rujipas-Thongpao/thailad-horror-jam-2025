using System.Collections;
using UnityEngine;

public class AbnormalRandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] ghostSounds;

    public void StartRandomSound()
    {
        StartCoroutine(RandomSound());
    }
    IEnumerator RandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(20f, 30f));

            //if (ghostSounds.Length == 0) continue;
            AudioPoolManager.instance.PlayClipAtPoint(ghostSounds[Random.Range(0, ghostSounds.Length)], transform.position);
        }
    }
}
