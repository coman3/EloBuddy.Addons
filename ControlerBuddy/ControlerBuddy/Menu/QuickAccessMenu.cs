using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.XInput;
using RectangleF = SharpDX.RectangleF;

namespace ControlerBuddy.Menu
{
    public class QuickAccessMenu
    {
        //Buttons
        public static ButtonCollection<Sprite> ButtonSprites { get; set; }
        public static RectangleF ButtonRectangle { get; set; }

        //Menu
        public RectangleF Rectangle { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public bool IsOpen { get; private set; }
        private float LastButton { get; set; }
        public float ButtonDelay { get; set; }

        public QuickAccessMenu(RectangleF rectangle, MenuGroup parentGroup)
        {
            Rectangle = rectangle;
            MenuGroup = parentGroup;

            LastButton = Game.Time;
            ButtonDelay = 0.1f;

            ButtonRectangle = new RectangleF(0, 0, 24, 24);
            ButtonSprites = new ButtonCollection<Sprite>();
            ButtonSprites.AddAll(
                new Sprite(TextureLoader.BitmapToTexture(Get(Properties.Resources.Button_A, ButtonRectangle.Width))),
                new Sprite(TextureLoader.BitmapToTexture(Get(Properties.Resources.Button_B, ButtonRectangle.Width))),
                new Sprite(TextureLoader.BitmapToTexture(Get(Properties.Resources.Button_X, ButtonRectangle.Width))),
                new Sprite(TextureLoader.BitmapToTexture(Get(Properties.Resources.Button_Y, ButtonRectangle.Width))));
        }

        public void Show()
        {
            if(IsOpen) return;
            MenuGroup.ResetItems();
            Drawing.OnEndScene += Drawing_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
            MenuGroup.Active = true;
            IsOpen = true;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (Game.Time - LastButton > ButtonDelay)
            {
                if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.A))
                {
                    if (SelectItem(MenuGroup, ControllerButton.A))
                        Hide();
                }
                else if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.B))
                {
                    if (SelectItem(MenuGroup, ControllerButton.B))
                        Hide();
                }
                else if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.X))
                {
                    if (SelectItem(MenuGroup, ControllerButton.X))
                        Hide();
                }
                else if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.Y))
                {
                    if (SelectItem(MenuGroup, ControllerButton.Y))
                        Hide();
                }else return;
                
                LastButton = Game.Time;
            }

        }

        private bool SelectItem(MenuGroup group,  ControllerButton controllerButton)
        {
            if (group.Active)
                group.SelectItem(controllerButton);
            else if(group.Values.Any(x => x != null && x.Child != null))
            {
                
            }
            return false;
        }

        public void Hide()
        {
            if (!IsOpen) return;
            Drawing.OnEndScene -= Drawing_OnDraw;
            Game.OnUpdate -= Game_OnUpdate;
            IsOpen = false;
        }

        public static Bitmap Get(Bitmap map, float size)
        {
            var bitmap = new Bitmap((int)size, (int)size);
            var g = Graphics.FromImage(bitmap);
            g.DrawImage(map, new System.Drawing.RectangleF(0, 0, size, size));
            g.Save();
            return bitmap;
        }
        #region Drawing

        public void DrawButton(Vector2 centerPos, GamepadButtonFlags button)
        {
            switch (button)
            {
                case GamepadButtonFlags.Y:
                    ButtonSprites[3].Draw(centerPos.Offset(-ButtonRectangle.Center));
                    break;
                case GamepadButtonFlags.B:
                    ButtonSprites[1].Draw(centerPos.Offset(-ButtonRectangle.Center));
                    break;
                case GamepadButtonFlags.X:
                    ButtonSprites[2].Draw(centerPos.Offset(-ButtonRectangle.Center));
                    break;
                case GamepadButtonFlags.A:
                    ButtonSprites[0].Draw(centerPos.Offset(-ButtonRectangle.Center));
                    break;
            }

        }

        private void Drawing_OnDraw(EventArgs args)
        {
            MenuGroup.Draw();

            DrawButton(Rectangle.TopCenter().Offset(0, -ButtonRectangle.Height), GamepadButtonFlags.Y);
            DrawButton(Rectangle.RightCenter().Offset(ButtonRectangle.Width, 0), GamepadButtonFlags.B);
            DrawButton(Rectangle.LeftCenter().Offset(-ButtonRectangle.Width, 0), GamepadButtonFlags.X);
            DrawButton(Rectangle.BottomCenter().Offset(0, ButtonRectangle.Height), GamepadButtonFlags.A);



        }

        #endregion

    }
}