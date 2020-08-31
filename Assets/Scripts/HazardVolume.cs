using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //detect if it is player
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        //if valid, continue
        if (playerShip != null)
        {
            //kill
            playerShip.Kill();
        }
    }
}
