using UnityEngine;

public class ApproveStampHandler : MonoBehaviour
{
    [SerializeField] private GameObject lostStamp;
    [SerializeField] private GameObject securedStamp;

    public void StampLost()
    {
        lostStamp.SetActive(true);
        securedStamp.SetActive(false);
    }

    public void StampSecure()
    {
        lostStamp.SetActive(false);
        securedStamp.SetActive(true);
    }
}
