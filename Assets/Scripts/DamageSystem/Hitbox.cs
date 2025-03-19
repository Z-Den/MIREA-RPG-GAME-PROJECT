using UnityEngine;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public class Hitbox: MonoBehaviour, IDamage
    {
        [SerializeField] private int _damageValue;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private LayerMask _layerMask;
        
        public int Value => _damageValue;
        public DamageType Type => _damageType;
        public LayerMask Layer => _layerMask;
        
        public virtual void CollideWith(IDamageable damageable){}
    }
}