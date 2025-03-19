using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Units
{
    public class UnitUI : MonoBehaviour
    {   
        [SerializeField] protected Unit Unit;
        
        [SerializeField] private Image _healthBar;
        [SerializeField] private bool _isLookAtCamera;
        private Camera _camera;
        
        protected virtual void OnEnable()
        {
            _camera = Camera.main;
            Unit.Health.HealthChanged += DamageApplied;
            DamageApplied(1, 1);
        }

        protected virtual void OnDisable()
        {
            Unit.Health.HealthChanged -= DamageApplied;
        }

        private void Update()
        {
            if (_isLookAtCamera)
                LookAtCamera();
        }

        private void LookAtCamera()
        {
            transform.LookAt(_camera.transform);
        }

        private void DamageApplied(int currentHealth, int maxHealth)
        {
            if (_healthBar != null && Unit != null)
                UpdateHealthBar((float)currentHealth / maxHealth);
        }

        private void UpdateHealthBar(float fillValue)
        {
            _healthBar.fillAmount = fillValue;
        }
    }
}