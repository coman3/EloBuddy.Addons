using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace DrawingBuddy
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        private static readonly AIHeroClient Player = ObjectManager.Player;
        private static readonly float TurrentRange = 950f;
        public static SpellSlot[] SpellSlots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
        public static Dictionary<AIHeroClient, Vector3[]> ChampPaths;
        public static Circle MovementCircle;
        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            ChampPaths = new Dictionary<AIHeroClient, Vector3[]>();
            // Initialize the classes that we need
            Config.Initialize();
            MovementCircle = new Circle(Config.Colors.MovementCircleColor.GetColor(), Config.Movement.CircleRadius + 1, 3f, true);
            // Listen to events we need
            EloBuddy.Obj_AI_Base.OnNewPath += Player_OnNewPath;
             Drawing.OnBeginScene += Drawing_OnBeginScene;
            Drawing.OnEndScene += Drawing_OnEndScene;
            //Drawing.OnDraw += Drawing_OnDraw;
            
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
           
        }

        private static void Drawing_OnBeginScene(EventArgs args)
        {
            if (!Player.IsInShopRange())
            {
                DrawPaths();
            }
        }

        private static void Player_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            AIHeroClient key = sender as AIHeroClient;
            if (key != null && key.IsInRange(Player, 5000))
            {
                ChampPaths[key] = args.Path;
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (!Player.IsInShopRange())
            {
                //DrawPaths();
                DrawTurrentRanges();
                DrawChampionRanges();

            }

        }

        private static void DrawPaths()
        {
            foreach (var champ in ChampPaths)
            {
                if (Config.Movement.EnableAllyDrawings && champ.Key.IsAlly ||
                    Config.Movement.EnableEnemyDrawings && champ.Key.IsEnemy)
                    MovementCircle.Draw(champ.Value);
                var points = champ.Value;
                for (int i = 1; i < points.Length; i++)
                {
                    Drawing.DrawLine(Drawing.WorldToScreen(points[i - 1]), Drawing.WorldToScreen(points[i]),
                        Config.Movement.LineThickness, Config.Colors.MovementLineColor.GetSystemColor());
                    //Line.DrawLine(Config.Colors.MovementLineColor.GetSystemColor(), Config.Movement.LineThickness + 1,
                     // Drawing.WorldToScreen(points[i - 1]), Drawing.WorldToScreen(points[i]));
                }
            }

        }

        public static void DrawTurrentRanges()
        {
            foreach (var turret in EntityManager.Turrets.AllTurrets)
            {
                if (turret.IsEnemy || Config.Ranges.Turrents.ShowAlliedTurrents && !turret.IsDead)
                    if (Player.IsInRange(turret, TurrentRange + Config.Ranges.Turrents.TurrentRangeDisplayOffset))
                        if (Config.Colors.DrawSmoothTurrentRange)
                            Drawing.DrawCircle(turret.Position, TurrentRange, Config.Colors.TurrentColor.GetSystemColor());
                        else
                            Circle.Draw(Config.Colors.TurrentColor.GetColor(), TurrentRange, turret.Position);
            }
        }
        public static void DrawChampionRanges()
        {
            foreach (var hero in Config.Ranges.Champions.HeroRanges)
            {
                if (Config.Ranges.Champions.EnableRangesOfAllies && hero.Key.IsAlly || Config.Ranges.Champions.EnableRangesOfEnemies && hero.Key.IsEnemy)
                {
                    var heroconfig = hero.Value;
                    for (int spell = 0; spell < SpellSlots.Length; spell++)
                    {
                        var getSpell = hero.Key.Spellbook.GetSpell(SpellSlots[spell]);
                        if (heroconfig.GetChecked(SpellSlots[spell]))
                        {
                            if (!Config.Ranges.Champions.OnlyShowRangesWhenReady || getSpell.IsReady && Config.Ranges.Champions.OnlyShowRangesWhenReady)
                            {
                                if (getSpell.SData.CastRangeDisplayOverride > 0)
                                    Circle.Draw(Config.Colors.SpellColors[SpellSlots[spell]].GetColor(),
                                        getSpell.SData.CastRangeDisplayOverride,
                                        hero.Key.Position);
                                else if (getSpell.SData.CastRange < 25000)
                                    Circle.Draw(Config.Colors.SpellColors[SpellSlots[spell]].GetColor(),
                                        getSpell.SData.CastRange,
                                        hero.Key.Position);
                            }
                        }
                    }
                }
            }
        }
    }
}
