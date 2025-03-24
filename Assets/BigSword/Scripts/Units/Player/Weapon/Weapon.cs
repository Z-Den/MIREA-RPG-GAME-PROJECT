using System;
using System.Collections;
using DamageSystem;
using PivotConnection;
using Units.Input;
using Units.UI;
using UnityEngine;

namespace Units.Player.Weapon
{
    public class Weapon : MonoBehaviour,IUnitActionController, IUIElementHolder, IPivotFollower
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private LayerMask _damageMask;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _recoilPower = 1000;
        [SerializeField] private float _shotCooldown = 30f;
        [SerializeField] private float _shotChargeTime = 30f;
        [SerializeField] private PhysicalMover _physicalMover;
        [SerializeField] private TrailRenderer _trailPrefab;
        [SerializeField] private float _trailSpeed = 20f; 
        [SerializeField] private float _shootDistance;
        [Header("UI")]
        [SerializeField] private PlayerShootBar _shootBarPrefab;
        [SerializeField] private Bar _swordRunesBar;
        private float _cooldown;
        private float _shotChargeTimer;

        private bool IsCanShoot => _cooldown <= 0;
        
        public Action<float, float> CooldownChanged;
        public Action<float, float> ChargeTimerChanged;
        private Transform _pivotTransform;
        private IUnitInput _inputActions;

        Transform IPivotFollower.PivotTransform
        {
            get => _pivotTransform;
            set => _pivotTransform = value;
        }
        
        private void Start()
        {   
            _inputActions.ShotStarted += StartChargeShot;
            _inputActions.ShotCanceled += Shot;
            CooldownChanged?.Invoke(0, 1);
            ChargeTimerChanged?.Invoke(1, 1);
        }

        private void OnDisable()
        {
            _inputActions.ShotStarted -= StartChargeShot;
            _inputActions.ShotCanceled -= Shot;
        }

        private void StartChargeShot()
        {
            if (!IsCanShoot)
                return;
            
            _shotChargeTimer = _shotChargeTime;
        }

        private void Update()
        {
            UpdateTimer(ref _cooldown, ref _shotCooldown, ref CooldownChanged);
            UpdateTimer(ref _shotChargeTimer, ref _shotChargeTime, ref ChargeTimerChanged);
            
            if (!_pivotTransform)
                return;
            
            _physicalMover.SetMoveDirection((_pivotTransform.position + transform.forward * _offset.z) - transform.position);
            _physicalMover.SetRotation(_pivotTransform.rotation);
        }

        private void UpdateTimer(ref float timer, ref float cooldown, ref Action<float, float> callback)
        {
            if (timer <= 0) return;
            
            timer -= Time.deltaTime;
            timer = timer < 0 ? 0 : timer;
            callback?.Invoke(timer, cooldown);
        }
        
        private void Shot()
        {
            if (!IsCanShoot)
                return;

            var shootDistance = _shootDistance;
            
            var raycastHits = Physics.BoxCastAll(transform.position, Vector3.one,
                transform.forward, Quaternion.identity, shootDistance, _damageMask);
            foreach (var hit in raycastHits)
            {
                if (!hit.collider.TryGetComponent(out IDamageable damageable)) continue;
                
                var damage = new Damage(30, DamageType.Magical, _damageMask);
                damageable.ApplyDamage(damage);
            }
            _rigidbody.AddForce(-transform.forward * _recoilPower);
            _cooldown = _shotCooldown;
            _shotChargeTimer = 0f;
            ChargeTimerChanged?.Invoke(1, 1);
            StartCoroutine(SpawnTrail());
        }

        private IEnumerator SpawnTrail()
        {
            var timer = 0f;
            var trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
            var startPosition = transform.position;
            var endPosition = startPosition + transform.forward * _shootDistance;

            while (timer < (_shootDistance / _trailSpeed))
            {
                timer += Time.deltaTime;
                trail.transform.position = Vector3.Lerp(startPosition, endPosition, timer / (_shootDistance / _trailSpeed));
                yield return null;
            }
            Destroy(trail.gameObject);
        }

        public UIElement GetUIElement()
        {
            var shotBar = Instantiate(_shootBarPrefab);
            CooldownChanged += shotBar.CooldownChanged;
            ChargeTimerChanged += shotBar.ChargeTimerChanged;
            ChargeTimerChanged += _swordRunesBar.UpdateBar;
            return shotBar;
        }

        IUnitInput IUnitActionController.InputActions
        {
            get => _inputActions;
            set => _inputActions = value;
        }
    }
}