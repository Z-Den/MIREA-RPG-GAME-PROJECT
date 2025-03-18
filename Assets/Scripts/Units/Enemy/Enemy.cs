using System;
using Units.Enemy.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Enemy
{
    public class Enemy : Unit
    {
        public EnemyParameters Parameters;
        public EnemyStateMachine StateMachine;
        
        protected override void Initialized()
        { 
            base.Initialized();
            StateMachine.Init(this);
        }

    }
}
