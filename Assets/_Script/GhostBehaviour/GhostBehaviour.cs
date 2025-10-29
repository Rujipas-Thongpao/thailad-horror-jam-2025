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

        var angle = Vector3.Dot(player.PlayerCam.camHolder.transform.forward.normalized, (forward.position - player.transform.position).normalized);
        Debug.Log(angle);

        if (angle <= .8f) return;

        player.SanityController.DrainSanity(-24f * Time.deltaTime);
    }
}
