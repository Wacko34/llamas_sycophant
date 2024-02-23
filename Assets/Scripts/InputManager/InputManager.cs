using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private IMovable _playerMovement;
    private PlayerInput _playerInput;

    private void Awake() 
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Enable();

        if(!TryGetComponent<IMovable>(out _playerMovement))
        {
            Debug.LogWarning("There Is No IMovable object");
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _playerMovement.Move(_playerInput.Player.Move.ReadValue<Vector2>());
    }
}
