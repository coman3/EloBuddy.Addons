using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using EloBuddy;
using EloBuddy.SDK.Menu;

namespace AdEvade.Config.Plugins
{
    [ConfigPlugin(Name = "Custom", Author = "", Description = "Set your own config preset. Preset can be found in '%appdata%/AdEvade/Data.xml'.", RecomendedChampions = new [] {"All"}, Version = "0.0.0.0")]
    public class Custom : ConfigPreset
    {
        public static readonly string ConfigDataFile = Path.Combine(Constants.ConfigSaveFolder, Constants.ConfigSaveDataFileName);
        public static readonly string ConfigDataFolder = Path.GetDirectoryName(ConfigDataFile);
        public bool Saved = false;
        public override void InitiateConfig(ref Dictionary<ConfigValue, object> values)
        {
            Values = values;
            if (!File.Exists(ConfigDataFile))
            {
                SaveConfigData();
            }
            Game.OnUpdate += Game_OnUpdate;
        } //Leaves config untouched and saves it, or loads it when this preset is selected

        private void Game_OnUpdate(EventArgs args)
        {
            if (!MainMenu.IsVisible && !Saved)
            {
                SaveConfigData();
                Saved = true;
            }
        }

        public override void SetValue(ConfigValue key, object value, bool raiseEvent = true)
        {
            base.SetValue(key, value, raiseEvent);
            var name = key.Name().ToLower();
            if (name.Contains("key") && !name.Contains("keys")) return;
            if (Saved) Saved = false; //Saying that a value has changed.
            
        }

        public override void LoadConfig()
        {
            LoadConfigData();
        }

        public override void UnLoadConfig()
        {
            SaveConfigData();
            Game.OnUpdate -= Game_OnUpdate;
        }
        public bool LoadConfigData()
        {
            ConsoleDebug.WriteLineColor("Please ignore the following errors, as its a EloBuddy issue, and will only happen once per load. (this message will display twice).", ConsoleColor.Green, true);
            FileStream file = File.OpenRead(ConfigDataFile);
            try
            {
                if (!Directory.Exists(ConfigDataFolder)) return false;
                if (!File.Exists(ConfigDataFile)) return false;
                XmlSerializer x = new XmlSerializer(typeof (SerializableDictionary<ConfigValue, object>));
                var temp = x.Deserialize(file) as SerializableDictionary<ConfigValue, object>;
                if (temp != null)
                {
                    foreach (var o in temp)
                    {
                        SetValue(o.Key, o.Value);
                    }
                }
                    

                file.Close();
                ConsoleDebug.WriteLineColor("Alright, from now all errors are now an issue!", ConsoleColor.DarkGreen, true);
                return true;
            }
            catch (Exception ex)
            {
                ConsoleDebug.WriteLineColor("Alright, from now all errors are now an issue!", ConsoleColor.DarkGreen, true);
                ConsoleDebug.WriteLine("Error Loading Config Data File...");
                ConsoleDebug.WriteLine(ex);
            }
            finally
            {
                file.Close();
            }
            return false;
        }

        public void SaveConfigData()
        {
            try
            {
                if (!Directory.Exists(ConfigDataFolder)) Directory.CreateDirectory(ConfigDataFolder);
                if (!File.Exists(ConfigDataFile)) File.WriteAllText(ConfigDataFile, "");
                FileStream file = File.OpenWrite(ConfigDataFile);
                try
                {
                    XmlSerializer x = new XmlSerializer(typeof(SerializableDictionary<ConfigValue, object>));
                    ConsoleDebug.WriteLine("Saving Config Data...");
                    x.Serialize(file, new SerializableDictionary<ConfigValue, object>(Values));
                    file.Close();
                    ConsoleDebug.WriteLine("     Successful!");
                }
                catch (Exception ex)
                {
                    ConsoleDebug.WriteLine("     Error Saving Config Data File...");
                    ConsoleDebug.WriteLine(ex);
                }
                finally
                {
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                ConsoleDebug.WriteLine("    Error Reading Config File...");
                ConsoleDebug.WriteLine(ex);
            }

        }
    }
}