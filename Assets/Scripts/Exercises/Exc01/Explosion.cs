using UnityEngine;
using FishNet.Object;
using System;

public class Explosion : NetworkBehaviour
{
    [SerializeField] private int _damage;

    // [ServerRpc(RequireOwnership = false)]
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.TakeDamage(_damage);
        }
    }
}