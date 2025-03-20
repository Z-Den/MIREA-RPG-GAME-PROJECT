using System;
using Units.Player.Spells;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit
    {
        [Header("Player settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PlayerMover _physicalMover;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Shield _shield;
        
        public PlayerInput PlayerInput => _playerInput;
        
    }
}
