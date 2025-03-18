using System;
using System.Collections.Generic;

namespace Units.Player.Spells
{
    public class SpellHolder
    {
        public List<Spell> Spells { get; private set; }
        public bool IsSpellInHolder(Spell spell) => Spells.Contains(spell);
        public Action SpellChanged;


        public void AddSpell(Spell spell)
        {
            if (IsSpellInHolder(spell))
                return;

            Spells.Add(spell);
            SpellChanged?.Invoke();
        }

        public void RemoveSpell(Spell spell)
        {
            if (IsSpellInHolder(spell))
                return;

            Spells.Remove(spell);
            SpellChanged?.Invoke();
        }
    }
}