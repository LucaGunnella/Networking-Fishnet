using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class RPC_Lesson : NetworkBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RpcSendChat("Hello world!");        
    }

    [ServerRpc(Logging = LoggingType.Off, RequireOwnership = true)]
    private void RpcSendChat(string msg)
    {
        Debug.Log($"Client {Owner.ClientId} sent '{msg}' on the server.");
    }
}
