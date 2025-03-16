using UnityEngine;

namespace Units.Player
{
    public class PlayerUI : UnitUI
    {
        [SerializeField] private GameObject _deadPanel;

        public void SetDeadPanelVisible(bool isVisible)
        {
            _deadPanel.SetActive(isVisible);
        }
    }
}