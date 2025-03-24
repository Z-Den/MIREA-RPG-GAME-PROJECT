using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Follow : State
    {
        private readonly float _checkSphereRange;
        private readonly LayerMask _respondMask;
        
        protected Transform Taget;
        
        public Follow(Enemy enemy) : base(enemy)
        {
            _checkSphereRange = enemy.Parameters.CheckSphereRadius;
            _respondMask = enemy.Parameters.RespondMask;
        }

        public override void OnEnter()
        {
            Taget = GetTarget();
            if (!Taget)
                IsStateChange?.Invoke();
        }

        public override void OnUpdate()
        {
            var direction = Taget.position - Enemy.transform.position;
            
            if (direction.magnitude > _checkSphereRange * 2)
                IsStateChange?.Invoke();
            
            Enemy.Mover.SetMoveDirection(direction);
            Enemy.Mover.SetRotation(Quaternion.LookRotation(direction));
        }

        public override void OnExit()
        {
        }
        
        private Transform GetTarget()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange * 1.1f, _respondMask);
            if (colliders.Length == 0)
                return null;

            var minDistance = float.MaxValue;
            Transform target = null;
            foreach (var collider in colliders)
            {
                var distance = Vector3.Distance(collider.transform.position, Enemy.transform.position);
                if (distance < minDistance)
                    target = collider.transform;
            }
            return target;
        }
    }
}