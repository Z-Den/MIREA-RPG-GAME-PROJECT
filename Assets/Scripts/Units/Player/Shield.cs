using System;
using UnityEngine;

namespace Units.Player
{
    [Serializable]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _shield;
        [SerializeField] private float _maxShieldSize;
        [SerializeField] private float _sizeChangeSpeed;
        private float _currentSize;
        private float _goatSize;
        
        public void OnEnable()
        {
            _player.PlayerInput.ShieldStarted += ShieldStarted ;
            _player.PlayerInput.ShieldCanceled += ShieldCanceled;
        }

        private void OnDisable()
        {
            _player.PlayerInput.ShieldStarted -= ShieldStarted ;
            _player.PlayerInput.ShieldCanceled -= ShieldCanceled;
        }

        private void ShieldStarted()
        {
            _goatSize = _maxShieldSize;
        }

        private void Update()
        {
            _currentSize = Mathf.Lerp(_currentSize, _goatSize, Time.deltaTime * _sizeChangeSpeed);
            _shield.transform.localScale = Vector3.one * _currentSize; 
        }

        private void ShieldCanceled()
        {
            _goatSize = 0f;
        }
    }
}