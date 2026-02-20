using System;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class BarrelExplosion : NetworkBehaviour
{
    public readonly SyncVar<Vector3> _explosionSphereTransform = new();

    private void Awake()
    {
        _explosionSphereTransform.OnChange += ChangeSizeSphere;
    }

    private void ChangeSizeSphere(Vector3 prev, Vector3 next, bool asServer)
    {
        transform.localScale = next;
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        StartCoroutine(ExplosionVFX());
    }
    IEnumerator ExplosionVFX()
    {
        Debug.Log("barrel vfx spawned");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("barrel vfx despawned");
        Despawn(gameObject);
    }
}
