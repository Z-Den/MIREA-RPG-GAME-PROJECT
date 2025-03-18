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
        [SerializeField] private Image _shotIcon;
        [SerializeField] private TMP_Text _cooldownText;
        private Weapon.Weapon _weapon;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            var player = unit as Player;
            if (player == null)
                return;

            _weapon = player.Weapon;
            _weapon.CooldownChanged += CooldownChanged;
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