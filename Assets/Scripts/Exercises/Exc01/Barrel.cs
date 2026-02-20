using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private NetworkObject _cubePrefab;
    [SerializeField] private Animator _animator;

    void Awake()
    {
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void Explode()
    {

        if(_animator != null)
            _animator.SetTrigger("Explode");
    }

    // [ServerRpc(RequireOwnership = true)]
    public void DeInit()
    {
        Despawn();
    }
    
}