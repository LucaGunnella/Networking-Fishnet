using System.Collections;
using UnityEngine;
using FishNet.Object;

public class Barrel : AHealthComponent
{
      [SerializeField] private float _explosionRadius;
      [SerializeField] private int _explosionDamage = 25;
      [SerializeField] private NetworkObject _explosionSpherePrefab;

     protected override void OnHealthChanged(int prev, int next, bool asServer)
     {
          Health.Value = next;
          if (Health.Value <= 0)
               BarrelExplosion();
     }
     [ServerRpc(RequireOwnership = false)]
     private void BarrelExplosion()
     {
          // check objects inside collider, if an object has health component, Debug log
          Collider[] hitColliders = new Collider[100];
          int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, hitColliders);
          for (int i = 0; i < colliderCount; i++)
          {
               if (hitColliders[i].TryGetComponent(out AHealthComponent health))
               {
                    // skip itself
                    if (health == this) continue;
                    health.TakeDamage(_explosionDamage);
                    Debug.Log($"Damage taken from {health.gameObject.name}, now has {health.Health.Value} health.");
               }
          }
          // instantiate a sphere in server for explosion vfx
          NetworkObject obj = Instantiate(_explosionSpherePrefab, transform.position, Quaternion.identity);
          Spawn(obj);
          var BarrelExplosion = obj.GetComponent<BarrelExplosion>();
          BarrelExplosion._explosionSphereTransform.Value = new Vector3(_explosionRadius, _explosionRadius, _explosionRadius);
          Despawn(gameObject);
     }
     
     private void OnDrawGizmos()
     {
          // draw a circle with radius of explosion
          Gizmos.DrawWireSphere(transform.position, _explosionRadius);
     }
}
