using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EzEvade.Draw;
using EzEvade.EvadeSpells;

namespace EzEvade.Config
{
    public enum ConfigDataType
    {
        Data, Spells, KeyBind, EvadeSpell
    }

    public class ConfigValueChangedArgs : EventArgs
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public ConfigValueChangedArgs(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
    public static class Properties
    {
        public delegate void ConfigValueChangedHandler(ConfigValueChangedArgs args);
        public static event ConfigValueChangedHandler OnConfigValueChanged;


        public static readonly Dictionary<string, object> Data = new Dictionary<string, object>();

        public static readonly Dictionary<string, SpellConfig> Spells = new Dictionary<string, SpellConfig>();

        public static readonly Dictionary<string, EvadeSpellConfig> EvadeSpells = new Dictionary<string, EvadeSpellConfig>();

        public static readonly Dictionary<string, KeyBind> Keys = new Dictionary<string, KeyBind>();

        public static SpellConfig GetSpellConfig(this Data.SpellData spell, SpellConfigControl control)
        {
            return new SpellConfig
            {
                DangerLevel = Convert.ToInt16(spell.Dangerlevel),
                Dodge = true,
                Draw = true,
                Radius = spell.Radius
            };
        }

        public static T GetData<T>(string key)
        {
            if (Data.Any(i => i.Key == key))
            {
                if(Data[key] is T)
                    return (T) Data[key];
                else 
                    Debug.DrawTopLeft("Tryed To Access key with wrong type: " + key);
            }
            return default(T);
        }
        public static SpellConfig GetSpell(string key)
        {
            if (Spells.Any(i => i.Key == key))
            {
                return Spells[key];
            }
            return new SpellConfig {DangerLevel = 2, Dodge = true, Draw = true, EvadeSpellMode = 1, Radius = 20};
        }
        public static void SetData(string key, object value, bool raiseEvent = true)
        {
            if (Data.Any(i => i.Key == key))
            {
                Data[key] = value;
                return;
            }
            Data.Add(key, value);
            if (raiseEvent && OnConfigValueChanged != null)
                OnConfigValueChanged.Invoke(new ConfigValueChangedArgs(key, value));
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

        public static void SetKey(string key, KeyBind value, bool raiseEvent = true)
        {
            if (Keys.Any(i => i.Key == key))
            {
                Keys[key] = value;
                return;
            }
            Keys.Add(key, value);
        }
    }

    public struct EvadeSpellConfig
    {
        public bool Use { get; set; }
        public int DangerLevel { get; set; }
        public int SpellMode { get; set; }
    }

    public struct SpellConfig
    {
        public bool Dodge { get; set; }
        public bool Draw { get; set; }
        public float Radius { get; set; }
        public int DangerLevel { get; set; }
        public int EvadeSpellMode { get; set; }
    }
}
