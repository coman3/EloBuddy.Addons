using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EzEvade.EvadeSpells;

namespace EzEvade.Config
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
            _menu = menu.AddSubMenu(menuName, spell.CharName + spell.Name + "EvadeSpellSettings");

            UseSpellCheckBox = new DynamicCheckBox(ConfigDataType.EvadeSpell, spell.Name, "Use Spell", true, true, SpellConfigProperty.UseEvadeSpell);
            DangerLevelSlider = new StringSlider(ConfigDataType.EvadeSpell, spell.Name, "Danger Level", spell.Dangerlevel - 1, SpellConfigProperty.DangerLevel, SpellConfigControl.DangerLevels);
            SpellModeSlider = new StringSlider(ConfigDataType.EvadeSpell, spell.Name, "Spell Mode", EvadeSpell.GetDefaultSpellMode(spell), SpellConfigProperty.SpellMode, SpellModes);

            _menu.Add(spell.Name + "UseEvadeSpell", UseSpellCheckBox.CheckBox);
            _menu.Add(spell.Name + "EvadeSpellDangerLevel", DangerLevelSlider.Slider.Slider);
            _menu.Add(spell.Name + "EvadeSpellMode", SpellModeSlider.Slider.Slider);
            Config.SetEvadeSpell(spell.Name, new EvadeSpellConfig { DangerLevel = spell.Dangerlevel, Use = true, SpellMode = EvadeSpell.GetDefaultSpellMode(spell) });
        }

        public Menu GetMenu()
        {
            return _menu;
        }
    }
}