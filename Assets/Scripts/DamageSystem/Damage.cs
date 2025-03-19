using UnityEngine;

namespace DamageSystem
{
    public class Damage : IDamage
    {
        public int Value { get; }
        public DamageType Type { get; }
        public LayerMask Layer { get; }
        
        public void CollideWith(IDamageable damageable){}

        public Damage(int value, DamageType type, LayerMask layer = default)
        {
            Value = value;
            Type = type;
            Layer = layer;
        }
    }
}