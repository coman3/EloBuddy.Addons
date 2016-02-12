using System;
using System.Runtime.InteropServices;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX.XInput;

namespace ControlerBuddy.WindowsHooks
{
    public class Mouse
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        public static void Lock()
        {
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            
            if ((ControllerManager.Buttons != GamepadButtonFlags.None || Movement.Mode != Orbwalker.ActiveModes.None))
            {
                var pos = Player.Instance.Position.WorldToScreen();
                SetCursorPos((int)pos.X, (int)pos.Y);
            }
        }
    }
}