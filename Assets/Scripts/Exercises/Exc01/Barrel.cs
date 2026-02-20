using System.Collections;
using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private int _damage = 5;

    private Renderer _renderer;
    private bool _isDestroying = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnHit()
    {
        if (_isDestroying) return;
        
        if (IsServerStarted) ServerProcessHit();
        else ServerOnHit();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerOnHit() => ServerProcessHit();

    private void ServerProcessHit()
    {
        if (_isDestroying) return;
        _isDestroying = true;

        RpcChangeColor(_color);
        StartCoroutine(OnDestroyingRoutine());
    }

    private IEnumerator OnDestroyingRoutine()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        
        Explode();

        ServerManager.Despawn(gameObject);
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent(out HealthComponent health))
            {
                health.TakeDamage(_damage);
            }
        }
    }

    [ObserversRpc]
    private void RpcChangeColor(Color newColor)
    {
        if (_renderer != null) _renderer.material.color = newColor;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}