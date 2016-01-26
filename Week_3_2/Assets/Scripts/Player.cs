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

    // Jumping

    private bool isJumping = false;

    // Death height

    public float DeathHeight = -5f;

    // The direction the player is facing

    private Vector3 moveDirection = Vector3.zero;

    // The players current location

    private Vector3 currentLocation;

    // The players character controller

    CharacterController characterController;
    NavMeshAgent characterAgent;

    public enum ControlModes
    {
        Keyboard,
        PointAndClick
    }

    public ControlModes ControlMode;

    void Start()
    {
        // Get the mode pref

        string mode = PlayerPrefs.GetString("Mode", "KNM");

        // Use it to set the mode of play

        switch(mode)
        {
            case "PNC":
                ControlMode = ControlModes.PointAndClick;
                break;
            case "KNM":
                ControlMode = ControlModes.Keyboard;
                break;
        }


        // Create a ref to the player objects character controller

        characterController = 
            GetComponent<CharacterController>();

        // Get the navmesh agent for Point and Click

        characterAgent =
            GetComponent<NavMeshAgent>();

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
        Vector3 target = new Vector3();

        // Perform the raycast and populate rayDistance

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            // Draw a line from the rays origin to the point of intersection
            // so that it can be seen in the editor.

            Debug.DrawLine(ray.origin, point, Color.red);

            // Correct for the y transform of the player

            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);

            // Update the target

            target = heightCorrectedPoint;
        }

        // Check which mode we are in and apply movement 
        // polling to reflect

        switch(ControlMode)
        {
            case ControlModes.PointAndClick:
                MovePNC(target);
                break;
            case ControlModes.Keyboard:
                MoveKNM(target);
                break;
        }

        // See if we have walked off the edge

        CheckForDeath();
    }

    public void MovePNC(Vector3 target)
    {
        // Enable the NavMeshAgent but only
        // if we are not jumping

        if(!isJumping)
            characterAgent.enabled = true;

        // Rotate the player to look at the mouse point
        // Un-comment to enable look at.

        // transform.LookAt(target);

        // Check for a left mouse press

        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            // Reset the agents current path
            characterAgent.ResetPath();

            // Give the agent a new destination
            characterAgent.destination = target;
        }

        // Enable jump for PNC mode

        if (Input.GetButton("Jump") && !isJumping)
        {
            // Calculate the current speed of the
            // navmeshagent.

            Vector3 curMove = transform.position - currentLocation;
            float curSpeed = curMove.magnitude / Time.deltaTime;

            // Update the move direction

            moveDirection = transform.TransformDirection(Vector3.forward);

            // Acount for the calculated speed

            moveDirection *= curSpeed;

            // Disable the agent

            characterAgent.Stop();
            characterAgent.enabled = false;

            // Apply the jump

            isJumping = true;
            moveDirection.y = jumpSpeed;
        }

        if (isJumping)
        {
            // Update the move direction to cater for gravity

            moveDirection.y -= gravity * Time.deltaTime;

            // Move the player character

            characterController.Move(moveDirection * Time.deltaTime);

            // Check to see if we have landed and if so
            // re-enable the NavMeshAgent

            if (characterController.isGrounded)
            {
                characterAgent.enabled = true;
                isJumping = false;
            }
        }

        // Update and or archive the current location

        currentLocation = transform.position;
    }

    public void MoveKNM(Vector3 target)
    {
        // Disable the NavMeshAgent

        if (characterAgent.enabled == true)
            characterAgent.enabled = false;

        // Rotate the player to look at the mouse point

        transform.LookAt(target);

        // Check to see if the player is on the ground. If he/she is then
        // poll for input and update the movement direction accordingly.

        if (characterController.isGrounded)
        {
            isJumping = false;

            // Get user input for X and Y

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Set this as the transform direction

            moveDirection = transform.TransformDirection(moveDirection);

            // Update this to cater for speed

            moveDirection *= speed;

            // Check for a jumping input trigger

            if (Input.GetButton("Jump") && !isJumping)
            {
                isJumping = true;
                moveDirection.y = jumpSpeed;
            }
        }

        // Update the move direction to cater for gravity

        moveDirection.y -= gravity * Time.deltaTime;

        // Move the player character

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void CheckForDeath()
    {
        // Check to see if we are lower than the platform/deathHeight

        if (transform.position.y < DeathHeight)
        {
            // If we are reset the player

            transform.position = new Vector3(0, 5, 0);
            moveDirection = Vector3.zero;
        }
    }
}