
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

        public void UpdateBar(float current, float max)
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

        protected void FillBar(float current, float max)
        {
            if (_bar == null)
                return;
            
            _bar.fillAmount = current / max;
        }
    }
}