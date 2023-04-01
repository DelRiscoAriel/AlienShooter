﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;               //The speed that the player will movw at

    Vector3 movement;                      //The vector to store the direction of the player movement
    Animator anim;
    Rigidbody playerRigidbody;

#if !MOBILE_INPUT
    int floorMask;                         //A layer mask so that a ray can be cast just at gameobjets on the floor layer
    float camRayLength = 100f;             //The length os the ray from the camara into the scene
#endif

    void Awake()
    {
#if !MOBILE_INPUT
        //Create a leyet mask for the floor layer
        floorMask = LayerMask.GetMask("Floor");
#endif

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Store Input axis
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        //Move the player around
        Move(h, v);

        //Turn the player to face the mouse
        Turning();

        //Animate the player
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        //Set the movement vector based on the axis input
        movement.Set(h, 0f, v);

        //Normalized the movement vector and make it proportional to the speed per second
        movement = movement.normalized * speed * Time.deltaTime;

        //Move the player to it's current position plus the movement
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
#if !MOBILE_INPUT
        //Create a ray from the mouse cursor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Create a Raycasthit variableto store information about what was hit by the ray
        RaycastHit floorHit;

        //Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Create a vector from the player to the point on the floor the raycast from the mouse hit
            Vector3 playerToMouse = floorHit.point - transform.position;

            //Ensure the vector is entirely along the floor plane
            playerToMouse.y = 0f;

            //Create a quaternion(rotation) based on looking down the vector from the player to the mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Set the player's rotation to this new rotation
            playerRigidbody.MoveRotation(newRotation);
        }
#else

        Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X"), 0f, CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

        if (turnDir != Vector3.zero)
        {
            //Create a vector from the player to the point on the floor the raycast from the mouse hit
            Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

            //Ensure the vecctor is entirely along the floor plane
            playerToMouse.y = 0f;

            //Create a quaternion(rotation) based on looking down the vector from the player to the mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Set the player's rotation to this new rotation
            playerRigidbody.MoveRotation(newRotation);
        }
#endif
    }

    void Animating(float h, float v)
    {
        //Create a boolean that is true if eitherof the input axes is non-zero
        bool walking = h != 0f || v != 0f;

        //Tell the animator whether or not the player is walking
        anim.SetBool("isWalking", walking);
    }
}
