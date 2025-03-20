using UnityEngine;

namespace DamageSystem
{
    public class Damage : IDamage
    {
        public float Value { get; }
        public DamageType Type { get; }
        public LayerMask Layer { get; }
        
        public void CollideWith(IDamageable damageable){}

        public Damage(float value, DamageType type, LayerMask layer = default)
        {
            Value = value;
            Type = type;
            Layer = layer;
        }
    }
}