using System;
using Units.Player.Spells;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit
    {
        [Header("Player settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PhysicalMover _physicalMover;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Shield _shield;
        
        public PlayerInput PlayerInput => _playerInput;
        public SpellHolder SpellHolder {get; private set;}
        public Rigidbody Rigidbody => _rigidbody;
        
        protected override void Initialized()
        {
            base.Initialized();
            SpellHolder = new SpellHolder();
        }

        private void Update()
        {
            ApplyMove();
        }

        private void ApplyMove()
        {
            var moveDirection = (PlayerInput.MoveDirection.y * _rigidbody.transform.forward + 
                                 PlayerInput.MoveDirection.x * _rigidbody.transform.right);
            _physicalMover.SetMoveDirection(moveDirection);
            _physicalMover.SetRotationDegree(PlayerInput.Rotation);
        }
    }
}
