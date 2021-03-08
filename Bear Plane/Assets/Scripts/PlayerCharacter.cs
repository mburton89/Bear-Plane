﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    void Update()
    {
        if (!PlayerSwap.isPlane)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                _crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                _crouch = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerSwap.isPlane)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
            _jump = false;
        }
    }

    //public void HandlePhoneInput(Vector2 initialTouchPosition, Vector2 currentTouchPosition)
    //{
    //    Vector2 directionRelativeToPhoneMiddle = currentTouchPosition - initialTouchPosition;
    //    controller.Move(directionRelativeToPhoneMiddle.x * Time.fixedDeltaTime, _crouch, _jump);
    //}

    public void HandleJoystickInput(Vector2 joystickDirection)
    {
        controller.Move((joystickDirection.x * runSpeed) * Time.fixedDeltaTime, _crouch, _jump);
    }

    public void HandleJumpPressed()
    {
        _jump = true;
    }
}