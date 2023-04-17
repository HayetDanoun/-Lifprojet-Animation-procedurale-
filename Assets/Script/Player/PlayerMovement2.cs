using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity =-9.81f;

    private bool groundedPlayer;
    public float jumpHeight = 2.0f;

    public AudioSource RunSound;
    public AudioSource JumpSound;
    // Update is called once per frame
    void Update()
    {

         groundedPlayer = controller.isGrounded;

        if (Input.GetKey(KeyCode.Space) && groundedPlayer) //jump
        {
            RunSound.Stop();
            JumpSound.Play();
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        //if(groundedPlayer && TimeDeL'AudioFini)
        if(groundedPlayer && RunSound.isPlaying == false){
            JumpSound.Play();
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(RunSound) RunSound.Play ();
        
    }
}
