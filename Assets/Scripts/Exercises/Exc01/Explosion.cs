using UnityEngine;
using FishNet.Object;
using System;

public class Explosion : NetworkBehaviour
{
    [SerializeField] private int _damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.TakeDamage(_damage);
        }
    }
}