using UnityEngine;
using UnityEngine.Serialization;

namespace Units
{
    public class PhysicalMover : MonoBehaviour
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
        [SerializeField] protected Rigidbody Rigidbody;
        private Quaternion _targetRotation;
        private Vector3 _moveVector;
        private Vector3 _goalVelocity;
        private Vector3 _direction;
        private float _degree;
        private float _sensitivity = 0.1f;

        public bool IsOnGround { get; private set; }
        public virtual float MaxSpeed => _maxSpeed;

        public void SetMoveDirection(Vector3 directoion)
        {
            _direction = directoion;
        }

        public void SetRotationDegree(float rotationAngle)
        {
            _degree += rotationAngle * _sensitivity;
            if (_degree >= 360) _degree = 0;
            else if (_degree < 0) _degree = 360;
            var newRotation = Quaternion.Euler(new Vector3(0, _degree, 0));
            SetRotation(newRotation);
        }

        public void SetRotation(Quaternion rotation)
        {
            _targetRotation = Quaternion.Lerp(_targetRotation, rotation, 
                Time.deltaTime * _rotationSpeed);
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
            if (move.magnitude > 1f) move.Normalize();
            
            var velocityDot = Vector3.Dot(move, Rigidbody.linearVelocity);
            var acceleration = _accelerationPower * _accelerationFromDot.Evaluate(velocityDot);

            var velocity = move * MaxSpeed;
            _goalVelocity = Vector3.MoveTowards(_goalVelocity, velocity, acceleration * Time.fixedDeltaTime);

            var neededAccel = (_goalVelocity - Rigidbody.linearVelocity) / Time.fixedDeltaTime;
            neededAccel = Vector3.ClampMagnitude(neededAccel, _maxAcceleration);
            Rigidbody.AddForce(neededAccel * Rigidbody.mass);
        }

        private void Floating(RaycastHit hit)
        {
            var velocity = Rigidbody.linearVelocity;
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
            Rigidbody.AddForce(rayDirection * springForce);

            if (hitbody)
                hitbody.AddForceAtPosition(rayDirection * -springForce, hit.point);
        }

        private void RotationStabilization()
        {
            var toGoal = _targetRotation * Quaternion.Inverse(Rigidbody.transform.rotation);

            toGoal.ToAngleAxis(out var rotDegrees, out var rotAxis);
            rotAxis.Normalize();

            if (rotDegrees > 180f)
                rotDegrees -= 360f;
            
            var rotRadians = rotDegrees * Mathf.Deg2Rad;
            
            Rigidbody.AddTorque((rotAxis * (rotRadians * _rotationSpringStrength)) -
                                 (Rigidbody.angularVelocity * _rotationSpringDamper));
        }

        private bool CheckGround(out RaycastHit hit)
        {
            return Physics.Raycast(Rigidbody.transform.position, -Vector3.up, 
                out hit, _distanceToFloor * 2, _groundLayers);
        }
    }
}