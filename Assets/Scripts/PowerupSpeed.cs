using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour
{

    [Header("Powerup Settings")]
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider> ();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        //if we havea valid player and not already powered up
        if (playerShip != null && _poweredUp == false)
        {
            //start powerup timer. restart of its already been started
            StartCoroutine(PowerupSequence(playerShip));
        }
    }
    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        //set bool for detecting lockout
        _poweredUp = true;

        ActivatePowerup(playerShip);
        ///simulate this obj being disabled, we dont want to really disable it, still needed fof script to function
        DisableObject();

        //wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        //reset
        DeactivatePowerUp(playerShip);
        EnableObject();

        //set bool to release lockout
        _poweredUp = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            //powerup player
            playerShip.SetSpeed(_speedIncreaseAmount);
            //Visuals
            playerShip.SetBoosters(true);
        }
    }
    void DeactivatePowerUp(PlayerShip playerShip)
    {
        //revert player powerup . - will substract
        playerShip?.SetSpeed(-_speedIncreaseAmount);
        //visuals
        playerShip?.SetBoosters(false);
    }

    public void DisableObject()
    {
        //disable collider so it cant be retriggered
        _colliderToDeactivate.enabled = false;
        ///disbale visuals to simulate deactivated
        _visualsToDeactivate.SetActive(false);
        //To do: reactivateparticle flash/audio
    }

    public void EnableObject()
    {
        //enable collider so it can be retriggered
        _colliderToDeactivate.enabled = true;
        //enable visuals agian to draw player attention 
        _visualsToDeactivate.SetActive(true);
        //To Do: reactivate particle flash/audio
    }
}
