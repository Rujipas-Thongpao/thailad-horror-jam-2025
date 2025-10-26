using System;
using UnityEngine;

public class NearByChecker : MonoBehaviour
{
    public event Action EventPlayerNearBy;
    public event Action EventPlayerFarBy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DetectableObject>() != null)
            other.GetComponent<DetectableObject>().OnPlayerNearBy();
    }
}
