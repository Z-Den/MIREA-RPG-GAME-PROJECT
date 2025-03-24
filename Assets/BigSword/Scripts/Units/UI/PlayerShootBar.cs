using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units.UI
{
    public class PlayerShootBar : UIElement
    {
        [Header("Cooldown")]
        [SerializeField] private Image _shotIcon;
        [SerializeField] private TMP_Text _cooldownText;
        [Header("Charge")]
        [SerializeField] private Image _chargeIcon;
        [SerializeField] private TMP_Text _chargeText;
        
        public void ChargeTimerChanged(float current, float max)
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

        public void CooldownChanged(float current, float max)
        {
            _shotIcon.fillAmount = current / max;
            _cooldownText.text = current == 0? "LMB" : current.ToString("0");
        }
    }
}