using UnityEngine;

namespace Units.Enemy.StateMachine.States
{
    public class Idle : OffState
    {
        private float _timer = 0;
        private readonly float _checkDelay;
        private readonly float _checkSphereRange;
        private readonly LayerMask _respondMask;
        
        public Idle(Enemy enemy) : base(enemy)
        {
            _checkSphereRange = enemy.Parameters.CheckSphereRadius;
            _checkDelay = enemy.Parameters.CheckDelay;
            _respondMask = enemy.Parameters.RespondMask;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            SetTimer();
        }

        protected void SetTimer()
        {
            if (_timer < 0)
            {
                _timer = _checkDelay;
                CheckPlayer();
            }

            _timer -= Time.deltaTime;
        }

        private void CheckPlayer()
        {
            var colliders = Physics.OverlapSphere(Enemy.transform.position, _checkSphereRange, _respondMask);
            if (colliders.Length > 0)
            {
                IsStateChange?.Invoke();
            }
        }
    }
}