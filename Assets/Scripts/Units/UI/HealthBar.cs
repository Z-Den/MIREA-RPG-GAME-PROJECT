using System.Linq;
using DamageSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Units.UI
{
    public class HealthBar : TwoSideBar
    {
        private const string ResistanceIconPath = "Sprites/Resistant/";
        private const string ImmunityIconPath = "Sprites/Immune/";
        
        [Header("Resistances")]
        [SerializeField] private Transform _iconContainer;
        [SerializeField] private Image _iconPrefab;

        public void ResistanceIconsUpdate(DamageType[] immunities, DamageType[] resistances)
        {
            ClearContainer(_iconContainer); 
            var immunityWithoutDuplicates = immunities.GroupBy(x => x).Select(y => y.First());
            foreach (var immunity in immunityWithoutDuplicates)
            {
                var inst = Instantiate(_iconPrefab, _iconContainer);
                var sprite = Resources.Load<Sprite>(ImmunityIconPath + immunity);
                inst.sprite = sprite;
            }
           
            var resistanceWithoutDuplicates = resistances.GroupBy(x => x).Select(y => y.First());
            resistanceWithoutDuplicates = resistanceWithoutDuplicates.Except(immunityWithoutDuplicates);
            foreach (var resistance in resistanceWithoutDuplicates)
            {
                var inst = Instantiate(_iconPrefab, _iconContainer);
                var sprite = Resources.Load<Sprite>(ResistanceIconPath + resistance);
                inst.sprite = sprite;
            }
        }

        private void ClearContainer(Transform container)
        {
            foreach(Transform child in container)
            {
                Destroy(child.gameObject);
            }
        }
    }
}