using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class OffState : State
    {
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        
        public OffState(Enemy enemy) : base(enemy)
        {

        }

        public override void OnEnter()
        {
            _targetPosition = Enemy.transform.position;
            _targetRotation = Enemy.transform.rotation;
        }

        public override void OnUpdate()
        {
            Enemy.Mover.SetMoveDirection(_targetPosition - Enemy.transform.position);
            Enemy.Mover.SetRotation(_targetRotation);
        }

        public override void OnExit()
        {
            
        }
    }
}