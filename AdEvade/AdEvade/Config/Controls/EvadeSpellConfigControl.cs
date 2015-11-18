using AdEvade.Data.EvadeSpells;
using EloBuddy.SDK.Menu;

namespace AdEvade.Config.Controls
{
    public class EvadeSpellConfigControl
    {
        public DynamicCheckBox UseSpellCheckBox;
        public StringSlider DangerLevelSlider;
        public StringSlider SpellModeSlider;

        public static readonly string[] SpellModes = {"Undodgeable", "Activation Time", "Always"};

        private readonly Menu _menu;
        public EvadeSpellConfigControl(Menu menu, string menuName, EvadeSpellData spell )
        {

            UseSpellCheckBox = new DynamicCheckBox(ConfigDataType.EvadeSpell, spell.Name, "Use Spell", true, true, SpellConfigProperty.UseEvadeSpell);
            DangerLevelSlider = new StringSlider(ConfigDataType.EvadeSpell, spell.Name, "Danger Level", (int) spell.Dangerlevel, SpellConfigProperty.DangerLevel, SpellConfigControl.DangerLevels);
            SpellModeSlider = new StringSlider(ConfigDataType.EvadeSpell, spell.Name, "Spell Mode", (int)EvadeSpell.GetDefaultSpellMode(spell), SpellConfigProperty.SpellMode, SpellModes);
            menu.AddGroupLabel(menuName);
            menu.Add(spell.Name + "UseEvadeSpell", UseSpellCheckBox.CheckBox);
            menu.Add(spell.Name + "EvadeSpellDangerLevel", DangerLevelSlider.Slider.Slider);
            menu.Add(spell.Name + "EvadeSpellMode", SpellModeSlider.Slider.Slider);
            Properties.SetEvadeSpell(spell.Name, new EvadeSpellConfig { DangerLevel = spell.Dangerlevel, Use = true, SpellMode = EvadeSpell.GetDefaultSpellMode(spell) });
        }

        public Menu GetMenu()
        {
            return _menu;
        }
    }
}