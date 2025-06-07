using System;
using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    private PlayerMovement2D _playerMovement;
    private PlayerAttack _playerAttack;
    
    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement2D>();
        _playerAttack = GetComponentInParent<PlayerAttack>();
    }

    public void OnStartAttacking()
    {
        _playerMovement.CanMove = false;
    }

    public void OnStopAttacking()
    {
        _playerMovement.CanMove = true;
    }

    public void OnStartKnockback()
    {
        _playerMovement.CanMove = false;
        _playerAttack.CanAttack = false;
    }

    public void OnStopKnockback()
    {
        _playerMovement.CanMove = true;
        _playerAttack.CanAttack = true;
    }
}
