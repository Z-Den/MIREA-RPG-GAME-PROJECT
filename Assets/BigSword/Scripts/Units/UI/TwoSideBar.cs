using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Units.UI
{
    public class TwoSideBar : UIElement
    {
        [SerializeField] private Color _frontColor = Color.white;
        [SerializeField] private Color _backgroundColor = Color.white;
        [SerializeField] private Image[] _images;
        [SerializeField] private Image[] _otherElements;
        [SerializeField] private bool _isInvisible;
        private readonly float _noInteractTime = 3f;
        private readonly float _alphaSpeed = 0.5f;
        private float _timer = 0;
        private float _alpha = 1;

        private void OnValidate()
        {
            SetColorFor(_images, _frontColor, 1);
            SetColorFor(_otherElements, _backgroundColor, 1);
        }

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
            if (!_isInvisible)
                return;
            
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
            SetColorFor(_images, _frontColor, _alpha);
            SetColorFor(_otherElements, _backgroundColor, _alpha);
        }

        private void SetColorFor( Image[] images, Color baseColor, float alpha = 1)
        {
            var color = baseColor;
            color.a = alpha;
            foreach (var image in images)
                image.color = color;
        }
    }
}