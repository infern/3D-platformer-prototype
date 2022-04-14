using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLift : MonoBehaviour
{
    [SerializeField] LiftScript platform;


    private void OnTriggerEnter(Collider other)
    {
        PlayerComponents player = other.gameObject.GetComponent<PlayerComponents>();
        if (player != null && platform.LiftTop()) platform.Activate();
    }

}
