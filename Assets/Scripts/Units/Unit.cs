using System;
using DamageSystem;
using Units.Health;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        private void Awake()
        {
            Initialized();
        }

        protected virtual void Initialized()
        {
        }
        
    }
}
