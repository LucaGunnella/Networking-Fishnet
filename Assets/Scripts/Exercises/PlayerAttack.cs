using FishNet.Object;
using System;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    private Animator _animator;
    [NonSerialized] public bool CanAttack;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // Add logic and make this method synchronized
    private void Attack()
    {
        _animator.SetTrigger("HasAttacked");
        
        // add your code
    }
}
