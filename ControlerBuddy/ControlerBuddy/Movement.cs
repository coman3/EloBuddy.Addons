using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlerBuddy.WindowsHooks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.XInput;
using Color = System.Drawing.Color;
using Font = SharpDX.Direct3D9.Font;
using Line = EloBuddy.SDK.Rendering.Line;
using RectangleF = SharpDX.RectangleF;

namespace ControlerBuddy
{
    public static class Movement
    {
        public static bool DisableAtacking { get { return Orbwalker.DisableAttacking; } set { Orbwalker.DisableAttacking = value; } }
        public static Vector3 OrbwalkingPosistion { get; set; }

        public static Orbwalker.ActiveModes Mode
        {
            get { return Orbwalker.ActiveModesFlags; }
            set
            {
                ModeText.TextValue = Mode.ToString();
                Orbwalker.ActiveModesFlags = value;
            }
        }
        public static RectangleF ModeSelectorRectangle { get; set; }
        public static GamepadButtonFlags OpenModeSelectorKey = GamepadButtonFlags.RightShoulder;
        public static bool ModeSelectorKeyDown { get; set; }
        public static Text ModeText { get; set; }
        public static RectangleF HudRectangle { get; set; }
        #region Initialization
        public static void Initialize()
        {
            PrepVariables();

            CreateModeText();
            Mode = Orbwalker.ActiveModes.None;

            HookEvents();

        }

        private static void PrepVariables()
        {
            HudRectangle = new RectangleF(Drawing.Width / 3.575f, Drawing.Height - Drawing.Height / 5.5f, Drawing.Width - Drawing.Width / 3.425f * 2, 300);
        }
        private static void CreateModeText()
        {
            ModeText = new Text(" ", new FontDescription
            {
                FaceName = "Agency FB",
                Height = 50,
                Width = 30,
                Weight = FontWeight.DemiBold,
                OutputPrecision = FontPrecision.Default,
                Quality = FontQuality.Antialiased
            })
            {
                Position = new Vector2(10, 10),
                Color = Color.White,
                TextValue = " "
            };
        }
        private static void HookEvents()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Game.OnUpdate += Game_OnUpdate;
        }
        #endregion

        #region Drawing
        private static void Drawing_OnEndScene(EventArgs args)
        {
            Line.DrawLine(Color.White, HudRectangle.TopLeft, HudRectangle.BottomRight);
        }
        private static void Drawing_OnDraw(EventArgs args)
        {
            Circle.Draw(new ColorBGRA(255, 255, 255, 255), 20, 2, OrbwalkingPosistion);
            ModeText.Draw();
            Drawing.DrawText(Game.CursorPos2D.Offset(new Vector2(0, -20)), Color.White, Game.CursorPos2D.ToString(), 10);
        }
        #endregion

        private static void Game_OnUpdate(EventArgs args)
        {
            OrbwalkingPosistion = Player.Instance.Position.Offset(ControllerManager.LeftStick);

            if (OrbwalkingPosistion.Distance(Player.Instance.Position) > Orbwalker.HoldRadius)
                Orbwalker.OrbwalkTo(OrbwalkingPosistion);
            else
                Player.IssueOrder(GameObjectOrder.Stop, Player.Instance.Position);


            ModeSelectorKeyDown = ControllerManager.Buttons.HasFlag(OpenModeSelectorKey);
            if(ModeSelectorKeyDown) CheckSelectMode();
        }

        private static void CheckSelectMode()
        {
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.A)) Mode = Orbwalker.ActiveModes.Combo;
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.X)) Mode = Orbwalker.ActiveModes.LaneClear;
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.Y)) Mode = Orbwalker.ActiveModes.LastHit;
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.B)) Mode = Orbwalker.ActiveModes.None;
        }

        

    }
}
