using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace PerfectWard.Config { 
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

        /// <summary>
        /// The Data Set All Dynamic Controls(<see cref="DynamicSlider"/>, <see cref="DynamicCheckBox"/>) access.
        /// </summary>
        public static readonly Dictionary<string, object> Data = new Dictionary<string, object>();
        /// <summary>
        /// The Data Set <see cref="DynamicKeyBind"/> access.
        /// </summary>
        public static readonly Dictionary<string, KeyBind> Keys = new Dictionary<string, KeyBind>();

        /// <summary>
        /// Gets data from the Data Set.
        /// </summary>
        /// <typeparam name="T">Type of data saved</typeparam>
        /// <param name="key">Key within the Data Set</param>
        /// <returns>Returns the value of the given key. If the key is not not found, returns the default value of that object.</returns>
        public static T GetData<T>(string key)
        {
            if (Data.Any(i => i.Key == key))
            {
                if(Data[key] is T)
                    return (T) Data[key];
            }
            Console.WriteLine("Key '" + key + "' not found returning: " + default(T));
            return default(T);
        }
        /// <summary>
        /// Sets the data at the specified key 
        /// </summary>
        /// <param name="key">Key within the Data Set</param>
        /// <param name="value">Data to store with that key</param>
        /// <param name="raiseEvent">Whether of not to raise an event (*Set to false when using on a <see cref="Menu"/> item*)</param>
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
        /// <summary>
        /// Gets the <see cref="KeyBind"/> at the specified key.
        /// </summary>
        public static KeyBind GetKey(string key)
        {
            if (Keys.Any(i => i.Key == key))
                return Keys[key];
            return default(KeyBind);
        }
        /// <summary>
        /// Sets a <see cref="KeyBind"/> at the specified key (Overwrites)
        /// </summary>
        /// <param name="key">Key within the Data Set</param>
        /// <param name="value">Data to store with that key</param>
        /// <param name="raiseEvent">hether of not to raise an event (*Set to false when using on a <see cref="Menu"/> item*)</param>
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
}
