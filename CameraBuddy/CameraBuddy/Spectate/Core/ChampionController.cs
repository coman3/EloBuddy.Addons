using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EloBuddy;

namespace CameraBuddy.Spectate.Core
{
    public static class ChampionController
    {
        public static ChampionPlugin ChampionPlugin { get; private set; }
        private static Assembly CurrentAssembly {get { return Assembly.GetExecutingAssembly();} } 
        public static bool LoadChampion(AIHeroClient instance)
        {
            var possiablePlugins =
                CurrentAssembly.GetTypes()
                    .Where(
                        x =>
                            x.IsClass && !x.IsAbstract &&
                            x.IsSubclassOf(typeof (ChampionPlugin)) && (
                                (ChampionPluginAttribute) x.GetCustomAttribute(typeof (ChampionPluginAttribute)))
                                .ChampionName == instance.ChampionName).ToList();

            if (possiablePlugins.Count == 0) return false;
            if(possiablePlugins.Count > 1) Console.WriteLine("Found many plugins for: " + instance.ChampionName +". Randomizing what plugin to use...");

            List<ChampionPlugin> plugins = new List<ChampionPlugin>();
            foreach (var possiablePlugin in possiablePlugins)
            {
                var attribute = (ChampionPluginAttribute) possiablePlugin.GetCustomAttribute(typeof (ChampionPluginAttribute));
                if (attribute.SuportedVersion == CurrentAssembly.GetName().Version.ToString())
                {
                    var plugin = (ChampionPlugin)Activator.CreateInstance(possiablePlugin);
                    plugins.Add(plugin);
                }
            }
            ChampionPlugin = plugins.RandomItem();
            return true;
        }
    }
}
