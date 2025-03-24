using System;
using UnityEngine;

namespace Units.Input
{
    public interface IUnitInput
    {
        public Vector2 MoveDirection {get; set;}
        public float Rotation {get; set;}
        public Action ShotStarted { get; set; }
        public Action ShotCanceled{ get; set; }
        public Action Spell1Started{ get; set; }
        public Action Spell1Canceled{ get; set; }
        public Action RunStarted{ get; set; }
        public Action RunCanceled{ get; set; }
    }
}