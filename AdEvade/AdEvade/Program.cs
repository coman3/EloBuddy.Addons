using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.Sandbox;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX.Direct3D9;
using Coman3.API.Champion;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Color = System.Drawing.Color;
using Sprite = EloBuddy.SDK.Rendering.Sprite;

namespace AdEvade
{
    public static class Program
    {
        //public static AdEvade Evade;
        public static Sprite PlayerIcon;
        public static IconGenerator Generator;
        public static Menu Menu;
        static void Main(string[] args)
        {
            Generator = new IconGenerator(IconGenerator.IconType.Square, 64, 64, Color.DarkBlue, 2);
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(PlayerIcon == null) return;
            //PlayerIcon.Draw(Player.Instance.HPBarPosition);
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Menu = MainMenu.AddMenu("AdEvadeTest", "Test AdEvade");
            PlayerIcon = new Sprite(TextureLoader.BitmapToTexture(Generator.GetChampionIcon(Player.Instance.ChampionName)));

            Menu.AddLabel("Test");
            Menu.Add(Guid.NewGuid().ToString(), new SpriteControl(PlayerIcon));
            Menu.AddSeparator(100);
            new PlayerEvadeControl(PlayerIcon).Add(Menu);
            Menu.AddLabel("Test");

            Chat.Print("Loaded!");
            Drawing.OnEndScene += Drawing_OnDraw;
        }
    }

    class PlayerEvadeControl
    {
        public SpriteControl SpriteControl { get; set; }
        public CheckBox CheckBox { get; set; }

        public PlayerEvadeControl(Sprite playerIcon)
        {
            SpriteControl = new SpriteControl(playerIcon);
            CheckBox = new CheckBox("TestCheckbox");
        }

        public void Add(Menu menu)
        {
            menu.Add("TestIcon", SpriteControl);
            menu.AddSeparator(64);
            menu.Add("TestCheckbox", CheckBox);
        }
    }
    class SpriteControl : ValueBase<bool>
    {
        public Sprite Icon;
        public SpriteControl(Sprite sprite) : base(Guid.NewGuid().ToString(), "", 64)
        {
            Icon = sprite;
        }

        public override string VisibleName { get { return ""; } }

        public override Vector2 Offset
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public override bool CurrentValue { get { return false; } set {} }

        public override bool Draw()
        {
            
            if(Icon != null) Icon.Draw(Position);
            return true;
        }
    }
}
