using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Patrol : Idle
    {
        private Vector3[] _path;
        private int _currentPathIndex;
        private const float _minDistanceToPoint = 3f;
        
        public Patrol(Enemy enemy) : base(enemy)
        {
            _path = enemy.Parameters.Path;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _currentPathIndex = 0;
        }

        public override void OnUpdate()
        {
            SetTimer();
            MoveByPath();
        }

        private void MoveByPath()
        {
            var isToClose = (_path[_currentPathIndex] - Enemy.transform.position).magnitude < _minDistanceToPoint;
            if (isToClose)
                _currentPathIndex++;

            if (_currentPathIndex >= _path.Length)
                _currentPathIndex = 0;

            var direction = _path[_currentPathIndex] - Enemy.transform.position;
            direction.y = 0;
            Enemy.Mover.SetMoveDirection(direction);
            Enemy.Mover.SetRotation(Quaternion.LookRotation(direction));
        }
    }
}