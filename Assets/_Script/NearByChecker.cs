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
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //        Debug.Log("Player Far By");
    //        EventPlayerFarBy?.Invoke();
    //}
}
