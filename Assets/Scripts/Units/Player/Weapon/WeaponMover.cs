using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Player.Weapon
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponMover : MonoBehaviour
    {
        [Header("Ground Ray")]
        [SerializeField] private float _distanceToFloor = 0.6f;
        [SerializeField] private LayerMask _groundLayers;
        [Header("Move")]
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _accelerationPower = 20f;
        [SerializeField] private float _maxAcceleration = 100f;
        [SerializeField] private AnimationCurve _accelerationFromDot;
        [SerializeField] private float _rotationSpeed = 120f;
        [Header("Spring")] 
        [SerializeField] private float _springStrength = 2000f;
        [SerializeField] private float _springDamper = 100f;
        [SerializeField] private float _rotationSpringStrength = 100f;
        [SerializeField] private float _rotationSpringDamper = 20f;
        [SerializeField] private Transform _pivot;
        [SerializeField] private Rigidbody _rigidbody;
        private Quaternion _targetRotation;
        private Vector3 _moveVector;
        private Vector3 _goalVelocity;
        private Vector3 _direction;
        
        public bool IsOnGround
        {
            get; private set;
        }
        
        private void Update()
        {
            SetMoveDirection(_pivot.position - transform.position);
            SetTargetRotation();
        }

        private void SetMoveDirection(Vector3 directoion)
        {
            _direction = directoion;
        }

        private void FixedUpdate()
        {
            IsOnGround = CheckGround(out var hit);
            
            if (!IsOnGround) return;
            
            Floating(hit);
            HorizontalMove();
            RotationStabilization();
        }

        private void HorizontalMove()
        {
            var move = _direction;

            var velocityDot = Vector3.Dot(move, _rigidbody.linearVelocity);
            var acceleration = _accelerationPower * _accelerationFromDot.Evaluate(velocityDot);

            var velocity = move * _maxSpeed;
            _goalVelocity = Vector3.MoveTowards(_goalVelocity, velocity, acceleration * Time.fixedDeltaTime);

            var neededAccel = (_goalVelocity - _rigidbody.linearVelocity) / Time.fixedDeltaTime;
            neededAccel = Vector3.ClampMagnitude(neededAccel, _maxAcceleration);
            _rigidbody.AddForce(neededAccel * _rigidbody.mass);
        }

        private void Floating(RaycastHit hit)
        {
            var velocity = _rigidbody.linearVelocity;
            var rayDirection = -Vector3.up;

            var otherVelocity = Vector3.zero;
            var hitbody = hit.rigidbody;
            if (hitbody)
                otherVelocity = hitbody.linearVelocity;
            
            var rayDirVelocity = Vector3.Dot(rayDirection, velocity);
            var otherDirVelocity = Vector3.Dot(rayDirection, otherVelocity);

            var relVel = rayDirVelocity - otherDirVelocity;
            var deltaX = hit.distance - _distanceToFloor;
            var springForce = (deltaX * _springStrength) - (relVel * _springDamper);
            _rigidbody.AddForce(rayDirection * springForce);

            if (hitbody)
                hitbody.AddForceAtPosition(rayDirection * -springForce, hit.point);
        }

        private void RotationStabilization()
        {
            var toGoal = _targetRotation * Quaternion.Inverse(transform.rotation);

            toGoal.ToAngleAxis(out var rotDegrees, out var rotAxis);
            rotAxis.Normalize();

            if (rotDegrees > 180f)
                rotDegrees -= 360f;
            
            var rotRadians = rotDegrees * Mathf.Deg2Rad;
            
            _rigidbody.AddTorque((rotAxis * (rotRadians * _rotationSpringStrength)) -
                                 (_rigidbody.angularVelocity * _rotationSpringDamper));
        }

        private bool CheckGround(out RaycastHit hit)
        {
            return Physics.Raycast(transform.position, -Vector3.up, out hit, _distanceToFloor * 2, _groundLayers);
        }
        
        private void SetTargetRotation()
        {
            _targetRotation = Quaternion.Lerp(_targetRotation, _pivot.rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
