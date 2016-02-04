using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // The players character controller

    CharacterController characterController;
    NavMeshAgent characterAgent;

    public Transform playerTransform;

    // Use this for initialization
    void Start ()
    {
        // Create a ref to the player objects character controller

        characterController =
            GetComponent<CharacterController>();

        // Get the navmesh agent for Point and Click

        characterAgent =
            GetComponent<NavMeshAgent>();

        playerTransform = GameManager.Instance.PlayerObject.transform;

        if (characterAgent.enabled == true)
            characterAgent.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(characterController.isGrounded)
        {
            if (characterAgent.enabled == false)
                characterAgent.enabled = true;

            Vector3 target = playerTransform.position;

            // Reset the agents current path
            characterAgent.ResetPath();

            // Give the agent a new destination
            characterAgent.destination = target;
        }
	}
}
