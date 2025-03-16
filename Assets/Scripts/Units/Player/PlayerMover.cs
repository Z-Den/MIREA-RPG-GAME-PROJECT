using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private PlayerInput _playerInput;
        private float _rotation;
        
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
            var moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
            var moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            Move(moveDirection);
            
            var rotationInput = _playerInput.Player.Rotation.ReadValue<float>();
            Rotate(rotationInput);
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.linearVelocity = direction * _moveSpeed;
        }

        private void Rotate(float rotationDegree)
        {
            _rotation += rotationDegree * Time.deltaTime * _rotationSpeed;
            transform.rotation = Quaternion.Euler(0, _rotation, 0);
        }
    }
}
