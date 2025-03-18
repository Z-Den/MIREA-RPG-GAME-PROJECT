using System;
using UnityEngine;

namespace Units.Player
{
    public class PlayerInput
    {
        private PlayerInputAction _playerInput;
        
        public Vector2 MoveDirection {get; private set;}
        public float Rotation {get; private set;}
        public Action ShotClicked;
        
        public PlayerInput()
        {
            _playerInput = new PlayerInputAction();
            _playerInput.Enable();
            _playerInput.Player.Shot.performed += _ => ShotClicked?.Invoke();
        }

        public void Update()
        {
            MoveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            Rotation = _playerInput.Player.Rotation.ReadValue<float>();
        }
    }
}
