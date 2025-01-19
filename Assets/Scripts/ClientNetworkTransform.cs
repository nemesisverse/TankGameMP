using UnityEngine;
using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Allow only the owner to commit changes to the transform
        CanCommitToTransform = IsOwner;

        // Debug log for verification
        Debug.Log($"Network object spawned. IsOwner: {IsOwner}, CanCommitToTransform: {CanCommitToTransform}");
    }

    private void CustomUpdate()
    {
        // Check if NetworkManager is initialized and active
        if (NetworkManager != null && (NetworkManager.IsConnectedClient || NetworkManager.IsListening))
        {
            if (CanCommitToTransform)
            {
                // Example: Sync transform to the server
                CommitTransformToServer();
            }
        }
        else
        {
            Debug.LogWarning("NetworkManager is not connected or initialized.");
        }
    }

    protected override bool OnIsServerAuthoritative()
    {
        // Specify client-authoritative mode
        return false;
    }

    private void CommitTransformToServer()
    {
        // Example: Add logic for syncing transform changes
        Debug.Log($"Committing transform to server: {transform.position}");
    }
}
