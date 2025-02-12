using UnityEngine;

public class ProjectileLaunceher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    // Update is called once per frame
    void Update()
    {
        
    }
}
