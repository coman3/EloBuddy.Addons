using System;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CameraBuddy.Spectate.Core
{
    public static class Camera
    {
        public static Menu CameraMenu { get; private set; }
        public static readonly float CameraYYaw = EloBuddy.Camera.YawY;
        public static readonly float CameraXYaw = EloBuddy.Camera.YawX;
        public static readonly float CameraPitch = EloBuddy.Camera.Pitch;
        public static readonly float CameraZoom = EloBuddy.Camera.ZoomDistance;

        static Camera()
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            EloBuddy.Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (FolowAction)
            {
                EloBuddy.Camera.CameraX = Situation.Game.GameStateInfo.Posistion.X;
                EloBuddy.Camera.CameraY = Situation.Game.GameStateInfo.Posistion.Y;
            }
        }

        public static bool FolowAction { get; set; }

        public static void AddMenu(Menu mainMenu)
        {
            CameraMenu = mainMenu.AddSubMenu("Camera", "Camera");
            CameraMenu.Add("CameraZoom", new Slider("Camera Zoom Level", (int) CameraZoom, 0, 10000)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    EloBuddy.Camera.SetZoomDistance(changeArgs.NewValue);
                };
            CameraMenu.Add("CameraXYaw", new Slider("Camera X Yaw", (int) CameraXYaw, 0, 390)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    EloBuddy.Camera.YawX = changeArgs.NewValue;
                };
            CameraMenu.Add("CameraYYaw", new Slider("Camera Y Yaw", (int) CameraYYaw, 0, 390)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    EloBuddy.Camera.YawY = changeArgs.NewValue;
                };
            CameraMenu.Add("CameraPitch", new Slider("Camera Pitch", (int) CameraPitch, 2, 89)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    EloBuddy.Camera.Pitch = changeArgs.NewValue;
                };
            CameraMenu.Add("CameraFollowAction", new CheckBox("Follow Game Action", false)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    FolowAction = changeArgs.NewValue;
                };
            CameraMenu.Add("CameraSetToDefault", new CheckBox("Set Camera TO Default", false)).OnValueChange
                +=
                (sender, changeArgs) =>
                {
                    if (!changeArgs.OldValue && changeArgs.NewValue)
                    {
                        EloBuddy.Camera.YawX = CameraXYaw;
                        EloBuddy.Camera.YawY = CameraYYaw;
                        EloBuddy.Camera.Pitch = CameraPitch;
                        EloBuddy.Camera.SetZoomDistance(CameraZoom);
                        sender.CurrentValue = false;
                    }

                };
        }
    }
}