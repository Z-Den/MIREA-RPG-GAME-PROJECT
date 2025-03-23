using System.Collections.Generic;
using PivotConnection;
using Units;
using Units.Input;
using Units.Player;
using Units.UI;
using UnityEngine;

namespace Bootstrapper
{
    public class PlayerBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [Header("Prefabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private UnitUI _playerUIPrefab;
        [SerializeField] private List<GameObject> _playerItemPrefabs;
        private Player _player;
        private UnitUI _playerUI;
        
        private void Awake()
        {
            _player = InstantiatePrefab(_playerPrefab);
            _playerUI = InstantiatePrefab(_playerUIPrefab);
            ConfigureDependenciesInPlayerComponents();
            
            var items = new List<GameObject>(); 
            foreach (var itemPrefab in _playerItemPrefabs)
                items.Add(InstantiatePrefab(itemPrefab));
            
            ConfigureDependencies(items);
        }

        private T InstantiatePrefab<T>(T prefab) where T : Object
        {
            return Instantiate(prefab, _playerSpawnPoint.position, Quaternion.identity);
        }

        private void ConfigureDependenciesInPlayerComponents()
        {
            _player.gameObject.TryGetComponent(out IUnitInput input);
            _player.gameObject.TryGetComponent(out IPivot pivot);
            
            foreach (var component in _player.gameObject.GetComponents<IUnitActionController>())
                component.SetInput(input);

            foreach (var component in _player.gameObject.GetComponents<IPivotFollower>())
                component.SetPivot(pivot);
            
            foreach (var component in _player.gameObject.GetComponents<IUIElementHolder>())
                _playerUI.Add(component.GetUIElement());
        }
        
        private void ConfigureDependencies(List<GameObject> connectedObjects)
        {
            _player.gameObject.TryGetComponent(out IUnitInput input);
            _player.gameObject.TryGetComponent(out IPivot pivot);
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
                        _playerUI.Add(element);
                }
            }
        }
    }
}