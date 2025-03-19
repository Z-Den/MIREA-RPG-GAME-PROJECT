using UnityEngine;

namespace Units.UI
{
    public interface IUIElementHolder
    {
        public UnitUI UI {get;}
        
        public void SetUIElement();
        public void RemoveUIElement();
    }
}