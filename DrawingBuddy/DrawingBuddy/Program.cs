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
        public static List<KeyValuePair<Obj_AI_Base, Vector3[]>> ChampPaths;

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            ChampPaths = new List<KeyValuePair<Obj_AI_Base, Vector3[]>>();
            // Initialize the classes that we need
            Config.Initialize();
            // Listen to events we need
            EloBuddy.Obj_AI_Base.OnNewPath += Player_OnNewPath;
            Drawing.OnBeginScene += Drawing_OnBeginScene;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Drawing.OnDraw += Drawing_OnDraw;
            
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            DrawPaths();
        }

        private static void Drawing_OnBeginScene(EventArgs args)
        {
            //DrawPaths();
        }

        private static void Player_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (!sender.IsMinion && !sender.IsMonster)
            {
                if (ChampPaths.Any(x => x.Key == sender))
                    ChampPaths[ChampPaths.FindIndex(x => x.Key == sender)] =
                        new KeyValuePair<Obj_AI_Base, Vector3[]>(sender, args.Path);
                else
                    ChampPaths.Add(new KeyValuePair<Obj_AI_Base, Vector3[]>(sender, args.Path));
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            DrawTurrentRanges();
            DrawChampionRanges();
            
        }

        private static void DrawPaths()
        {
            foreach (var champ in ChampPaths)
            {
                if (Config.Movement.EnableAllyDrawings && champ.Key.IsAlly ||
                    Config.Movement.EnableEnemyDrawings && champ.Key.IsEnemy)
                    if (champ.Key.IsVisible && champ.Key.CanMove && champ.Key.IsMoving)
                    {
                        if (Config.Movement.CircleRadius > 0)
                            Circle.Draw(Config.Colors.MovementCircleColor.GetColor(), Config.Movement.CircleRadius,
                                champ.Value);
                        var points = champ.Value;
                        if (Config.Movement.LineThickness > 0)
                            for (int i = 0; i < points.Length; i++)
                            {
                                if (i - 1 >= 0)
                                {
                                    Line.DrawLine(Config.Colors.MovementLineColor.GetSystemColor(),
                                        Config.Movement.LineThickness, points[i - 1], points[i]);
                                }
                            }
                    }
            }
        }

        public static void DrawTurrentRanges()
        {
            foreach (var turret in EntityManager.Turrets.AllTurrets)
            {
                if (turret.IsEnemy || Config.Ranges.Turrents.ShowAlliedTurrents)
                    if (Player.IsInRange(turret, TurrentRange + Config.Ranges.Turrents.TurrentRangeDisplayOffset))
                        if (Config.Colors.DrawSmoothTurrentRange)
                            Drawing.DrawCircle(turret.Position, TurrentRange, Config.Colors.TurrentColor.GetSystemColor());
                        else
                            Circle.Draw(Config.Colors.TurrentColor.GetColor(), TurrentRange, turret.Position);
            }
        }
        public static void DrawChampionRanges()
        {
            foreach (var hero in EntityManager.Heroes.AllHeroes)
            {
                if (Config.Ranges.Champions.EnableRangesOfAllies && hero.IsAlly || Config.Ranges.Champions.EnableRangesOfEnemies && hero.IsEnemy)
                {
                    var heroconfig = Config.Ranges.Champions.HeroRanges.First(x => x.Key == hero);
                    for (int spell = 0; spell < SpellSlots.Length; spell++)
                    {
                        var getSpell = hero.Spellbook.GetSpell(SpellSlots[spell]);
                        if (heroconfig.Value.GetChecked(SpellSlots[spell]))
                        {
                            if (!Config.Ranges.Champions.OnlyShowRangesWhenReady || getSpell.IsReady && Config.Ranges.Champions.OnlyShowRangesWhenReady)
                            {
                                if (getSpell.SData.CastRangeDisplayOverride > 0)
                                    Circle.Draw(Config.Colors.SpellColors.First(x => x.Key == SpellSlots[spell]).Value.GetColor(),
                                        getSpell.SData.CastRangeDisplayOverride,
                                        hero.Position);
                                else if (getSpell.SData.CastRange < 25000)
                                    Circle.Draw(Config.Colors.SpellColors.First(x => x.Key == SpellSlots[spell]).Value.GetColor(), getSpell.SData.CastRange,
                                        hero.Position);
                            }
                        }
                    }
                }
            }
        }
    }
}
