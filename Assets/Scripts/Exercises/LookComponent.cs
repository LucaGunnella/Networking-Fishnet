using FishNet.Connection;
using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class LookComponent : NetworkBehaviour
{
    [SerializeField] private Transform _cameraSocket;
    [SerializeField] private GameObject _cameraPrefab;

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        CreateCamera();
    }

    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void CreateCamera()
    {
        Instantiate(_cameraPrefab, _cameraSocket);
    }
}