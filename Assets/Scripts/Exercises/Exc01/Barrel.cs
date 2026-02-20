using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

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