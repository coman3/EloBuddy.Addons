using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraBuddy.Camera;
using CameraBuddy.MenuGroups;
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
        static void Main()
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            LoadMenu();
        }

        private static void LoadMenu()
        {
            MainMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu("CameraBuddy", "CameraBuddy");
            MainMenu.Add("CameraSwitchMode", new KeyBind("Switch Camera Mode", false, KeyBind.BindTypes.PressToggle))
                .OnValueChange += (sender, args) =>
                {
                    SwitchMode();
                };
            MainMenu.Add("CameraReset", new CheckBox("Reset Camera", false))
                .OnValueChange += (sender, args) =>
                {
                    if (args.OldValue || !args.NewValue) return;
                    Reset();
                    sender.CurrentValue = false;
                };

            IntelligenceMenu = MainMenu.AddSubMenu("Intelligence");
            IntelligenceMenu.AddGroup(new AutoMoveOnDamage());
            IntelligenceMenu.AddGroup(new AutoPositionCamera());
        }

        private static void SwitchMode()
        {
            Console.WriteLine("SwitchMode");
            var cameraState = new CameraState { Position = Player.Instance.Position.To2D() };
            cameraState.Set(false, 25);
        }

        private static void Reset()
        {
            
        }
    }
}
