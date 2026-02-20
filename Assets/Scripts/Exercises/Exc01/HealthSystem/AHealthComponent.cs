using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class AHealthComponent : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    
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
    
    protected virtual void OnHealthChanged(int prev, int next, bool asServer)
    {
        Health.Value = next;
    }
}
