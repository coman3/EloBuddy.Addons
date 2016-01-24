using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace CameraBuddy.Camera
{
    public static class CameraMovement
    {
        public static bool IsMoving { get; private set; }
        public static CameraState BeforeMoveState { get; private set; }
        public static CameraState AfterMoveState { get; private set; }
        public static float Distance { get { return BeforeMoveState.Position.Distance(AfterMoveState.Position); } }
        public static float Speed { get; set; }
        public static Priority CurrentMovePriority { get; set; }
        public static Vector2 SpeedVector
        {
            get
            {
                if(BeforeMoveState == null || AfterMoveState == null) return Vector2.Zero;
                return new Vector2((BeforeMoveState.Position.X - AfterMoveState.Position.X)/Speed,
                    (BeforeMoveState.Position.Y - AfterMoveState.Position.Y)/Speed);
            }
        }

        public static void MoveToSmooth(Priority priority, Vector2 end, float speed, bool zoomInOut = false)
        {
            if (IsMoving && priority <= CurrentMovePriority) return;

            //Setting states so we know where we are going and where from
            SetMoveStates(end);
            Speed = speed;

            if (Distance < 5) return;
            IsMoving = true;
            CurrentMovePriority = priority;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void SetMoveStates(Vector2 end)
        {
            BeforeMoveState = new CameraState();
            BeforeMoveState.Set();
            AfterMoveState = new CameraState(BeforeMoveState.YawPitch, end, BeforeMoveState.Zoom);
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Game.CursorPos2D.X < 20 || Game.CursorPos2D.Y < 20) IsMoving = false;
            if (Drawing.Width - Game.CursorPos2D.X < 20 || Drawing.Height - Game.CursorPos2D.Y < 20) IsMoving = false;
            if(AfterMoveState == null || BeforeMoveState == null) return;

            if (EloBuddy.Camera.ScreenPosition.Distance(AfterMoveState.Position) < 5)
            {
                AfterMoveState.Set(false);
                IsMoving = false;
            }

            if (!IsMoving)
            {
                UnHookMove();
                return;
            }

            //Everything is all good, so lets continue
            EloBuddy.Camera.ScreenPosition = new Vector2(EloBuddy.Camera.ScreenPosition.X - SpeedVector.X, EloBuddy.Camera.ScreenPosition.Y - SpeedVector.Y);

            //TODO: Drawing Menu
            Circle.Draw(new ColorBGRA(255, 0, 0, 255), 10, BeforeMoveState.Position.To3D(), AfterMoveState.Position.To3D());
            Line.DrawLine(Color.Blue, BeforeMoveState.Position.To3D(), AfterMoveState.Position.To3D());
        }

        private static void UnHookMove()
        {
            Drawing.OnDraw -= Drawing_OnDraw;
            CurrentMovePriority = Priority.VeryLow;
            Speed = -1;
            AfterMoveState = null;
            BeforeMoveState = null;
        }
    }

    public enum Priority : byte
    {
        VeryLow = 0,
        Low = 1,
        LowMedium = 2,
        Medium = 3,
        HighMedium = 4,
        LowHigh = 5,
        High = 6,
        VeryHigh = 7
    }
}