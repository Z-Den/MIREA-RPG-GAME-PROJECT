using System;
using UnityEngine;

namespace Units.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        private Quaternion _targetRotation; 
        
        
        private void Update()
        {
            //_targetRotation = Quaternion.Lerp(_targetRotation, Quaternion.LookRotation(Direction), Time.deltaTime * _parameters.RotationSpeed);
        }
    }
}
