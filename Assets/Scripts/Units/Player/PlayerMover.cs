using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Player
{
    public class PlayerMover
    {
        private readonly PlayerInput _playerInput;
        private Rigidbody _rigidbody;
        private float _moveSpeed = 5f;
        private float _rotationSpeed = 10f;
        private float _rotation;

        public PlayerMover(Player player)
        {
            _rigidbody = player.Rigidbody;
            _playerInput = player.PlayerInput;
        }
        
        public void Update()
        {
            Move(_playerInput.MoveDirection);
            Rotate(_playerInput.Rotation);
        }

        private void Move(Vector2 input)
        {
            var direction = new Vector3(input.x, 0, input.y).normalized;
            var value = (direction.z * _rigidbody.transform.forward + direction.x * _rigidbody.transform.right) * _moveSpeed;
            _rigidbody.linearVelocity = value;
        }

        private void Rotate(float rotationDegree)
        {
            _rotation += rotationDegree * Time.deltaTime * _rotationSpeed;
            _rigidbody.transform.rotation = Quaternion.Euler(0, _rotation, 0);
        }

    }
}
