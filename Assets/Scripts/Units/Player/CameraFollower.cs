using System;
using UnityEngine;

namespace Units.Player
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        private void Update()
        {
            if (_pivot == null)
                return;
            
            transform.localPosition = _pivot.transform.position;
            transform.rotation = _pivot.transform.rotation;
        }
    }
}