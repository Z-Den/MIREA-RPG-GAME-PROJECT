using System;
using Units.Input;
using Units.UI;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalMover : MonoBehaviour, IUIElementHolder
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
        [Header("Stamina")]
        [SerializeField] private float _maxSpeedMultiplier = 2;
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _staminaRegenPerSecond;
        [SerializeField] private float _staminaSpendPerSecond;
        [Header("UI")]
        [SerializeField] private TwoSideBar _staminaBarPrefab;
        private Quaternion _targetRotation;
        private Rigidbody _rigidbody;
        private Vector3 _moveVector;
        private Vector3 _goalVelocity;
        private Vector3 _direction;
        private float _degree;
        private float _sensitivity = 0.1f;
        private float _stamina;
        private float _speedMultiplier = 1f;
        private bool _isRunning;
        
        public Action<float, float> OnStaminaChanged;
        public bool IsOnGround { get; private set; }
        public float MaxSpeed => _maxSpeed * _speedMultiplier;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _stamina = _maxStamina;
            OnStaminaChanged?.Invoke(_stamina, _maxStamina);
        }

        public void RunStarted()
        {
            if (_stamina / _maxStamina < 0.1f)
                return;
            
            _speedMultiplier = _maxSpeedMultiplier;
            _isRunning = true;
        }
        
        public void RunCanceled()
        {
            _speedMultiplier = 1f;
            _isRunning = false;
        }
        
        private void Update()
        {
            Regenerate();
            Spend();
            
            if (_stamina <= 0)
            {
                _stamina = 0;
                RunCanceled();
            }
        }
        
        private void Regenerate()
        {
            if (_stamina < _maxStamina && !_isRunning)
            {
                _stamina += Time.deltaTime * _staminaRegenPerSecond;
                OnStaminaChanged?.Invoke(_stamina, _maxStamina);
            }
        }

        private void Spend()
        {
            if (_stamina > 0 && _isRunning)
            {
                _stamina -= Time.deltaTime * _staminaSpendPerSecond;
                OnStaminaChanged?.Invoke(_stamina, _maxStamina);
            }
        }
        
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
            
            var velocityDot = Vector3.Dot(move, _rigidbody.linearVelocity);
            var acceleration = _accelerationPower * _accelerationFromDot.Evaluate(velocityDot);

            var velocity = move * MaxSpeed;
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
            var toGoal = _targetRotation * Quaternion.Inverse(_rigidbody.transform.rotation);

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
            return Physics.Raycast(_rigidbody.transform.position, -Vector3.up, 
                out hit, _distanceToFloor * 2, _groundLayers);
        }

        public UIElement GetUIElement()
        {
            if (_staminaBarPrefab == null)
                return null;
            
            var staminaBar = Instantiate(_staminaBarPrefab);
            OnStaminaChanged += staminaBar.FillBar;
            return staminaBar;
        }
    }
}