using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private NetworkObject _cubePrefab;
    [SerializeField] private Animator _animator;

    private MeshRenderer _meshRenderer;

    void Awake()
    {
        if(gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer))
        
            _meshRenderer = meshRenderer;
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void Explode()
    {
        if(_meshRenderer != null)
            _meshRenderer.enabled = false;

        if(_animator != null)
            _animator.SetTrigger("Explode");
    }

    // [ServerRpc(RequireOwnership = true)]
    public void DeInit()
    {
        Despawn();
    }
    
}