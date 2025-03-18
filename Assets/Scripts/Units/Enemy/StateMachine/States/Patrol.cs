using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Patrol : Idle
    {
        private Vector3[] _path;
        
        public Patrol(Enemy enemy) : base(enemy)
        {
            _path = enemy.Parameters.Path;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            MoveByPath();
        }

        private void MoveByPath()
        {
            
        }
    }
}