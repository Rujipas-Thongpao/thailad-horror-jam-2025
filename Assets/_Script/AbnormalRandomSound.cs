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
            yield return new WaitForSeconds(Random.Range(10f, 20f));

            //if (ghostSounds.Length == 0) continue;
            AudioPoolManager.instance.PlayClipAtPoint(ghostSounds[Random.Range(0, ghostSounds.Length)], transform.position);
        }
    }
}
