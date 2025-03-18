using System;
using DamageSystem;
using UnityEngine;

namespace Units.Player.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _recoilPower = 1000;
        [SerializeField] private float _shotCooldown = 10f;
        private PlayerInput _playerInput;
        private float _cooldown;

        public Action<float, float> CooldownChanged;
        
        public void Init(Player player)
        {
            _playerInput = player.PlayerInput;
            _playerInput.ShotClicked += Shot;
            CooldownChanged(0, 1);
        }

        private void Update()
        {
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                _cooldown = _cooldown < 0? 0 : _cooldown;
                CooldownChanged?.Invoke(_cooldown, _shotCooldown);
            }
        }

        private void Shot()
        {
            if (_cooldown > 0)
                return;
            
            if (Physics.BoxCast(transform.position, Vector3.one, 
                    transform.forward , out RaycastHit hit, Quaternion.identity, 
                    15, _layerMask))
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    var damage = new Damage(10, DamageType.Magical, _layerMask);
                    damageable.ApplyDamage(damage);
                }
            }
            _rigidbody.AddForce(-transform.forward * _recoilPower);
            _cooldown = _shotCooldown;
        }
    }
}