using System;
using UnityEngine;

namespace Units.Player.Weapon
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponMover : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPivot;

        private void Update()
        {
            transform.position = _weaponPivot.position;
            transform.rotation = _weaponPivot.rotation;
        }
    }
}
