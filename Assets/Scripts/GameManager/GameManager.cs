using System;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerUI _playerUI;

        private void Start()
        {
            _playerUI.SetDeadPanelVisible(false);
            _player.Health.OnDeath += OnDeath;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDeath()
        {
            _playerUI.SetDeadPanelVisible(true);
        }
    }
}
