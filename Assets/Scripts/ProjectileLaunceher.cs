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
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Collider2D playerCollider;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float muzzleFlashDuration;
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

        PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);
        SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
    }

    [ServerRpc]

    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(clientProjectilePrefab, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;

       
        SpawnDummyProjectileClientRPC(spawnPos , direction);
    }
    [ClientRpc]
    private void SpawnDummyProjectileClientRPC(Vector3 spawnPos, Vector3 direction)
    {
        if (IsOwner) { return; }

        SpawnDummyProjectile(spawnPos , direction);
    }
    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
       GameObject projectileInstance =  Instantiate(clientProjectilePrefab  , spawnPos , Quaternion.identity);
        projectileInstance.transform.up = direction;
    }
}
