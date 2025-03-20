using UnityEngine;

namespace DamageSystem
{
    public interface IDamage
    {
        public float Value { get; }
        public DamageType Type { get; }
        public LayerMask Layer { get; }

        public void CollideWith(IDamageable damageable);
    }
}
