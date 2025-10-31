using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    private PlayerManager player;
    [SerializeField] private Transform forward;

    private void Start()
    {
        player = PlayerManager.Instance;
    }

    private void Update()
    {
        if (!player) return;

        var angle = Vector3.Dot(player.PlayerCam.camHolder.transform.forward.normalized, (transform.position - player.transform.position).normalized);

        if (angle <= .7f) return;

        player.SanityController.DrainSanity(-30f * Time.deltaTime);
    }
}
