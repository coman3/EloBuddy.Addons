using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms.VisualStyles;
using AdEvade.Draw;
using AdEvade.Utils;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AdEvade.Config
{
    public static class ConfigPluginControler
    {
        public static Dictionary<ConfigPluginAttribute, ConfigPreset> Configs { get; set; }
        public static Dictionary<ConfigPluginAttribute, Menu> Menus { get; set; }
        private static int SelectedIndex { get; set; }
        public static ConfigPreset SelectedPreset { get; private set; }
        public static Menu SelectedPresetMenu { get; private set; }
        private static Menu _configMenu;
        private static bool _blockChangedEvent;
        private static float _lastChangedTime;

        public static void MoveTo(int index, bool relative)
        {
            if (relative) index += SelectedIndex;

            if (index > Configs.Count) index = 0;
            SelectedIndex = index;
            SelectedPreset = Configs.ElementAt(index).Value;
            Console.WriteLine("Selected Config Plugin: " + SelectedPreset.GetAttribute().Name);
            Properties.Values.Clear();
            DisableAllInMenu();
            var menu = Menus[SelectedPreset.GetAttribute()];
            menu.DisplayName = "* " + menu.DisplayName;
            SelectedPresetMenu = menu;
            _blockChangedEvent = true;
            menu.Get<CheckBox>("Enabled").CurrentValue = true;
            _blockChangedEvent = false;
            SelectedPreset.InitiateConfig(ref Properties.Values);
            SelectedPreset.LoadConfig();
            _lastChangedTime = Game.Time;
        }

        private static void DisableAllInMenu()
        {
            _blockChangedEvent = true;
            foreach (var menu in Menus)
            {
                menu.Value.Get<CheckBox>("Enabled").CurrentValue = false;
                if (menu.Value == SelectedPresetMenu)
                {
                    menu.Value.DisplayName = menu.Value.DisplayName.Replace("* ", "");
                }
            }
            _blockChangedEvent = false;
        }

        public static void LoadConfigPresets()
        {
            Drawing.OnEndScene += Drawing_OnEndScene;
            _configMenu = MainMenu.AddMenu("AdEvade Presets", "AdEvadePreset", "AdEvade Preset Manager");
            _configMenu.AddGroupLabel("Installed Presets");
            Configs = new Dictionary<ConfigPluginAttribute, ConfigPreset>();
            Console.WriteLine("Loading Config Presets...");
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var plugins = types.Where(IsConfigPlugin);
            foreach (var plugin in plugins)
            {
                var pluginItem = (ConfigPreset)NewInstance(plugin);
                var attribute = pluginItem.GetAttribute();
                Configs.Add(attribute, pluginItem);
                Console.WriteLine("Loaded Config: Name: {0} (By: {1}) Version: {2}\n   Suported Champions: {3}",
                        attribute.Name, attribute.Author, attribute.Version,
                        string.Join(", ", attribute.RecomendedChampions));
                _configMenu.AddLabel(GetFriendlyConfigTitle(attribute));
                _configMenu.AddLabel("Recomended Champions: " + string.Join(", ", attribute.RecomendedChampions));
                _configMenu.AddSeparator();
            }
            LoadMenus(_configMenu);

            MoveTo(Configs.Count - 1, false);
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            Drawing.DrawText(0, 0, Color.White, _lastChangedTime.ToString());
            if (SelectedPreset != null)
            {
                if(_lastChangedTime + Constants.DrawChangeLength > Game.Time)
                    SelectedPreset.DrawOnEnabled();
                SelectedPreset.Draw();
            }
        }

        private static string GetFriendlyConfigTitle(ConfigPluginAttribute attribute)
        {
            return string.Format("{0} (By: {1}) Version: {2}", attribute.Name, attribute.Author, attribute.Version);
        }
        private static void LoadMenus(Menu mainMenu)
        {
            Menus = new Dictionary<ConfigPluginAttribute, Menu>();
            var index = 0;
            foreach (var config in Configs)
            {
                var menu = mainMenu.AddSubMenu(config.Key.Name, null, GetFriendlyConfigTitle(config.Key));
                if (!string.IsNullOrEmpty(config.Key.Description))
                {
                    menu.AddGroupLabel("Description");
                    menu.AddLabel(config.Key.Description, 200);
                }
                var val = index;
                menu.Add("Enabled", new CheckBox("Enabled", false)).OnValueChange +=
                    delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                    {
                        if(_blockChangedEvent) return;
                        if (args.OldValue && !args.NewValue) sender.CurrentValue = false;
                        if (sender.CurrentValue) MoveTo(val, false);
                    };
                index++;
                Menus.Add(config.Key, menu);
            }
        }

        private static bool IsConfigPlugin(Type t)
        {
            return t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ConfigPreset));
        }
        private static object NewInstance(Type type)
        {
            var target = type.GetConstructor(Type.EmptyTypes);
            var dynamic = new DynamicMethod(string.Empty, type, new Type[0], target.DeclaringType);
            var il = dynamic.GetILGenerator();

            il.DeclareLocal(target.DeclaringType);
            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            var method = (Func<object>)dynamic.CreateDelegate(typeof(Func<object>));
            return method();
        }
    }
}