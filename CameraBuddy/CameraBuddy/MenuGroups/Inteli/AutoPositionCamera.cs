using System;
using System.Linq;
using CameraBuddy.Camera;
using CameraBuddy.Game;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace CameraBuddy.MenuGroups.Inteli
{
    public class AutoPositionCamera : MenuGroup
    {
        private const float CrosshairSize = 10f;
        private readonly Vector2 _centerScreen = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f);

        public KeyBind DirectCameraTowardsHeroes { get; set; }
        public KeyBind DirectCameraTowardsMinions { get; set; }
        public KeyBind DirectCameraTowardsMouse { get; set; }
        private CameraModeSelector CameraMode
        {
            get
            {
                if(DirectCameraTowardsMouse.CurrentValue) return CameraModeSelector.Mouse;
                if (DirectCameraTowardsHeroes.CurrentValue) return CameraModeSelector.Heroes;
                if (DirectCameraTowardsMinions.CurrentValue) return CameraModeSelector.Minions;
                return CameraModeSelector.None;
            }
        }

        public Slider HeroDetectionRange { get; set; }
        public Slider MinionDetectionRange { get; set; }
        public CheckBox DrawFocusPoint { get; set; }
        public CheckBox DrawCrosshair { get; set; }
        public CheckBox DrawDetectionRange { get; set; }
        public Slider ExtraDistance { get; set; }


        private Vector2[] Crosshair { get; set; }
        private CameraState CameraState { get; set; }
        private Vector2 FocusPoint { get; set; }
        private bool CrosshairMade { get; set; }
        public AutoPositionCamera()
        {
            Drawing.OnDraw += Draw;
            EloBuddy.Game.OnUpdate += OnUpdate;
            CameraState = new CameraState();
            FocusPoint = Vector2.Zero;
        }

        private void CreateCrosshair(Vector2 centerPos)
        {
            Crosshair = new Vector2[5];
            Crosshair[0] = new Vector2(centerPos.X - CrosshairSize, centerPos.Y);
            Crosshair[1] = new Vector2(centerPos.X + CrosshairSize, centerPos.Y);
            Crosshair[2] = new Vector2(centerPos.X, centerPos.Y);
            Crosshair[3] = new Vector2(centerPos.X, centerPos.Y + CrosshairSize);
            Crosshair[4] = new Vector2(centerPos.X, centerPos.Y - CrosshairSize);
            CrosshairMade = true;
        }

        private void OnUpdate(EventArgs args)
        {
            CheckCrosshair();
            var distance = 0f;
            var playerPos = Player.Instance.Position;
            switch (CameraMode)
            {
                case CameraModeSelector.None:
                    return;
                case CameraModeSelector.Heroes:
                    var heroes =
                        Heroes.Enemies.Where(
                            x =>
                                x.GetPosistion().Distance(Player.Instance.Position) < HeroDetectionRange.CurrentValue &&
                                x.IsAlive()).ToList();
                    if (heroes.Count <= 0)
                    {
                        FocusPoint = playerPos.To2D();
                        return;
                    }
                    FocusPoint = heroes.AveragePosition().To2D();
                    distance = Math.Min(ExtraDistance.CurrentValue, playerPos.Distance(FocusPoint));
                    FocusPoint = playerPos.Extend(FocusPoint, distance);

                    break;
                case CameraModeSelector.Minions:
                    var minions = Minions.Enemy.Where(x => x.GetPosistion().Distance(Player.Instance.Position) < MinionDetectionRange.CurrentValue / 2f && x.IsAlive()).ToList();
                    if (minions.Count <= 0)
                    {
                        minions = Minions.Ally.Where(x => x.GetPosistion().Distance(Player.Instance.Position) < MinionDetectionRange.CurrentValue / 2f && x.IsAlive()).ToList();
                        if (minions.Count <= 0)
                        {
                            FocusPoint = playerPos.To2D();
                            return;
                        }
                    }
                    FocusPoint = minions.AveragePosition().To2D();
                    distance = Math.Min(ExtraDistance.CurrentValue, playerPos.Distance(FocusPoint));
                    FocusPoint = playerPos.Extend(FocusPoint, distance);
                    break;
                case CameraModeSelector.Mouse:
                    var pos = EloBuddy.Game.CursorPos2D;
                    distance = Math.Min(pos.Distance(_centerScreen), ExtraDistance.CurrentValue);
                    var angle = new Vector2(-(_centerScreen.X - pos.X) / _centerScreen.Distance(pos),
                        -(pos.Y - _centerScreen.Y) / _centerScreen.Distance(pos));
                    FocusPoint = new Vector2(Player.Instance.Position.X + angle.X * distance,
                        Player.Instance.Position.Y + angle.Y * distance);
                    break;
                default:
                    return;
            }
        }

        private void CheckCrosshair()
        {
            if (DrawCrosshair.CurrentValue)
            {
                if (Crosshair == null || Crosshair.Length <= 0)
                {
                    CreateCrosshair(_centerScreen);
                }
            }
        }

        private void Draw(EventArgs args)
        {

            if(!Enabled) return;
            if (DrawCrosshair.CurrentValue && CrosshairMade)
            {
                Line.DrawLine(Color.White, Crosshair);
            }

            if (CameraState == null) return;
            if (FocusPoint == Vector2.Zero) return;

            var mode = CameraMode;
            if (mode != CameraModeSelector.None)
            {
                if (DrawFocusPoint.CurrentValue)
                    Circle.Draw(new ColorBGRA(255, 255, 100, 100), 40, FocusPoint.To3D());
                CameraState.Position = FocusPoint;
                CameraState.Set();
            }
            if(DrawDetectionRange.CurrentValue)
                Circle.Draw(new ColorBGRA(100, 100, 100, 100),
                    mode != CameraModeSelector.Minions
                        ? HeroDetectionRange.CurrentValue
                        : MinionDetectionRange.CurrentValue, Player.Instance.Position);


            
        }

        public override void AddToMenu(Menu menuBase)
        {
            var menu = menuBase.Parent.AddSubMenu("     Auto Position");
            menu.AddLabel("'Auto Position' will automatically move the camera relatively towards Enemy Heroes, Minions, or The Mouse.");
            menu.AddLabel("It will always keep your player visible within the view port.");
            menu.AddSeparator(10);

            DirectCameraTowardsHeroes = AddKeyBind(new KeyBind("Direct Towards Heroes", false, KeyBind.BindTypes.HoldActive));
            DirectCameraTowardsMinions = AddKeyBind(new KeyBind("Direct Towards Minions", false, KeyBind.BindTypes.HoldActive));
            DirectCameraTowardsMouse = AddKeyBind(new KeyBind("Direct Towards Mouse", false, KeyBind.BindTypes.HoldActive));

            HeroDetectionRange = AddSlider(new Slider("Hero Detection Range", 3000, 500, 5000));
            MinionDetectionRange = AddSlider(new Slider("Minion Detection Range", 3000, 500, 5000));

            DrawFocusPoint = AddCheckbox(new CheckBox("Draw Focus Point"));
            DrawCrosshair = AddCheckbox(new CheckBox("Draw Crosshair"));
            DrawDetectionRange = AddCheckbox(new CheckBox("Draw Detection Range"));
            ExtraDistance = AddSlider(new Slider("Extra Distance towards focus point", 100, 0, 1000));


            base.AddToMenu(menu);
        }

        public override string GetUniqueId()
        {
            return "AutoPositionCamera";
        }

        private enum CameraModeSelector
        {
            None,
            Heroes,
            Minions,
            Mouse
        }
    }
}