using UnityEngine;

namespace Units.Player.Spells
{
    [CreateAssetMenu(menuName = "Config/Spell/TestSpell")]
    public class Spell : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
    }
}