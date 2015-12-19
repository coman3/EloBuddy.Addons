using System;
using System.Collections.Generic;
using AdEvade.Config;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AdEvade.Config
{

    public abstract class ConfigPreset
    {
        public Dictionary<ConfigValue, object> Values;
        public ConfigPluginAttribute GetAttribute()
        {
            // Get instance of the attribute.
            ConfigPluginAttribute myAttribute = (ConfigPluginAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ConfigPluginAttribute));
            if (myAttribute == null)
                throw new Exception("Attribute Not Found!");
            return myAttribute;
        }

        public virtual void SetValue(ConfigValue key, object value, bool raiseEvent = true)
        {
            if((key.IsInt() && (value is int || value is float)) || (key.IsBool() && value is bool))
                if (Values.ContainsKey(key))
                    Values[key] = value;
                else
                    Values.Add(key, value);
            if(raiseEvent) Properties.OnValueChanged(key, value);
        }
        public virtual void LoadMenu(Menu menu) { }
        public virtual void InitiateConfig(ref Dictionary<ConfigValue, object> values)
        {
            values.Clear();
            //Integers
            values.Add(ConfigValue.ExtraDetectionRange, 0);
            values.Add(ConfigValue.ExtraLineWidth, 0);
            values.Add(ConfigValue.ExtraSpellRadius, 0); //ExtraAvoidDistance
            values.Add(ConfigValue.ExtraPingBuffer, 0);
            values.Add(ConfigValue.ExtraCpaDistance, 0);
            values.Add(ConfigValue.RejectMinDistance, 0);
            values.Add(ConfigValue.MinimumComfortZone, 0);
            values.Add(ConfigValue.ExtraEvadeDistance, 0);
            values.Add(ConfigValue.EvadeMode, EvadeMode.Smooth);
            values.Add(ConfigValue.FastEvadeActivationTime, 0);
            values.Add(ConfigValue.SpellActivationTime, 0);
            values.Add(ConfigValue.TickLimiter, 0);
            values.Add(ConfigValue.SpellDetectionTime, 0);
            values.Add(ConfigValue.LowDangerDrawWidth, 1);
            values.Add(ConfigValue.NormalDangerDrawWidth, 2);
            values.Add(ConfigValue.HighDangerDrawWidth, 3);
            values.Add(ConfigValue.ExtremeDangerDrawWidth, 4);
            values.Add(ConfigValue.ReactionTime, 0);
            values.Add(ConfigValue.DodgeInterval, 0);

            //Booleans
            values.Add(ConfigValue.DodgeSkillShots, true);
            values.Add(ConfigValue.DrawSkillShots, true);
            values.Add(ConfigValue.PreventDodgingNearEnemy, false);
            values.Add(ConfigValue.CalculateWindupDelay, false);
            values.Add(ConfigValue.HighPrecision, false);
            values.Add(ConfigValue.PreventDodgingUnderTower, false);
            values.Add(ConfigValue.ClickOnlyOnce, false);
            values.Add(ConfigValue.OnlyDodgeDangerous, false);
            values.Add(ConfigValue.DodgeDangerousKeysEnabled, false);
            values.Add(ConfigValue.DodgeFowSpells, false);
            values.Add(ConfigValue.DodgeCircularSpells, false);
            values.Add(ConfigValue.ActivateEvadeSpells, false);
            values.Add(ConfigValue.DodgeDangerousKey1, false);
            values.Add(ConfigValue.DodgeDangerousKey2, false);
            values.Add(ConfigValue.RecalculatePath, false);
            values.Add(ConfigValue.ContinueMovement, false);
            values.Add(ConfigValue.CheckSpellCollision, false);
            values.Add(ConfigValue.AdvancedSpellDetection, false);
            values.Add(ConfigValue.FastMovementBlock, false);
            values.Add(ConfigValue.EnableEvadeDistance, false);
            values.Add(ConfigValue.ShowDebugInfo, false);
            values.Add(ConfigValue.EnableSpellTester, false);
            values.Add(ConfigValue.DrawEvadeStatus, false);
            values.Add(ConfigValue.DrawSpellPosition, false);
            values.Add(ConfigValue.DrawEvadePosition, false);
            values.Add(ConfigValue.AutoSetPing, false);

            Values = values;
        }
        public abstract void LoadConfig();
        public virtual void UnLoadConfig() { }
        /// <summary>
        /// Gets an <see cref="int"/> from the config database.
        /// </summary>
        /// <param name="value">The config value wanted from the database. (Must be a value less than 255)</param>
        /// <returns></returns>
        public virtual int GetInt(ConfigValue value)
        {
            if ((short)value > Constants.ValueSeperator)
                throw new ArgumentOutOfRangeException(
                    "ConfigValue is not within the range of integer options! (< " + Constants.ValueSeperator + ", value: " + (short)value + ":" +
                    value + ")");
            if (!Values.ContainsKey(value))
                throw new ArgumentException("Value not found within database! (value: "
                                            + (short)value + ":" + value + ")");
            return (int)Values[value];

        }
        /// <summary>
        /// Gets a <see cref="bool"/> from the config database.
        /// </summary>
        /// <param name="value">The config value wanted from the database. (Must be a value less than 255)</param>
        /// <returns></returns>
        public virtual bool GetBoolean(ConfigValue value)
        {
            if ((short)value < Constants.ValueSeperator)
                throw new ArgumentOutOfRangeException(
                    "ConfigValue is not within the range of boolean options! (> " + Constants.ValueSeperator + ", value: " + (short)value + ":" +
                    value + ")");
            if (!Values.ContainsKey(value))
                throw new ArgumentException("Value not found within database! (value: "
                                            + (short)value + ":" + value + ")");
            return (bool)Values[value];

        }
        /// <summary>
        /// Draws the following for Constants.DrawChangeLength
        /// </summary>
        public virtual void DrawOnEnabled() { }
        /// <summary>
        /// Draws the following while the preset is enabled
        /// </summary>
        public virtual void Draw() { }
    }

    public class ConfigPluginAttribute : Attribute
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string[] RecomendedChampions { get; set; }
    }
}