using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public LayerMask groundLayer;
    public bool isGround;
    public float gravity = -30f;
    public float jumpHeight = 2f;
    private Vector3 velocity;

    public float sens = 200f;
    public float mouseY;
    private float mouseX;
    public float minClamp = -90f, maxClamp = 90f;
    public Transform cameraHolder;

    private void Update()
    {
        Movement();
        LockRotation();
    }

    private void LockRotation()
    {
        mouseY -= Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, minClamp, maxClamp);

        mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;

        cameraHolder.localRotation = Quaternion.Euler(Vector3.right * mouseY);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void Movement()
    {
        isGround = Physics.CheckSphere(transform.position, 0.25f, groundLayer);

        if (isGround && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        move = move.normalized * moveSpeed * Time.deltaTime;

        cc.Move(move);

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        cc.Move(velocity * Time.deltaTime);
    }
}


