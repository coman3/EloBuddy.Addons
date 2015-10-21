using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace DrawingBuddy
{
    public class HeroSpellCheckBoxConfig
    {
        public CheckBox CheckBoxQ { get; set; }
        public CheckBox CheckBoxW { get; set; }
        public CheckBox CheckBoxE { get; set; }
        public CheckBox CheckBoxR { get; set; }
        public AIHeroClient Hero { get; private set; }
        public Guid Id { get; private set; }
        private string _checkboxPrefix;
        public HeroSpellCheckBoxConfig(Menu menu, AIHeroClient hero, string checkboxPrefix = "Display")
        {
            Hero = hero;
            Id = Guid.NewGuid();
            _checkboxPrefix = checkboxPrefix;
            CheckBoxQ = new CheckBox(_checkboxPrefix + " " + hero.Spellbook.GetSpell(SpellSlot.Q).Name);
            CheckBoxW = new CheckBox(_checkboxPrefix + " " + hero.Spellbook.GetSpell(SpellSlot.W).Name);
            CheckBoxE = new CheckBox(_checkboxPrefix + " " + hero.Spellbook.GetSpell(SpellSlot.E).Name);
            CheckBoxR = new CheckBox(_checkboxPrefix + " " + hero.Spellbook.GetSpell(SpellSlot.R).Name);

            menu.Add(Id + "Q", CheckBoxQ);
            menu.Add(Id + "W", CheckBoxW);
            menu.Add(Id + "E", CheckBoxE);
            menu.Add(Id + "R", CheckBoxR);
        }

        public bool GetChecked(SpellSlot slot)
        {
            switch (slot)
            {
                case SpellSlot.Q:
                    return CheckBoxQ.CurrentValue;
                case SpellSlot.W:
                    return CheckBoxW.CurrentValue;
                case SpellSlot.E:
                    return CheckBoxE.CurrentValue;
                case SpellSlot.R:
                    return CheckBoxR.CurrentValue;
            }
            return true;
        }
    }
}