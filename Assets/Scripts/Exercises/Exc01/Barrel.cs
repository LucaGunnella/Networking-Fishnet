using System;
using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
     private float _explosionRadius;

     private void BarrelExplosion()
     {
          // check objects inside collider, if an object has health component, Debug log
          Collider[] hitColliders = new Collider[10];
          int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, hitColliders);
          for (int i = 0; i < colliderCount; i++)
          {
               Debug.Log(hitColliders[i].gameObject.name);
          }
          Destroy(gameObject);
     }
}
