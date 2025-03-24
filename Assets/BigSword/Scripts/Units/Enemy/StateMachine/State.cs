using System;
using JetBrains.Annotations;
using Units.Input;
using UnityEngine;

namespace Units.Enemy.StateMachine
{
    public abstract class State
    {
        protected Enemy Enemy;
        public Action IsStateChange;
            
        public State(Enemy enemy)
        {
            Enemy = enemy;
        }
        
        public abstract void OnEnter(); 
        
        public abstract void OnUpdate();
        
        public abstract void OnExit();
    }
}