using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    private PlayerManager player;

    private void Start()
    {
        player = PlayerManager.Instance;
    }

    private void Update()
    {
        if (!player) return;

        var angle = Vector3.Dot(-player.PlayerCam.camHolder.transform.forward, transform.forward);

        if (angle <= .8f) return;

        player.SanityController.DrainSanity(-10f * Time.deltaTime);
    }
}
