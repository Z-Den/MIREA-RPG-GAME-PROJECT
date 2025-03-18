using System;
using Units.Player.Spells;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit
    {
        [Header("Player settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Weapon.Weapon _weapon;
        [SerializeField] private PlayerUI _playerUI;
        private PlayerMover _playerMover;

        public Weapon.Weapon Weapon => _weapon;
        public PlayerInput PlayerInput {get; private set;}
        public SpellHolder SpellHolder {get; private set;}
        public Rigidbody Rigidbody => _rigidbody;
        
        protected override void Initialized()
        {
            base.Initialized();
            PlayerInput = new PlayerInput();
            SpellHolder = new SpellHolder();
            _playerMover = new PlayerMover(this);
            _weapon.Init(this);
        }

        private void Update()
        {
            PlayerInput.Update();
            _playerMover.Update();
        }
    }
}
