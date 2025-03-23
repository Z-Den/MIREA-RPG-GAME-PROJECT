using UnityEngine;

namespace PivotConnection
{
    public interface IPivotFollower
    {
        protected Transform PivotTransform { get; set; }
        
        public void SetPivot(IPivot pivot) => PivotTransform = pivot.PivotTransform;
    }
}