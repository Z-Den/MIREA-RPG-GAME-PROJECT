using System;
using Units.Enemy.StateMachine;
using Units.UI;
using UnityEngine;
using UnityEngine.Serialization;
using IUIElementHolder = Units.UI.IUIElementHolder;

namespace Units.Enemy
{
    public class Enemy : Unit
    {
        [SerializeField] private PhysicalMover _mover;
        [SerializeField] private EnemyParameters _parameters;
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private UnitUI _unitUI;
        
        public PhysicalMover Mover => _mover;
        public EnemyParameters Parameters => _parameters;
        public EnemyStateMachine StateMachine => _stateMachine;
        public UnitUI UnitUI => _unitUI;
        
        protected void Awake()
        { 
            _stateMachine.Init(this);
            AddUI();
        }

        private void AddUI()
        {
            foreach (var uiHolder in GetComponents<IUIElementHolder>())
            {
                var element = uiHolder.GetUIElement();
                if (element != null)
                    UnitUI.Add(element);
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
