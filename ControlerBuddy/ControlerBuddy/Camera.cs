using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using SharpDX.XInput;

namespace ControlerBuddy
{
    public class Camera
    {
        public static Vector3 CameraPosistion { get; set; }
        public static CameraMode CameraMode { get; set; }
        public static bool CameraModeSwitchTimer { get; set; }
        public static void Initialize()
        {
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Game_OnUpdate(System.EventArgs args)
        {
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.RightThumb) && !CameraModeSwitchTimer)
            {
                CameraModeSwitchTimer = true;
                CameraMode++;
                Console.WriteLine(CameraMode);
                Core.DelayAction(() => CameraModeSwitchTimer = false, 200);
            }
        }

        private static void Drawing_OnDraw(System.EventArgs args)
        {
            EloBuddy.Camera.ScreenPosition = Player.Instance.Position.Offset(ControllerManager.RightStick).To2D();
        }
    }

    public enum CameraMode
    {
        Locked,
        Variable,
        Target

    }
}