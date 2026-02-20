using System;
using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
     public void OnHit()
     {
          Debug.Log("Sono stato colpito");
          Destroy(gameObject);
     }
}
