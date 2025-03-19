using System;
using UnityEngine;

namespace Units.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputAction _playerInput;
        
        public Vector2 MoveDirection {get; private set;}
        public float Rotation {get; private set;}
        public Action ShotStarted;
        public Action ShotCanceled;
        public Action ShieldStarted;
        public Action ShieldCanceled;
        
        public void Awake()
        {
            _playerInput = new PlayerInputAction();
            _playerInput.Enable();
            
            _playerInput.Player.Shot.started += _ => ShotStarted?.Invoke();
            _playerInput.Player.Shot.canceled += _ => ShotCanceled?.Invoke();
            
            _playerInput.Player.Shield.started += _ => ShieldStarted?.Invoke();
            _playerInput.Player.Shield.canceled += _ => ShieldCanceled?.Invoke();
        }

        public void Update()
        {
            MoveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            Rotation = _playerInput.Player.Rotation.ReadValue<float>();
        }
    }
}
