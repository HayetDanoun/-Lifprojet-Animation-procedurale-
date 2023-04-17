using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float speed = 40f;

    [Header("Drag")]
    [SerializeField] private float drag = 6f;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    //public CharacterController controller;

    private void Start() => rb.freezeRotation = true;

    private void Update()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = (orientation.forward * horizontalInput - orientation.right * verticalInput).normalized;
        rb.drag = drag;


    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed, ForceMode.Acceleration);
    }
}