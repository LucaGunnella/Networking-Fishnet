using System;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComponent : AHealthComponent
{
    [SerializeField] private Slider _healthSlider;
    private NetworkObject _networkObject;

    protected override void Awake()
    {
        base.Awake();
        _healthSlider.maxValue = Health.Value;
        _healthSlider.value = Health.Value;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetHealth();
        }
    }

    [ServerRpc(RequireOwnership = true)]
    private void ResetHealth()
    { 
        Health.Value = _maxHealth;
    }
    
    protected override void OnHealthChanged(int prev, int next, bool asServer)
    {
        _healthSlider.value = Health.Value;
    }
}
