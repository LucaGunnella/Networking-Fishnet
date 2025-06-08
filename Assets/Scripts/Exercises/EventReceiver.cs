using System;
using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    private Movement2DComponent _movement;
    private AttackComponent _attackComponent;
    
    private void Awake()
    {
        _movement = GetComponentInParent<Movement2DComponent>();
        _attackComponent = GetComponentInParent<AttackComponent>();
    }

    public void OnStartAttacking()
    {
        _movement.CanMove = false;
    }

    public void OnStopAttacking()
    {
        _movement.CanMove = true;
    }

    public void OnStartKnockback()
    {
        _movement.CanMove = false;
        _attackComponent.CanAttack = false;
    }

    public void OnStopKnockback()
    {
        _movement.CanMove = true;
        _attackComponent.CanAttack = true;
    }
}
