using System;
using UnityEngine;

namespace Units.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        private PlayerInput _playerInput;
        
        private void OnEnable()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }


        private void Update()
        {
            var input = _playerInput.Player.Move.ReadValue<Vector2>();
            var moveDirection = new Vector3(input.x, 0, input.y).normalized;
            Move(moveDirection);
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.linearVelocity = direction * _speed;
        }
    }
}
