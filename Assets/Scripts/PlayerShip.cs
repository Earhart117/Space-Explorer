using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    public ParticleSystem thrusters;
    public bool includeChildren = true;
   

    Rigidbody _rb = null;

    private void Update()
    {

        if (!Input.GetButton("Vertical"))
        {
            thrusters.Play(includeChildren);
        }
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }
    
    //use force to build moment forward/backward
    void MoveShip()
    {
        // s/down =-1, w/up =1, none=0. scale by movespeed
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        //combine direction with calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        //apply the movement to the physics object
        _rb.AddForce(moveDirection);

    }
    void TurnShip()
    {
        // a/left= -1, d/right = 1, none= 0. scaled by turnspeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        //specify an axis to apply our turn amount as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        //spin rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }
    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }
    
}