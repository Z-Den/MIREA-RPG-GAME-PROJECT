using UnityEngine;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public class Hitbox: MonoBehaviour, IDamage
    {
        [SerializeField] private float _damageValue;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private LayerMask _layerMask;
        
        public float Value => _damageValue;
        public DamageType Type => _damageType;
        public LayerMask Layer => _layerMask;
        
        public virtual void CollideWith(IDamageable damageable){}
    }
}