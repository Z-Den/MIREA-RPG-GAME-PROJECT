using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private bool _isLookAtCamera;
        private Camera _camera;
        private Unit _unit;
        
        public virtual void Init(Unit unit)
        {
            _camera = Camera.main;
            _unit = unit;
            unit.Health.DamageApplied += DamageApplied;
            DamageApplied(0);
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

        private void DamageApplied(int obj)
        {
            if (_healthBar != null)
                UpdateHealthBar(_unit.Health.FillValue);
        }

        private void UpdateHealthBar(float fillValue)
        {
            _healthBar.fillAmount = fillValue;
        }
    }
}