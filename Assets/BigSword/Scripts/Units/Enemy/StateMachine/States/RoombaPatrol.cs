using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class RoombaPatrol : Patrol
    {
        private float _changePointTime = 3f;
        private float _nextPointDistance = 15f;
        private float _timer;
        
        public RoombaPatrol(Enemy enemy) : base(enemy)
        {
            _timer = 0f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _timer += Time.deltaTime;
            if (_timer >= _changePointTime)
            {
                _timer = 0;
                TargetPoint = GetNextPoint();
            }
        }

        protected override Vector3 GetNextPoint()
        {
            var dir = new Vector3(Random.value, Random.value, Random.value);
            Enemy.Mover.SetMoveDirection(Vector3.up);
            return Enemy.transform.position + dir.normalized * _nextPointDistance;
        }
    }
}