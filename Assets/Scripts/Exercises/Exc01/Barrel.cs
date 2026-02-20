using System;
using UnityEngine;
using FishNet.Object;

public class Barrel : AHealthComponent
{
      [SerializeField] private float _explosionRadius;
      [SerializeField] private int _explosionDamage = 25;

     protected override void OnHealthChanged(int prev, int next, bool asServer)
     {
          Health.Value = next;
          if (Health.Value <= 0)
               BarrelExplosion();
     }
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
          // instantiate a sphere for explosion
          GameObject explosionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
          explosionSphere.transform.position = transform.position;
          explosionSphere.transform.localScale = new Vector3(_explosionRadius, _explosionRadius, _explosionRadius);
          Destroy(explosionSphere, 0.5f);
          
          Destroy(gameObject);
     }

     private void OnDrawGizmos()
     {
          // draw a circle with radius of explosion
          Gizmos.DrawWireSphere(transform.position, _explosionRadius);
     }
}
