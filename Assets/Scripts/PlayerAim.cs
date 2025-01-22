// Importing the Unity.Netcode namespace, which provides networking functionality for multiplayer games.
using Unity.Netcode;

// Importing the UnityEngine namespace, which provides core Unity game development tools.
using UnityEngine;

// Defines a class named PlayerAim that extends NetworkBehaviour, making it suitable for networked gameplay.
public class PlayerAim : NetworkBehaviour
{
    // Private serialized field to hold a reference to the InputReader script,
    // which will likely manage input from the player (e.g., aim position).
    [SerializeField] private InputReader inputReader;

    // Private serialized field to hold a reference to the turret's Transform,
    // allowing us to manipulate its orientation in the game world.
    [SerializeField] private Transform turretTransform;

    // The LateUpdate method is called after all Update methods have been processed.
    // It is used here to ensure that the tank's movement updates are completed
    // before the turret's aiming logic is executed.



    private void LateUpdate()
    {
        // If this is not the owner's instance (in a networked multiplayer game), exit early.
        if (!IsOwner) { return; }


        if (Application.isFocused)
        {
            // Retrieve the player's aim position on the screen as a 2D vector.
            Vector2 aimScreenPosition = inputReader.AimPosition;

            // Convert the 2D screen position to a world position using the main camera.
            Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScreenPosition);




            // Calculate the direction vector from the turret's position to the aim world position
            // and set it as the 'up' direction of the turretTransform, effectively aiming it.
            turretTransform.up = new Vector2(
                aimWorldPosition.x - turretTransform.position.x, // Difference in x-coordinates
                aimWorldPosition.y - turretTransform.position.y  // Difference in y-coordinates
            );


        }
    }
}
