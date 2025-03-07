using System;
using DamageSystem;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private UnitUI _unitUI;
        public UnitHealth Health; 
            
        private void Awake()
        {
            Health = new UnitHealth(_maxHealth);
            Health.OnDeath += OnDeath;
            _unitUI.Init(this);
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamage>(out var damage))
                ApplyDamage(damage);
        }

        public void ApplyDamage(IDamage damage)
        {
            Health.ApplyDamage(damage);
        }
    }
}
