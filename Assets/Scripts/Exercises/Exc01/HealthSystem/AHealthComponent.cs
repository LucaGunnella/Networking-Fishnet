using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public abstract class AHealthComponent : NetworkBehaviour
{
    [SerializeField] protected int _maxHealth = 100;
    
    public readonly SyncVar<int> Health = new();
    
    protected virtual void Awake()
    {
        Health.Value = _maxHealth;
        
        Health.OnChange += OnHealthChanged;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
    }

    protected abstract void OnHealthChanged(int prev, int next, bool asServer);
}
