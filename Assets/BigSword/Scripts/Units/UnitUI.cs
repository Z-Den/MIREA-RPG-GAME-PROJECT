using System;
using Units.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Units
{
    public class UnitUI : MonoBehaviour
    {   
        [SerializeField] private bool _isLookAtCamera;
        private Camera _camera;

        public void Add(UIElement uiElement)
        {
            uiElement.transform.SetParent(transform);
            var rect = uiElement.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
        }
        
        protected virtual void Start()
        {
            _camera = Camera.main;
        }

        protected virtual void OnDisable()
        {
        }

        private void Update()
        {
            if (_isLookAtCamera)
                transform.LookAt(_camera.transform);
        }
    }
}