using System;
using DamageSystem;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth = 100;
        public UnitHealth Health; 
            
        private void Awake()
        {
            Initialized();
        }

        protected virtual void Initialized()
        {
            Health = new UnitHealth(_maxHealth);
            Health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamage>(out var damage))
            {
                if ((damage.Layer & (1 << gameObject.layer)) == 0)
                    return;
                ApplyDamage(damage);
            }
        }

        public void ApplyDamage(IDamage damage)
        {
            Health.ApplyDamage(damage);
        }
    }
}
