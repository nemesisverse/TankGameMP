using System;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLaunceher : NetworkBehaviour  
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;

    public bool shouldFire;
    public override void OnNetworkSpawn()
    {
        if(!IsOwner) { return; }
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) { return; }
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;

    }

    public void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) { return; }
        if (!shouldFire) {  return; }
        SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
    }

    [ServerRpc]

    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(clientProjectilePrefab, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;
    }

    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
       GameObject projectileInstance =  Instantiate(clientProjectilePrefab  , spawnPos , Quaternion.identity);
        projectileInstance.transform.up = direction;
    }
}
