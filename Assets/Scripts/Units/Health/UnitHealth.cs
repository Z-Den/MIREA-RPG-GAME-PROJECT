using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using Units.UI;
using UnityEngine;

namespace Units.Health
{
    public class UnitHealth : MonoBehaviour, IDamageable, IUIElementHolder
    {
        [SerializeField] private float _maxHealth;
        [Header("UI")]
        [SerializeField] private UnitUI _unitUI;
        [SerializeField] private Bar _healthBarPrefab;
        private float _health;
        private Bar _healthBar;
        public Action OnDeath;
        public Action<float, float> HealthChanged;
        public Action<float> DamageApplied;
        public UnitUI UI => _unitUI;
        
        private List<IDamage> _damageImmunitySources;
        private float _damageImmunityTime = 0.5f;
        
        private void OnEnable()
        {
            _damageImmunitySources = new List<IDamage>();
            _health = _maxHealth;
            SetUIElement();
            HealthChanged?.Invoke(_health, _maxHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamage>(out var damage))
            {
                if ((damage.Layer & (1 << gameObject.layer)) == 0)
                    return;
                
                if (_damageImmunitySources.Contains(damage))
                    return;
                
                StartCoroutine(DamageImmunity(damage));
                ApplyDamage(damage);
            }
        }
        
        public void ApplyDamage(IDamage damage)
        {
            var damageValue = damage.Value;
            _health -= damageValue;
            HealthChanged?.Invoke(_health, _maxHealth);
            if (_health <= 0)
                Die();
            DamageApplied?.Invoke(damageValue);
        }

        private IEnumerator DamageImmunity(IDamage souse)
        {
            _damageImmunitySources.Add(souse);
            yield return new WaitForSeconds(_damageImmunityTime);
            _damageImmunitySources.Remove(souse);
        }

        protected virtual void Die()
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        public void SetUIElement()
        {
            _healthBar = Instantiate(_healthBarPrefab);
            HealthChanged += _healthBar.UpdateBar;
            UI.Add(_healthBar);
        }

        public void RemoveUIElement()
        {
            HealthChanged -= _healthBar.UpdateBar;
            Destroy(_healthBar);
        }
    }
}