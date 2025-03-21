using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using Units.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Player
{
    [Serializable]
    public class Shield : MonoBehaviour, IUIElementHolder
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _shield;
        [SerializeField] private LayerMask _interactLayer;
        [Header("UI")]
        [SerializeField] private TwoSideBar _shieldBarPrefab;
        [SerializeField] private UnitUI _unitUI;
        [Header("Parameters")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _regenerationPerSecond = 10f;
        [SerializeField] private float _maxShieldSize;
        [SerializeField] private float _sizeChangeSpeed;
        private float _health;
        private TwoSideBar _shieldBar;
        private float _currentSize;
        private float _goatSize;
        private List<IDamage> _damageImmunitySources;
        private float _damageImmunityTime = 0.5f;
        
        public Action<float, float> ShieldHealthChanged;
        public Action<float> DamageApplied;
        public UnitUI UI => _unitUI;
        public bool CanShieldBeActive => _health / _maxHealth > 0.1f;
        public bool IsShieldActive => _goatSize > 0f;
        
        public void OnEnable()
        {
            _damageImmunitySources = new List<IDamage>();
            _health = _maxHealth;
            _playerInput.ShieldStarted += ShieldStarted ;
            _playerInput.ShieldCanceled += ShieldCanceled;
            SetUIElement();
            ShieldHealthChanged?.Invoke(_health, _maxHealth);
        }

        private void OnDisable()
        {
            _playerInput.ShieldStarted -= ShieldStarted;
            _playerInput.ShieldCanceled -= ShieldCanceled;
            RemoveUIElement();
        }

        private void ShieldStarted()
        {
            if (CanShieldBeActive)
                _goatSize = _maxShieldSize;
        }

        private void Update()
        {
            _currentSize = Mathf.Lerp(_currentSize, _goatSize, Time.deltaTime * _sizeChangeSpeed);
            _shield.transform.localScale = Vector3.one * _currentSize;

            transform.position = _playerInput.transform.position;
            Regenerate();
            
            if (IsShieldActive)
                ShieldHealthChanged?.Invoke(_health, _maxHealth);
        }

        private void Regenerate()
        {
            if (_health < _maxHealth && !IsShieldActive)
            {
                _health += Time.deltaTime * _regenerationPerSecond;
                ShieldHealthChanged?.Invoke(_health, _maxHealth);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamage>(out var damage))
            {
                if (_damageImmunitySources.Contains(damage))
                    return;
                
                if ((damage.Layer & (1 << gameObject.layer)) == 0)
                    return;
                
                ApplyDamage(damage);
                StartCoroutine(DamageImmunity(damage));
            }
        }
        
        public void ApplyDamage(IDamage damage)
        {
            var damageValue = damage.Value;
            _health -= damageValue;
            if (_health <= 0)
            {
                _health = 0;
                ShieldCanceled();
            }
            ShieldHealthChanged?.Invoke(_health, _maxHealth);
            DamageApplied?.Invoke(damageValue);
        }

        private IEnumerator DamageImmunity(IDamage souse)
        {
            _damageImmunitySources.Add(souse);
            yield return new WaitForSeconds(_damageImmunityTime);
            _damageImmunitySources.Remove(souse);
        }
        
        private void ShieldCanceled()
        {
            _goatSize = 0f;
        }
        
        public void SetUIElement()
        {
            _shieldBar = Instantiate(_shieldBarPrefab);
            ShieldHealthChanged += _shieldBar.FillBar;
            UI.Add(_shieldBar);
        }

        public void RemoveUIElement()
        {
            ShieldHealthChanged -= _shieldBar.FillBar;
        }
    }
}