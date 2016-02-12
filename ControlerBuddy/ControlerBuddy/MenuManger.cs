using System;
using System.Linq;
using ControlerBuddy.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;
using SharpDX.XInput;
using Color = System.Drawing.Color;
using Line = EloBuddy.SDK.Rendering.Line;
using RectangleF = SharpDX.RectangleF;

namespace ControlerBuddy
{
    public static class MenuManger
    {
        public static readonly Vector2 CenterScreen = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f);
        public static QuickAccessMenu Menu { get; set; }
        public static RectangleF MenuRectangle { get; set; }

        #region Initialization 

        public static void Initialize()
        {
            
            MenuRectangle = CenterScreen.Offset(new Vector2(0, Drawing.Height / 6f)).RectangleFromCenter(250);
            Menu = new QuickAccessMenu(MenuRectangle,
                new BasicMenuGroup("Group1", MenuRectangle, new MenuItem[]
                {
                    new TextMenuItem("A", ControllerButton.A, () => Chat.Print("Testing A!")),
                    new TextMenuItem("B", ControllerButton.B, null, new BasicMenuGroup("Group2", MenuRectangle, new MenuItem[]
                    {
                        new TextMenuItem("A 1", ControllerButton.A, () => Chat.Print("Testing Sub A!")),
                        new TextMenuItem("B 1", ControllerButton.B, null, new BasicMenuGroup("Group 3", MenuRectangle, new MenuItem[]
                        {
                            new TextMenuItem("B 1 1", ControllerButton.B, () => { }), 
                        })),
                        
                    })),
                    new TextMenuItem("X", ControllerButton.X, null),
                    new TextMenuItem("Y", ControllerButton.Y, null),
                }));
            Menu.Show();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (ControllerManager.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder)) Menu.Show();
        }

        #endregion

    }

    #region Menu Objects

    #region Base

    #endregion

    #region Groups
    public class BasicMenuGroup : MenuGroup
    {
        public BasicMenuGroup(string name, RectangleF rectangle, MenuItem[] menuItems) : base(name, rectangle)
        {
            foreach (var menuItem in menuItems.Where(x => x != null))
            {
                menuItem.SetParent(this);
                Add(menuItem.SelectButton, menuItem);
            }
        }
    }

    #endregion

    #region Items

    public class GroupMenuItem : MenuItem
    {
        public GroupMenuItem(MenuGroup child) : base(child)
        {
        }

        public override void Draw(RectangleF rectangle)
        {
            
        }

        public override bool Select()
        {
            
        }
    }
    public class TextMenuItem : MenuItem
    {
        public string Text { get; set; }
        public Action OnSelect { get; set; }

        public TextMenuItem(string text, ControllerButton button, Action onSelect, MenuGroup child = null) : base(child)
        {
            Text = text;
            OnSelect = onSelect;

            SelectButton = button;
        }

        public override void Draw(RectangleF rectangle)
        {
            if (Child != null && Child.Active)
            {
                Child.Draw();
                return;
            }
            rectangle.DrawFillRectangle(Color.FromArgb(180, Color.Black));
            Drawing.DrawText(rectangle.Center.Offset(-Text.Length * 8, -10), Color.White, Text, 10);
        }

        public override bool Select()
        {

            if (OnSelect != null && Child == null)
                OnSelect.Invoke();
            else if (Child != null && Child.Active)
                return Child.SelectItem(SelectButton);
            else if (Child != null && !Child.Active)
            {
                Child.Active = true;
                if(Parent != null) Parent.Active = false;
                return false;
            }
            return OnSelect != null;
        }
    }

    #endregion

    #endregion
}

