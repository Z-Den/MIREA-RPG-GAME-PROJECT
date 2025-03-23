using System;
using JetBrains.Annotations;
using Units.Input;
using UnityEngine;

namespace Units.Enemy.StateMachine
{
    public abstract class State : IUnitInput
    {
        protected Enemy Enemy;
        public Action IsStateChange;
        public Vector2 MoveDirection { get; set; }
        public float Rotation { get; set; }
        public Action ShotStarted { get; set; }
        public Action ShotCanceled { get; set; }
        public Action Spell1Started { get; set; }
        public Action Spell1Canceled { get; set; }
        public Action RunStarted { get; set; }
        public Action RunCanceled { get; set; }
            
        public State(Enemy enemy)
        {
            Enemy = enemy;
        }
        
        public abstract void OnEnter(); 
        
        public abstract void OnUpdate();
        
        public abstract void OnExit();
    }
}