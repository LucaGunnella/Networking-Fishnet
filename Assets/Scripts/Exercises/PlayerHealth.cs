using FishNet.Object;
using System;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
     // Add a synchronized variable for health and bind it to UI
     
     // Synchronize animator
     private Animator _animator;

     private void Awake()
     {
          GetComponentInChildren<Animator>();
     }
     
     // Add your code

     private void ReceiveHit()
     {
          _animator.SetTrigger("HasBeenHit");
     }
}
