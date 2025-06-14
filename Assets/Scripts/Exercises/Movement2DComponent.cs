using FishNet.Managing.Logging;
using FishNet.Object;
using System;
using UnityEngine;

public class Movement2DComponent : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    // Synchronize animator
    private Animator _animator;

    [NonSerialized] public bool CanMove;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        CanMove = true;
    }

    private void Update()
    {
        if (!CanMove) return;
        
        Move();
    }

    // Using Client attribute to force execution only for the owner of this NetworkObject.
    // LoggingType = if and what message should be broadcast in console upon not meeting requirements.
    // RequireOwnership = if ownership is required to execute this method.
    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        var moveDirection = new Vector3(horizontal, 0f, 0f);
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();
        
        transform.position += _moveSpeed * Time.deltaTime * moveDirection;;

        _animator.SetBool("IsWalking", horizontal != 0f);
    }
}