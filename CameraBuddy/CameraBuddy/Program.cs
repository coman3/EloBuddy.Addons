using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraBuddy.Camera;
using CameraBuddy.MenuGroups;
using CameraBuddy.MenuGroups.Events;
using CameraBuddy.MenuGroups.Inteli;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CameraBuddy
{
    class Program
    {
        public static Menu MainMenu { get; set; }
        public static Menu IntelligenceMenu { get; set; }
        public static Menu EventMenu { get; set; }
        public static CameraState DefaultCameraState { get; set; }
        static void Main()
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            DefaultCameraState = new CameraState();
            LoadMenu();
        }

        private static void LoadMenu()
        {
            MainMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu("CameraBuddy", "CameraBuddy");
            MainMenu.Add("CameraReset", new CheckBox("Reset Camera", false))
                .OnValueChange += (sender, args) =>
                {
                    if (args.OldValue || !args.NewValue) return;
                    Reset();
                    sender.CurrentValue = false;
                };
            EventMenu = MainMenu.AddSubMenu("Events");
            EventMenu.AddGroup(new AutoMoveOnDamage());
            //EventMenu.AddGroup(new DeathSpectate());

            IntelligenceMenu = MainMenu.AddSubMenu("Intelligence");
            IntelligenceMenu.AddGroup(new AutoPositionCamera());
        }

        private static void Reset()
        {
            DefaultCameraState.Set();
        }
    }
}
