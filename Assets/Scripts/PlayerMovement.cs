using FishNet.Managing.Logging;
using UnityEngine;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private void Update()
    {
        Move();
    }

    //Usiamo l'attributo Client per assicurarci che solo l'owner possa muovere il proprio Player
    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(horizontal, 0f, vertical);
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        transform.position += _moveSpeed * Time.deltaTime * moveDirection;
    }

}