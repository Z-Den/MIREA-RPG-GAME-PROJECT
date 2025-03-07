using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        private Camera _camera;
        private Unit _unit;
        
        public void Init(Unit unit)
        {
            _camera = Camera.main;
            _unit = unit;
            unit.Health.DamageApplied += DamageApplied;
            UpdateHealthBar(1);
        }

        private void Update()
        {
            LookAtCamera();
        }

        private void LookAtCamera()
        {
            transform.LookAt(_camera.transform);
        }

        private void DamageApplied(int obj)
        {
            UpdateHealthBar(_unit.Health.FillValue);
        }

        private void UpdateHealthBar(float fillValue)
        {
            _healthBar.fillAmount = fillValue;
        }
    }
}