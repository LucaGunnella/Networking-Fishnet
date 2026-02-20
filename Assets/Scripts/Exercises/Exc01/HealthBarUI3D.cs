using System;
using FishNet.Component.Animating;
using FishNet.Managing.Logging;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthComponent3D : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Slider _slider;
    public float HealthPercentage
    {
        get
        {
            float percentage = ((float) Health.Value)/_maxHealth;
            return Mathf.Max(Mathf.Min(percentage, 1), 0);
        }
    }

    public readonly SyncVar<int> Health = new();
    // Synchronize animator
    private NetworkAnimator _networkAnimator;

    #region Unity Callbacks
    private void Awake()
    {
        Health.Value = _maxHealth;

        _networkAnimator = GetComponentInChildren<NetworkAnimator>();
        Health.OnChange += OnHealthChanged;
    }

    
    public override void OnStartClient()
    {
        base.OnStartClient();
        DisableHealthBar();
    }


    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void DisableHealthBar()
    {
        _slider.fillRect.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
    }
    #endregion
    
    [ServerRpc(RequireOwnership = false)]
    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
    }

    private void ReceiveHit()
    {
        if(!_networkAnimator) return;
        
        _networkAnimator.SetTrigger("HasBeenHit");
    }

    private void OnHealthChanged(int prev, int next, bool asServer)
    {
        _slider.value = HealthPercentage;
        ReceiveHit();
    }
}