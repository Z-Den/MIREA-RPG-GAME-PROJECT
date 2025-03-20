using System;
using Units.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Player
{
    public class PlayerMover : PhysicalMover, IUIElementHolder
    {
        [SerializeField] private PlayerInput _playerInput;
        [Header("Stamina")]
        [SerializeField] private float _maxSpeedMultiplier = 2;
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _staminaRegenPerSecond;
        [SerializeField] private float _staminaSpendPerSecond;
        [Header("UI")]
        [SerializeField] private UnitUI _unitUI;
        [SerializeField] private TwoSideBar _staminaBarPrefab;
        private TwoSideBar _staminaBar;
        private float _stamina;
        private float _speedMultiplier = 1f;
        private bool _isRunning;
        
        public Action<float, float> OnStaminaChanged;
        public override float MaxSpeed => base.MaxSpeed * _speedMultiplier;
        public UnitUI UI => _unitUI;
        
        public void OnEnable()
        {
            _stamina = _maxStamina;
            _playerInput.RunStarted += RunStarted ;
            _playerInput.RunCanceled += RunCanceled;
            SetUIElement();
            OnStaminaChanged?.Invoke(_stamina, _maxStamina);
        }

        private void OnDisable()
        {
            _playerInput.RunStarted -= RunStarted;
            _playerInput.RunCanceled -= RunCanceled;
            RemoveUIElement();
        }

        private void RunStarted()
        {
            if (_stamina / _maxStamina < 0.1f)
                return;
            
            _speedMultiplier = _maxSpeedMultiplier;
            _isRunning = true;
        }
        
        private void RunCanceled()
        {
            _speedMultiplier = 1f;
            _isRunning = false;
        }
        
        private void Update()
        {
            ApplyMove();
            
            Regenerate();
            Spend();
            
            if (_stamina <= 0)
            {
                _stamina = 0;
                RunCanceled();
            }
        }
        
        private void ApplyMove()
        {
            var moveDirection = (_playerInput.MoveDirection.y * Rigidbody.transform.forward + 
                                 _playerInput.MoveDirection.x * Rigidbody.transform.right);
            SetMoveDirection(moveDirection);
            SetRotationDegree(_playerInput.Rotation);
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
        
        public void SetUIElement()
        {
            _staminaBar = Instantiate(_staminaBarPrefab);
            OnStaminaChanged += _staminaBar.FillBar;
            UI.Add(_staminaBar);
        }

        public void RemoveUIElement()
        {
            OnStaminaChanged -= _staminaBar.FillBar;
        }
    }
}
