using System;
using System.Collections.Generic;
using System.Linq;
using AdEvade.Config.Controls;
using AdEvade.Data;
using AdEvade.Data.Spells;
using AdEvade.Draw;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SpellData = AdEvade.Data.Spells.SpellData;

namespace AdEvade.Config
{
    public enum ConfigDataType
    {
        Data, Spells, KeyBind, EvadeSpell
    }

    public class ConfigValueChangedArgs : EventArgs
    {
        public ConfigValue Key { get; set; }
        public string SpellKey { get; set; }
        public readonly bool IsSpell;
        public object Value { get; set; }

        public ConfigValueChangedArgs(ConfigValue key, object value)
        {
            Key = key;
            Value = value;
        }
        public ConfigValueChangedArgs(string key, object value)
        {
            SpellKey = key;
            IsSpell = true;
            Value = value;
        }
    }
    public static class Properties
    {
        public delegate void ConfigValueChangedHandler(ConfigValueChangedArgs args);
        public static event ConfigValueChangedHandler OnConfigValueChanged;

        public static Dictionary<ConfigValue, object> Values = new Dictionary<ConfigValue, object>();
        public static readonly Dictionary<string, SpellConfig> Spells = new Dictionary<string, SpellConfig>();

        public static readonly Dictionary<string, EvadeSpellConfig> EvadeSpells = new Dictionary<string, EvadeSpellConfig>();
        public static SpellConfig GetSpellConfig(this SpellData spell, SpellConfigControl control)
        {
            return new SpellConfig
            {
                DangerLevel = spell.Dangerlevel,
                Dodge = true,
                Draw = true,
                Radius = spell.Radius,
                PlayerName = spell.CharName
            };
        }

        //public static T GetData<T>(string key)
        //{
        //    if (Data.Any(i => i.Key == key))
        //    {
        //        if(Data[key] is T)
        //            return (T) Data[key];
        //        else 
        //            Debug.DrawTopLeft("Tried To Access key with wrong type: " + key);
        //    }
        //    return default(T);
        //}
        public static void OnValueChanged(ConfigValue key, object value)
        {
            if(OnConfigValueChanged != null) OnConfigValueChanged.Invoke(new ConfigValueChangedArgs(key, value));
        }
        public static SpellConfig GetSpell(string key)
        {
            if (Spells.Any(i => i.Key == key))
            {
                //Debug.DrawTopLeft("Found Spell at key: " + key + " = " + Spells[key]);
                return Spells[key];
            }
            if (ConfigValue.EnableSpellTester.GetBool())
                if (SpellDatabase.Spells.Any(x => x.SpellName == key))
                {
                    var spellfromdb = SpellDatabase.Spells.First(x => x.SpellName == key);
                    return new SpellConfig { DangerLevel = spellfromdb.Dangerlevel, Radius = spellfromdb.Radius, Dodge = true, Draw = true, EvadeSpellMode = SpellModes.Always};
                }
            Debug.DrawTopLeft("*Spell: " + key + " Not Found, Returning: DO NOT DODGE");
            return new SpellConfig { DangerLevel = SpellDangerLevel.Normal, Dodge = false, Draw = true, EvadeSpellMode = SpellModes.Undodgeable, Radius = 20 };

        }
        public static EvadeSpellConfig GetEvadeSpell(string key)
        {
            if (EvadeSpells.Any(i => i.Key == key))
            {
                return EvadeSpells[key];
            }
            Debug.DrawTopLeft(" * Evade Spell: " + key + " Not Found, Returning: DO NOT USE");
            return new EvadeSpellConfig { DangerLevel = SpellDangerLevel.Low, SpellMode = SpellModes.Undodgeable, Use = false};
        }

        public static int GetInt(ConfigValue key)
        {
            return ConfigPluginControler.SelectedPreset.GetInt(key);
        }
        public static bool GetBool(ConfigValue key)
        {
            return ConfigPluginControler.SelectedPreset.GetBoolean(key);
        }
        public static void SetValue(ConfigValue key, object value, bool raiseEvent = true)
        {
            ConfigPluginControler.SelectedPreset.SetValue(key, value, raiseEvent);
        }
        public static void SetSpell(string id, SpellConfig value, bool raiseEvent = true)
        {
            if (Spells.Any(i => i.Key == id))
            {
                Spells[id] = value;
                return;
            }
            Spells.Add(id, value);
            if (raiseEvent && OnConfigValueChanged != null)
                OnConfigValueChanged.Invoke(new ConfigValueChangedArgs(id, value));
        }
        public static void SetEvadeSpell(string key, EvadeSpellConfig value, bool raiseEvent = true)
        {
            if (EvadeSpells.Any(i => i.Key == key))
            {
                EvadeSpells[key] = value;
                return;
            }
            EvadeSpells.Add(key, value);
            if (raiseEvent && OnConfigValueChanged != null)
                OnConfigValueChanged.Invoke(new ConfigValueChangedArgs(key, value));
        }

        public static void SetValues(Dictionary<ConfigValue, object> dictionary)
        {
            foreach (var o in dictionary)
            {
                Values[o.Key] = o.Value; //So we don't remove keys that didn't seem to be in the dictionary 
            }
        }
    }

    public class EvadeSpellConfig
    {
        public bool Use { get; set; }
        public SpellDangerLevel DangerLevel { get; set; }
        public SpellModes SpellMode { get; set; }

        public override string ToString()
        {
            return string.Format("Use: {0} DangerLevel: {1} SpellMode: {2}", Use, DangerLevel, SpellMode);
        }
    }

    public class SpellConfig
    {
        public bool Dodge { get; set; }
        public bool Draw { get; set; }
        public float Radius { get; set; }
        public SpellDangerLevel DangerLevel { get; set; }
        public SpellModes EvadeSpellMode { get; set; }
        public string PlayerName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", Dodge, Draw, Radius, DangerLevel, EvadeSpellMode, PlayerName);
        }
    }
}
