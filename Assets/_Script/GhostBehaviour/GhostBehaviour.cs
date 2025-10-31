using System.Collections;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    private PlayerManager player;
    [SerializeField] private Transform forward;

    [SerializeField] private AudioClip[] ghostSounds;

    private void Start()
    {
        player = PlayerManager.Instance;
        StartCoroutine(RandomSound());
    }

    private void Update()
    {
        if (!player) return;

        var angle = Vector3.Dot(player.PlayerCam.camHolder.transform.forward.normalized, (transform.position - player.transform.position).normalized);

        if (angle <= .7f) return;

        player.SanityController.DrainSanity(-30f * Time.deltaTime);

        Destroy(this.gameObject, 20f);
    }

    IEnumerator RandomSound()
    {
        while (true)
        {

            if (ghostSounds.Length == 0) continue;
            AudioPoolManager.instance.PlayClipAtPoint(ghostSounds[Random.Range(0, ghostSounds.Length)], transform.position);

            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }
}
