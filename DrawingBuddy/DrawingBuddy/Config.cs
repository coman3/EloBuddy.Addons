using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace DrawingBuddy
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "AddonTemplate";

        private static readonly Menu Menu;
        
        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to this AddonTemplate!");
            Menu.AddLabel("To change the menu, please have a look at the");
            Menu.AddLabel("Config.cs class inside the project, now have fun!");
            
        }

        public static void Initialize()
        {
            Movement.Initialize();
            Ranges.Initialize();
            Colors.Initialize();
        }

        public static class Movement
        {
            private static readonly Menu Menu;

            private static Slider _CircleRadius;
            private static Slider _LineThickness;
            private static CheckBox _EnableAllyMovementDrawing;
            private static CheckBox _EnableEnemyMovementDrawing;
            public static int CircleRadius { get { return _CircleRadius.CurrentValue; } }
            public static int LineThickness { get { return _LineThickness.CurrentValue; } }
            public static bool EnableAllyDrawings { get { return _EnableAllyMovementDrawing.CurrentValue; } }
            public static bool EnableEnemyDrawings { get { return _EnableEnemyMovementDrawing.CurrentValue; } }
            static Movement()
            {
                Menu = Config.Menu.AddSubMenu("Movement");
            }

            public static void Initialize()
            {
                Menu.AddGroupLabel("Movement");
                Menu.AddSeparator();
                _EnableAllyMovementDrawing = new CheckBox("Enable Ally Movement Drawings");
                Menu.Add("EnableAllyMovementDrawing", _EnableAllyMovementDrawing);
                _EnableEnemyMovementDrawing = new CheckBox("Enable Enemy Movement Drawings");
                Menu.Add("EnableEnemyMovementDrawing", _EnableEnemyMovementDrawing);
                _CircleRadius = new Slider("Radius of the movement turn circle", 25, 0, 100);
                Menu.Add("CircleRaduis", _CircleRadius);
                _LineThickness = new Slider("Thicknes of the movement line", 2, 0, 10);
                Menu.Add("LineThickness", _LineThickness);

            }
        }

        public static class Ranges
        {
            private static readonly Menu Menu;

            static Ranges()
            {
                Menu = Config.Menu.AddSubMenu("Ranges");
            }

            public static void Initialize()
            {
                Turrents.Initialize();
                Menu.AddSeparator(50);
                Champions.Initialize();
            }

            public static class Turrents
            {
                private static Slider _TurrentRangeDisplayOffset;
                private static CheckBox _ShowAlliedTurrents;

                public static int TurrentRangeDisplayOffset
                {
                    get { return _TurrentRangeDisplayOffset.CurrentValue; }
                }

                public static bool ShowAlliedTurrents
                {
                    get { return _ShowAlliedTurrents.CurrentValue; }
                }

                public static void Initialize()
                {
                    Menu.AddGroupLabel("Turrents");
                    _TurrentRangeDisplayOffset = new Slider("Turrents Range Display Offset", 500, 0, 1000);
                    Menu.Add("TurrentRangeDisplayOffset", _TurrentRangeDisplayOffset);
                    _ShowAlliedTurrents = new CheckBox("Display Allied Turrents");
                    Menu.Add("ShowAlliedTurrents", _ShowAlliedTurrents);
                }
            }

            public static class Champions
            {
                private static CheckBox _EnableRangesOfAllies;
                private static CheckBox _EnableRangesOfEnemies;
                private static CheckBox _OnlyShowRangesWhenReady;
                public static List<KeyValuePair<AIHeroClient, HeroSpellCheckBoxConfig>> HeroRanges;
                public static bool EnableRangesOfEnemies
                {
                    get { return _EnableRangesOfEnemies.CurrentValue; }
                }
                public static bool EnableRangesOfAllies
                {
                    get { return _EnableRangesOfAllies.CurrentValue; }
                }
                public static bool OnlyShowRangesWhenReady
                {
                    get { return _OnlyShowRangesWhenReady.CurrentValue; }
                }
                static Champions()
                {
                    HeroRanges = new List<KeyValuePair<AIHeroClient, HeroSpellCheckBoxConfig>>();
                }

                public static void Initialize()
                {
                    Menu.AddGroupLabel("Champions");
                    Menu.AddLabel("Enable or disable ranges of champions abliltys");
                    _OnlyShowRangesWhenReady = new CheckBox("Only show ranges of ablitlys when abilty is avaliable", true);
                    Menu.Add("OnlyShowRangesWhenReady", _OnlyShowRangesWhenReady);

                    Menu.AddGroupLabel("Allies");
                    _EnableRangesOfAllies = new CheckBox("Enable All Allies", false);
                    Menu.Add("EnableRangesOfAllies", _EnableRangesOfAllies);
                    foreach (var hero in EntityManager.Heroes.Allies)
                    {
                        Menu.AddGroupLabel(hero.ChampionName + " - " + hero.Name);
                        HeroRanges.Add(new KeyValuePair<AIHeroClient, HeroSpellCheckBoxConfig>(hero, new HeroSpellCheckBoxConfig(Menu, hero, "Show Range Of")));
                    }
                    Menu.AddSeparator(5);
                    Menu.AddGroupLabel("Enemies");

                    _EnableRangesOfEnemies = new CheckBox("Enable All Enemies", true);
                    Menu.Add("EnableRangesOfEnemies", _EnableRangesOfEnemies);
                    foreach (var hero in EntityManager.Heroes.Enemies)
                    {
                        Menu.AddGroupLabel(hero.ChampionName + " - " + hero.Name);
                        HeroRanges.Add(new KeyValuePair<AIHeroClient, HeroSpellCheckBoxConfig>(hero, new HeroSpellCheckBoxConfig(Menu, hero, "Show Range Of")));
                    }
                }
            }
        }

        public static class Colors
        {
            private static readonly Menu Menu;
            public static List<KeyValuePair<SpellSlot, ColorConfig>> SpellColors;
            public static ColorConfig TurrentColor;
            public static CheckBox TurrentSmoothCircle;

            public static ColorConfig MovementLineColor;
            public static ColorConfig MovementCircleColor;

            private static CheckBox HideMovement;
            private static CheckBox HideTurrents;
            private static CheckBox HideSpells;

            public static bool DrawSmoothTurrentRange { get { return TurrentSmoothCircle.CurrentValue; } }
            static Colors()
            {
                Menu = Config.Menu.AddSubMenu("Colors");
                SpellColors = new List<KeyValuePair<SpellSlot, ColorConfig>>();
                Game.OnTick += Game_OnTick;
            }

            private static void Game_OnTick(EventArgs args)
            {
                //TurrentColor.Hide();
            }

            public static void Initialize()
            {
                Menu.AddGroupLabel("Turrents");
                //HideTurrents = new CheckBox("Hide");
                //Menu.Add("HideTurrents", HideTurrents);
                TurrentColor = new ColorConfig(Menu, "TurrentsColor");
                TurrentSmoothCircle = new CheckBox("Enable Smooth Circle");
                Menu.Add("TurrentSmoothCircle", TurrentSmoothCircle);
                Menu.AddSeparator(50);

                Menu.AddGroupLabel("Movement");
                Menu.AddLabel("Line Color");
                MovementLineColor = new ColorConfig(Menu, "LineColor");
                Menu.AddLabel("Circle Color");
                MovementCircleColor = new ColorConfig(Menu, "CircleColor");

                Menu.AddSeparator(50);
                Menu.AddGroupLabel("Spells");
                foreach (var slot in Program.SpellSlots)
                {
                    Menu.AddLabel(Enum.GetName(typeof(SpellSlot), slot));
                    SpellColors.Add(new KeyValuePair<SpellSlot, ColorConfig>(slot,
                        new ColorConfig(Menu, "Spell" + Enum.GetName(typeof (SpellSlot), slot) + "Color"))); 
                    Menu.AddSeparator(10);
                }

                
            }
        }
    }
}
