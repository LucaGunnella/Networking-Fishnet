using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;
    private EsercizioInClasse _esercizioInputAction;
    
    [SerializeField]private GameObject _bulletPrefab;

    public void Awake()
    {
        _esercizioInputAction = new EsercizioInClasse();
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnFirePerformed(InputAction.CallbackContext obj)
    {
        Instantiate(_bulletPrefab, this.transform.position + new Vector3(1, 0, 1), this.transform.rotation);
    }

    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        _speed += 3f;
    }
    
    private void OnSprintCanceled(InputAction.CallbackContext obj)
    {
        _speed -= 3f;
    }

    public void OnMoveStop(InputAction.CallbackContext obj)
    {
        _direction = Vector3.zero;
    }

    public void StartJump(InputAction.CallbackContext obj)
    {
        StartCoroutine(Jump(obj));
    }
    private IEnumerator Jump(InputAction.CallbackContext obj)
    {
        transform.position += new Vector3(0, 3, 0);
        
        yield return new WaitForSeconds(2f);
        
        transform.position -= new Vector3(0, 3, 0);
    }

    private void OnMoveAction(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        _direction = new Vector3(value.x, 0, value.y);
    }

    private void OnEmoteStart(InputAction.CallbackContext obj)
    {
        transform.rotation = Quaternion.Euler(90,0, 0);
    }

    private void OnEmoteEnd(InputAction.CallbackContext obj)
    {
        transform.rotation = Quaternion.Euler(0,0, 0);
    }
    
    public void OnEnable()
    {
        _esercizioInputAction.Enable();
        _esercizioInputAction.Player.Move.performed += OnMoveAction;
        _esercizioInputAction.Player.Move.canceled += OnMoveStop;
        _esercizioInputAction.Player.Jump.performed += StartJump;
        
        _esercizioInputAction.Player.Sprint.performed += OnSprintPerformed;
        _esercizioInputAction.Player.Sprint.canceled += OnSprintCanceled;
        
        _esercizioInputAction.Player.Fire.started += OnFirePerformed;
        
        _esercizioInputAction.Player.Emote.performed += OnEmoteStart;
        _esercizioInputAction.Player.Emote.canceled += OnEmoteEnd;

        
    }

    public void OnDisable()
    {
        _esercizioInputAction.Player.Jump.performed -= StartJump;
        _esercizioInputAction.Player.Move.performed -= OnMoveAction;
        
        _esercizioInputAction.Player.Sprint.performed -= OnSprintPerformed;
        _esercizioInputAction.Player.Sprint.canceled -= OnSprintCanceled;
        
        _esercizioInputAction.Player.Fire.started -= OnFirePerformed;
        
        _esercizioInputAction.Player.Emote.started -= OnFirePerformed;
        _esercizioInputAction.Player.Emote.performed -= OnFirePerformed;
        
        _esercizioInputAction.Player.Emote.performed -= OnEmoteStart;
        _esercizioInputAction.Player.Emote.canceled -= OnEmoteEnd;
    }
    
}
