using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    [SerializeField] private NetworkObject _cubePrefab;
    
    
    public void Explode()
    {
        NetworkObject obj = Instantiate(_cubePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        
        if(obj.TryGetComponent<SyncMaterialColor>(out var syncMaterialColor)) // lesson 02
        {
            syncMaterialColor.color.Value = Random.ColorHSV();
        }
        
        Spawn(obj, Owner);
        Despawn();
    }
    
    
}