using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    private PlayerManager player;
    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindFirstObjectByType<PlayerManager>();
        }

        if (player == null) return;

        var angle = Vector3.Dot(-player.PlayerCam.camHolder.transform.forward, this.transform.forward);
        if(angle > .8f)
        {
            player.SanityController.ModifySanity(-10f * Time.deltaTime);
        }
    }
}
