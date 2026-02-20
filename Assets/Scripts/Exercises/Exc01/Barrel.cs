using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

    [ServerRpc(RequireOwnership = false, RunLocally = true)]
    public void Explode()
    {
        if(_animator != null)
            _animator.SetTrigger("Explode");
    }

    // [ServerRpc(RequireOwnership = false)]
    public void DeInit()
    {
        Despawn(DespawnType.Destroy);
    }
    
}