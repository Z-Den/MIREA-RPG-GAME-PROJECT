using UnityEngine;

namespace DamageSystem
{
    public interface IDamage
    {
        public int Value { get; }
        public DamageType Type { get; }
        public LayerMask Layer { get; }
    }
}
