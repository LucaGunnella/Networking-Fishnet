using System;
using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    private PlayerMovement2D _playerMovement;
    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement2D>();
    }

    public void OnStartAttacking()
    {
        _playerMovement.CanMove = false;
    }

    public void OnStopAttacking()
    {
        _playerMovement.CanMove = true;
    }
}
