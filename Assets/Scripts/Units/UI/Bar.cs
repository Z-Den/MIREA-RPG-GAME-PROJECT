
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Units.UI
{
    public class Bar : UIElement
    {
        [SerializeField] private Image _bar;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private bool _invertedBarFill;
        
        public virtual void UpdateBar(float current, float max)
        {
            ChangeText(current, max);
            FillBar(current, max);
        }
        
        protected virtual void ChangeText(float current, float max)
        {
            if (_value == null)
                return;
            
            _value.text = ((int)current) + " / " + ((int)max);
        }

        protected virtual void FillBar(float current, float max)
        {
            if (_bar == null)
                return;

            if (_invertedBarFill)
                _bar.fillAmount = (max - current) / max;
            else
                _bar.fillAmount = current / max;
        }
    }
}