using System;
using Units.Enemy.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Enemy
{
    public class Enemy : Unit
    {
        [SerializeField] private PhysicalMover _mover;
        [SerializeField] private EnemyParameters _parameters;
        [SerializeField] private EnemyStateMachine _stateMachine;
        
        public PhysicalMover Mover => _mover;
        public EnemyParameters Parameters => _parameters;
        public EnemyStateMachine StateMachine => _stateMachine;
        
        protected void Awake()
        { 
            _stateMachine.Init(this);
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
