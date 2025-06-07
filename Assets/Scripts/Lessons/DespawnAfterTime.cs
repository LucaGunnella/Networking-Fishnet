using UnityEngine;
using FishNet.Object;
using System.Collections;

public class DespawnAfterTime : NetworkBehaviour
{
     [SerializeField] private float _secondsBeforeDespawn = 3f;

     public override void OnStartServer()
     {
          StartCoroutine(DespawnAfterSeconds());
     }

     private IEnumerator DespawnAfterSeconds()
     {
          yield return new WaitForSeconds(_secondsBeforeDespawn);

          Despawn(); // NetworkBehaviour shortcut for ServerManager.Despawn(gameObject);
     }
}
