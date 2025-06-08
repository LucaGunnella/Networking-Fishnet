using FishNet.Object;
using System;
using UnityEngine;

public class AttackComponent : NetworkBehaviour
{
    private Animator _animator;
    [NonSerialized] public bool CanAttack;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        CanAttack = true;
    }

    private void Update()
    {
        if(!CanAttack) return;
        
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
