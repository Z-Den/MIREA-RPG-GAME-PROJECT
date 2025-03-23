using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Patrol : Idle
    {
        private Vector3[] _path;
        private int _currentPathIndex;
        private const float _minDistanceToPoint = 3f;
        
        protected Vector3 TargetPoint;
        
        public Patrol(Enemy enemy) : base(enemy)
        {
            _path = enemy.Parameters.Path;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _currentPathIndex = 0;
            TargetPoint = _path[_currentPathIndex];
        }

        public override void OnUpdate()
        {
            SetTimer();
            MoveByPath();
        }

        private void MoveByPath()
        {
            var isToClose = (TargetPoint - Enemy.transform.position).magnitude < _minDistanceToPoint;
            if (isToClose)
                TargetPoint = GetNextPoint();

            var direction = TargetPoint - Enemy.transform.position;
            direction.y = 0;
            Enemy.Mover.SetMoveDirection(direction);
            Enemy.Mover.SetRotation(Quaternion.LookRotation(direction));
        }

        protected virtual Vector3 GetNextPoint()
        {
            _currentPathIndex++;

            if (_currentPathIndex >= _path.Length)
                _currentPathIndex = 0;
            
            return _path[_currentPathIndex];
        }
    }
}