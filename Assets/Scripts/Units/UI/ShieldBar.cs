using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units.UI
{
    public class ShieldBar : UIElement
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private Image[] _otherElements;
        private readonly float _noInteractTime = 3f;
        private readonly float _alphaSpeed = 0.5f;
        private float _timer = 0;
        private float _alpha = 1;
        
        public void FillBar(float current, float max)
        {
            foreach (var image in _images)
            {
                image.fillAmount = current / max;   
            }
            _timer = 0;
        }

        private void Update()
        {
            if (_timer < _noInteractTime)
            {
                _timer += Time.deltaTime;
                _alpha += _alphaSpeed * Time.deltaTime;
            }
            else if (_alpha > 0)
            {
                _alpha -= Time.deltaTime * _alphaSpeed;
            }
            
            _alpha = Mathf.Clamp(_alpha, 0, 1);
            
            foreach (var image in _images)
                image.color = new Color(1,1, 1, _alpha);

            foreach (var image in _otherElements)
                image.color = new Color(1, 1, 1, _alpha);
        }
    }
}