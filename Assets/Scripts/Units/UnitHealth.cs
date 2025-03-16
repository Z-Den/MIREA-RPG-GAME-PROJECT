using System;
using DamageSystem;
using UnityEngine;

namespace Units
{
    public class UnitHealth
    {
        public int Health { get; private set; }
        public readonly int MaxHealth;
        public Action OnDeath;
        public Action<int> DamageApplied;
        public float FillValue => (float)Health / MaxHealth;


        public UnitHealth(int maxHealthValue)
        {
            MaxHealth = maxHealthValue;
            Health = MaxHealth;
        }

        public void ApplyDamage(IDamage damage)
        {
            var damageValue = damage.Value;
            Health -= damageValue;
            if (Health <= 0)
                Die();
            DamageApplied?.Invoke(damageValue);
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}