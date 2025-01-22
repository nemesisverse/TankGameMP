// Importing the Unity.Netcode namespace for network-related functionality
using Unity.Netcode;

// Importing the UnityEngine namespace for Unity-specific features
using UnityEngine;

// Define the PlayerMovement class, which handles player movement and is network-aware
public class PlayerMovement : NetworkBehaviour
{
    // Reference to an InputReader component, used to read player input
    [SerializeField] private InputReader inputReader;

    // Reference to the Transform component representing the player's body
    [SerializeField] private Transform bodyTransform;

    // Reference to the Rigidbody2D component used for physics-based movement
    [SerializeField] private Rigidbody2D rb;

    // Movement speed of the player (modifiable in the Unity Editor)
    [SerializeField] private float movementSpeed = 4f;

    // Turning rate of the player (degrees per second, editable in Unity Editor)
    [SerializeField] private float turningRate = 270f;

    // Stores the most recent movement input from the player
    private Vector2 previousMovementInput;

    // Called when the object is spawned on the network
    public override void OnNetworkSpawn()
    {
        // Check if this instance is owned by the local player
        if (!IsOwner) { return; }

        // Subscribe to the MoveEvent from the InputReader to handle player input
        inputReader.MoveEvent += HandleMove;
    }

    // Called when the object is removed from the network
    public override void OnNetworkDespawn()
    {
        // Check if this instance is owned by the local player
        if (!IsOwner) { return; }

        // Unsubscribe from the MoveEvent to prevent memory leaks
        inputReader.MoveEvent -= HandleMove;
    }

    // Called once per frame to handle updates like rotation
    void Update()
    {
        // Ensure only the owner of the object executes the code
        if (!IsOwner) { return; }

        // Calculate the rotation angle based on horizontal input
        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;

        // Rotate the player's body around the Z-axis
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    // Called at a fixed interval (physics updates) to handle movement
    private void FixedUpdate()
    {
        // Ensure only the owner of the object executes the code
        if (!IsOwner) { return; }

        // Set the velocity of the Rigidbody2D to move the player forward/backward
        rb.linearVelocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    // Handles movement input from the player
    private void HandleMove(Vector2 movementInput)
    {
        // Store the received input for use in Update and FixedUpdate
        previousMovementInput = movementInput;
    }
}
