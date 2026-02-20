using System.Collections;
using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
     [SerializeField] private float _timeToDestroy = 1f;
     [SerializeField] private Color _color = Color.red;
     
     private Renderer _renderer;
     private bool _isDestroying = false;
     
     private Coroutine _onDestroy;
     
     private void Awake()
     {
          _renderer = GetComponent<Renderer>();
     }
     
     public void OnHit()
     {
          if (_isDestroying) return;
          
          Debug.Log("Sono stato colpito");
          
          if (IsServerStarted)
          {
               _isDestroying = true;
               RpcChangeColor(_color);
               StartCoroutine(OnDestroyingRoutine());
          }
          else
          {
               ServerOnHit();
          }
     }
     
     [ServerRpc(RequireOwnership = false)]
     private void ServerOnHit()
     {
          _isDestroying = true;
          RpcChangeColor(_color);
          StartCoroutine(OnDestroyingRoutine());
     }

     private IEnumerator OnDestroyingRoutine()
     {
          yield return new WaitForSeconds(_timeToDestroy);

          ServerManager.Despawn(gameObject);

          yield return null;
     }

     [ObserversRpc]
     private void RpcChangeColor(Color newColor)
     {
          if (_renderer != null)
          {
               _renderer.material.color = newColor;
          }
     }
}