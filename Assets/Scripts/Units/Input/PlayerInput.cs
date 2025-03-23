using System;
using UnityEngine;

namespace Units.Input
{
    public class PlayerInput : MonoBehaviour, IUnitInput
    {
        private PlayerInputAction _playerInput;
        
        public Vector2 MoveDirection {get; set;}
        public float Rotation {get; set;}
        public Action ShotStarted {get; set;}
        public Action ShotCanceled {get; set;}
        public Action Spell1Started {get; set;}
        public Action Spell1Canceled {get; set;}
        public Action RunStarted {get; set;}
        public Action RunCanceled {get; set;}
        
        public void Awake()
        {
            _playerInput = new PlayerInputAction();
            _playerInput.Enable();
            
            _playerInput.Player.Shot.started += _ => ShotStarted?.Invoke();
            _playerInput.Player.Shot.canceled += _ => ShotCanceled?.Invoke();
            
            _playerInput.Player.Shield.started += _ => Spell1Started?.Invoke();
            _playerInput.Player.Shield.canceled += _ => Spell1Canceled?.Invoke();
            
            _playerInput.Player.Run.started += _ => RunStarted?.Invoke();
            _playerInput.Player.Run.canceled += _ => RunCanceled?.Invoke();
        }

        public void Update()
        {
            MoveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            Rotation = _playerInput.Player.Rotation.ReadValue<float>();
        }
    }
}
