using System;
using Units.Health;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UnitHealth _playerHealth;
        [SerializeField] private GameObject _deadPanel;

        private void Awake()
        {
            _deadPanel.SetActive(false);
            _playerHealth.OnDeath += OnDeath;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDeath()
        {
            _deadPanel.SetActive(true);
        }
    }
}
