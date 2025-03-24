using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DamageSystem;
using Units.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Health
{
    public class UnitHealth : MonoBehaviour, IDamageable, IUIElementHolder
    {
        [SerializeField] private float _maxHealth;
        [Header("Resistances")]
        [SerializeField] private DamageType[] _damageResistances;
        [SerializeField] private DamageType[] _damageImmunities;
        [Header("UI")]
        [SerializeField] private HealthBar _healthBarPrefab;
        private float _health;
        private List<IDamage> _damageImmunitySources;
        private float _damageImmunityTime = 0.5f;
        
        public Action OnDeath;
        public Action<float, float> HealthChanged;
        public Action<float> DamageApplied;
        public Action<DamageType[], DamageType[]> ResistanceChanged;
        public DamageType[] DamageResistances => _damageResistances;
        public DamageType[] DamageImmunities => _damageImmunities;
        
        private void Start()
        {
            _damageImmunitySources = new List<IDamage>();
            _health = _maxHealth;
            HealthChanged?.Invoke(_health, _maxHealth);
            ResistanceChanged?.Invoke(DamageImmunities, DamageResistances);
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
            var damageValue = CalculateDamage(damage);
            _health -= damageValue;
            HealthChanged?.Invoke(_health, _maxHealth);
            if (_health <= 0)
                Die();
            DamageApplied?.Invoke(damageValue);
        }

        private float CalculateDamage(IDamage damage)
        {
            if (DamageImmunities.Contains(damage.Type))
                return 0;
            if (DamageResistances.Contains(damage.Type))
                return damage.Value / 2;
            return damage.Value;
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

        public UIElement GetUIElement()
        {
            var healthBar = Instantiate(_healthBarPrefab);
            HealthChanged += healthBar.FillBar;
            ResistanceChanged += healthBar.ResistanceIconsUpdate;
            return healthBar;
        }
    }
}