using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // The main camera

    Camera viewCamera;

    // Player properties

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    // Death height

    public float DeathHeight = -5f;

    // The direction the player is facing

    private Vector3 moveDirection = Vector3.zero;

    // The players character controller

    CharacterController characterController;

    void Start()
    {
        // Create a ref to the player objects character controller

        characterController = 
            GetComponent<CharacterController>();

        // Create a ref to the main camera

        viewCamera = Camera.main;
    }

    void Update()
    {
        // Get the current mouse position

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        // This is the output of the raycast

        float rayDistance;

        // Perform the raycast and populate rayDistance

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            // Draw a line from the rays origin to the point of intersection
            // so that it can be seen in the editor.

            Debug.DrawLine(ray.origin, point, Color.red);

            // Correct for the y transform of the player

            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);

            // Rotate the player to look at the mouse point

            transform.LookAt(heightCorrectedPoint);
        }

        // Check to see if the player is on the ground. If he/she is then
        // poll for input and update the movement direction accordingly.

        if (characterController.isGrounded)
        {
            // Get user input for X and Y

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Set this as the transform direction

            moveDirection = transform.TransformDirection(moveDirection);

            // Update this to cater for speed

            moveDirection *= speed;

            // Check for a jumping input trigger

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }       

        // Update the move direction to cater for gravity

        moveDirection.y -= gravity * Time.deltaTime;

        // Move the player character

        characterController.Move(moveDirection * Time.deltaTime);

        // Check we have not walked off the plane

        CheckForDeath();
    }

    public void CheckForDeath()
    {
        if (transform.position.y < DeathHeight)
        {
            transform.position = new Vector3(0, 5, 0);
            moveDirection = Vector3.zero;
        }
    }
}