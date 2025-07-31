using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Vector2 _Movement;
    Rigidbody2D _Rigidbody;


    //idk how to call it but okay, vars
    private PlayerInput playerInput;

    //bools 
    private bool is_sprinting = false;

    //constants
    private float SPEED_MULTIPLIER = 5f;
    private float SPRINTING_SPEED_MULTIPLIER = 10f;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();


        var sprintAction = playerInput.currentActionMap["Sprint"];
        sprintAction.started += ctx => OnSprintStart();
        sprintAction.canceled += ctx => OnSprintStop();
    }



    private void OnMove(InputValue value)
    {
        _Movement = value.Get<Vector2>();
    }

    private void OnSprintStart()
    {
        is_sprinting = true;
    }

    private void OnSprintStop()
    {
        is_sprinting = false;
    }

    private void FixedUpdate()
    {
        _Rigidbody.linearVelocity = is_sprinting ? _Movement * SPRINTING_SPEED_MULTIPLIER : _Movement * SPEED_MULTIPLIER; // sprint or walk
    }
}
