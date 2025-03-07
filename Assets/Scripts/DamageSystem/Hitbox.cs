using UnityEngine;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public class Hitbox: MonoBehaviour, IDamage
    {
        [SerializeField] private int _damageValue;
        [SerializeField] private DamageType _damageType;

        public int Value => _damageValue;
        public DamageType Type => _damageType;
    }
}