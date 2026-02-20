using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class AHealthComponent : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    
    public readonly SyncVar<int> Health = new();

    [ServerRpc(RequireOwnership = false)]
    private void Awake()
    {
        Health.Value = _maxHealth;
        
        Health.OnChange += OnHealthChanged;
    }

    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
    }
    
    private void OnHealthChanged(int prev, int next, bool asServer)
    {
        Health.Value = next;
    }
}
