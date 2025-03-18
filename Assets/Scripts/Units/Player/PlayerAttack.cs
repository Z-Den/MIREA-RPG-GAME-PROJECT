using DamageSystem;
using UnityEngine;

namespace Units.Player
{
    public class PlayerAttack
    {
        private readonly PlayerInput _playerInput;
        private Weapon.Weapon _weapon;

        public PlayerAttack(Player player)
        {
            _playerInput = player.PlayerInput;
            _weapon = player.Weapon;
            _playerInput.ShotClicked += Shot;
        }
        

        private void Shot()
        {
            if (Physics.BoxCast(_weapon.transform.position, Vector3.one * 0.5f, _weapon.transform.forward, out RaycastHit hit))
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    var damage = new Damage(10, DamageType.Magical);
                    damageable.ApplyDamage(damage);
                }
                    
            
        }
    }
}