using System;
using ControlerBuddy.Database;
using EloBuddy;
using EloBuddy.SDK;

namespace ControlerBuddy
{
    public class SpellCasting
    {
        public static ISpellDatabase SpellDatabase { get; private set; }
        public static Spell.SpellBase Q { get { return SpellDatabase.QSpell; } }
        public static Spell.SpellBase W { get { return SpellDatabase.WSpell; } }
        public static Spell.SpellBase E { get { return SpellDatabase.ESpell; } }
        public static Spell.SpellBase R { get { return SpellDatabase.RSpell; } }
        public static bool SpellsLoaded { get; set; }
        public static void Initialize()
        {
            
        }


        public static void Initialize(bool spellsLoaded)
        {
            if(!spellsLoaded) throw new ArgumentException("Spells Have not been loaded, not initializing SpellCasting.");
            SpellsLoaded = true;
        }

        public static bool LoadSpells(Champion hero)
        {
            var type = Type.GetType("ControlerBuddy.Database" + hero);
            if(type == null) throw new NotSupportedException("Champion: " + hero + " Not Supported Yet!");
            var instance = (ISpellDatabase) Activator.CreateInstance(type);
            if(instance == null) throw new NullReferenceException("Champion type found, but could not create an instance.");
            SpellDatabase = instance;
            return true;
        }
    }
}