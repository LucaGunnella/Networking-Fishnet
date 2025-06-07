using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _cubePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnCube();
    }

    // We are using a ServerRpc here because the Server needs to do all network object spawning.
    // RPC = Remote Procedure Calls. ServerRpc runs logic on the server.
    [ServerRpc, Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void SpawnCube()
    {
        NetworkObject obj = Instantiate(_cubePrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
        Spawn(obj); // NetworkBehaviour shortcut for ServerManager.Spawn(obj);
    }
}
