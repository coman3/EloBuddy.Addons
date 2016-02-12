using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ControlerBuddy.Database
{
    public interface ISpellDatabase
    {
        Spell.SpellBase QSpell { get; set; }
        Spell.SpellBase WSpell { get; set; }
        Spell.SpellBase ESpell { get; set; }
        Spell.SpellBase RSpell { get; set; }
    }

    public class Lux : ISpellDatabase
    {
        public Lux()
        {
            QSpell = new Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 65);
            WSpell = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 0, 1400, 85);
            ESpell = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Circular, 250, 1300, 330);
            RSpell = new Spell.Skillshot(SpellSlot.R, 3290, SkillShotType.Circular, 500, int.MaxValue, 160);
        }

        public Spell.SpellBase QSpell { get; set; }
        public Spell.SpellBase WSpell { get; set; }
        public Spell.SpellBase ESpell { get; set; }
        public Spell.SpellBase RSpell { get; set; }
    }
}
    