using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Player
{
    public class PlayerUI : UnitUI
    {
        [SerializeField] private GameObject _deadPanel;
        [Header("Cooldown")]
        [SerializeField] private Image _shotIcon;
        [SerializeField] private TMP_Text _cooldownText;
        [Header("Charge")]
        [SerializeField] private Image _chargeIcon;
        [SerializeField] private TMP_Text _chargeText;
        [SerializeField] private Weapon.Weapon _weapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_weapon == null)
                return;
            
            _weapon.CooldownChanged += CooldownChanged;
            _weapon.ChargeTimerChanged += ChargeTimerChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_weapon == null)
                return;
            
            _weapon.CooldownChanged += CooldownChanged;
            _weapon.ChargeTimerChanged += ChargeTimerChanged;
        }

        private void ChargeTimerChanged(float current, float max)
        {
            if (current == 1)
            {
                _chargeText.text = "";
                _chargeIcon.fillAmount = 0;
            }
            else
            {
                var fill = (max - current) / max;
                _chargeText.text = (fill).ToString("0%");
                _chargeIcon.fillAmount = fill;
            }
        }

        private void CooldownChanged(float current, float max)
        {
            _shotIcon.fillAmount = current / max;
            _cooldownText.text = current == 0? "LMB" : current.ToString("0");
        }

        public void SetDeadPanelVisible(bool isVisible)
        {
            _deadPanel.SetActive(isVisible);
        }
    }
}