using Unity.Netcode;
using UnityEngine;

public class ConnectionButton : MonoBehaviour
{

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

}
