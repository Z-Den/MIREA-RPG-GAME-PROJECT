using System.Collections.Generic;
using PivotConnection;
using Units;
using Units.Enemy;
using Units.Input;
using Units.UI;
using UnityEngine;

namespace Bootstrapper
{
    public class EnemyBootstrapper : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private List<GameObject> _playerItemPrefabs;
        private Enemy _unit;
        private UnitUI _unitUI;

        public void InstantiateUnit(Enemy prefab, Transform spawnPoint)
        {
            _unit = InstantiatePrefab(prefab, spawnPoint);
            _unitUI = _unit.UnitUI;
            ConfigureDependenciesInComponents();
            
            var items = new List<GameObject>(); 
            foreach (var itemPrefab in _playerItemPrefabs)
                items.Add(Instantiate(itemPrefab));
            
            ConfigureDependencies(items);
        }
        
        private T InstantiatePrefab<T>(T prefab, Transform spawnPoint) where T : Object
        {
            return Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }

        private void ConfigureDependenciesInComponents()
        {
            var input = _unit.StateMachine as IUnitInput;
            _unit.gameObject.TryGetComponent(out IPivot pivot);
            
            foreach (var component in _unit.gameObject.GetComponents<IUnitActionController>())
                component.SetInput(input);

            foreach (var component in _unit.gameObject.GetComponents<IPivotFollower>())
                component.SetPivot(pivot);
            
            foreach (var component in _unit.gameObject.GetComponents<IUIElementHolder>())
                _unitUI.Add(component.GetUIElement());
        }
        
        private void ConfigureDependencies(List<GameObject> connectedObjects)
        {
            _unit.gameObject.TryGetComponent(out IUnitInput input);
            _unit.gameObject.TryGetComponent(out IPivot pivot);
            foreach (var connectedObject in connectedObjects)
            {
                foreach (var component in connectedObject.GetComponents<IUnitActionController>())
                {
                    component.SetInput(input);
                }

                foreach (var component in connectedObject.GetComponents<IPivotFollower>())
                {
                    component.SetPivot(pivot);
                }

                foreach (var component in connectedObject.GetComponents<IUIElementHolder>())
                {
                    var element = component.GetUIElement();
                    if (element != null)
                        _unitUI.Add(element);
                }
            }
        }
    }
}