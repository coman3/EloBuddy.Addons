using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EzEvade.Config;
using EzEvade.Data;
using EzEvade.Draw;
using EzEvade.Helpers;
using EzEvade.Utils;

namespace EzEvade.EvadeSpells
{
    class EvadeSpell
    {
        private static AIHeroClient MyHero { get { return ObjectManager.Player; } }

        public delegate void Callback();

        public static List<EvadeSpellData> EvadeSpells = new List<EvadeSpellData>();
        public static List<EvadeSpellData> ItemSpells = new List<EvadeSpellData>();
        public static EvadeCommand LastSpellEvadeCommand = new EvadeCommand { IsProcessed = true, Timestamp = EvadeUtils.TickCount };

        public static Menu Menu;
        public static Menu EvadeSpellMenu;

        public EvadeSpell(Menu mainMenu)
        {
            Menu = mainMenu;

            //Game.OnUpdate += Game_OnGameUpdate;

            EvadeSpellMenu = Menu.AddSubMenu("Evade Spells", "EvadeSpells");

            LoadEvadeSpellList();
            Shop.OnBuyItem += delegate { CheckForItems(); };
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            //CheckDashing();
        }

        public static void CheckDashing()
        {
            if (EvadeUtils.TickCount - LastSpellEvadeCommand.Timestamp < 250 && MyHero.IsDashing()
                && LastSpellEvadeCommand.EvadeSpellData.EvadeType == EvadeType.Dash)
            {
                //Console.WriteLine("" + dashInfo.EndPos.Distance(lastSpellEvadeCommand.targetPosition));
                LastSpellEvadeCommand.TargetPosition = Prediction.Position.GetDashPos(Player.Instance);
            }
        }

        private static void CheckForItems()
        {
            foreach (var spell in ItemSpells)
            {
                var hasItem = Items.HasItem((int)spell.ItemId);

                if (hasItem && !EvadeSpells.Exists(s => s.SpellName == spell.SpellName))
                {
                    EvadeSpells.Add(spell);

                    CreateEvadeSpellMenu(spell);
                }
            }
        }

        private static Menu CreateEvadeSpellMenu(EvadeSpellData spell)
        {

            string menuName = spell.Name + " (" + spell.SpellKey + ") Settings";

            if (spell.IsItem)
            {
                menuName = spell.Name + " Settings";
            }
            var evadeSpellConfig = new EvadeSpellConfigControl(EvadeSpellMenu, menuName, spell);

            return evadeSpellConfig.GetMenu();
        }

        public static int GetDefaultSpellMode(EvadeSpellData spell)
        {
            if (spell.Dangerlevel > 3)
            {
                return 0;
            }

            return 1;
        }

        public static bool PreferEvadeSpell()
        {
            if (!Situation.ShouldUseEvadeSpell())
                return false;

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.Spells)
            {
                Spell spell = entry.Value;

                if (!GameData.HeroInfo.ServerPos2D.InSkillShot(spell, GameData.HeroInfo.BoundingRadius))
                    continue;

                if (ActivateEvadeSpell(spell, true))
                {
                    return true;
                }
            }

            return false;
        }

        public static void UseEvadeSpell()
        {
            if (!Situation.ShouldUseEvadeSpell())
            {
                return;
            }

            //int posDangerlevel = EvadeHelper.CheckPosDangerLevel(ObjectCache.myHeroCache.serverPos2D, 0);

            if (EvadeUtils.TickCount - LastSpellEvadeCommand.Timestamp < 1000)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.Spells)
            {
                Spell spell = entry.Value;

                if (ShouldActivateEvadeSpell(spell))
                {
                    if (ActivateEvadeSpell(spell))
                    {
                        AdEvade.SetAllUndodgeable();
                        return;
                    }
                }
            }

        }

        public static bool ActivateEvadeSpell(Spell spell, bool checkSpell = false)
        {
            var sortedEvadeSpells = EvadeSpells.OrderBy(s => s.Dangerlevel);

            var extraDelayBuffer = Properties.GetData<int>("ExtraPingBuffer");
            float spellActivationTime = Properties.GetData<int>("SpellActivationTime") + Game.Ping + extraDelayBuffer;

            if (Properties.GetData<bool>("CalculateWindupDelay"))
            {
                var extraWindupDelay = AdEvade.LastWindupTime - EvadeUtils.TickCount;
                if (extraWindupDelay > 0)
                {
                    return false;
                }
            }

            foreach (var evadeSpell in sortedEvadeSpells)
            {
                var processSpell = true;

                if (Properties.EvadeSpells[evadeSpell.Name].Use
                    || GetSpellDangerLevel(evadeSpell) > spell.GetSpellDangerLevel()
                    || (!evadeSpell.IsItem && !(MyHero.Spellbook.CanUseSpell(evadeSpell.SpellKey) == SpellState.Ready))
                    || (evadeSpell.IsItem && !(Items.CanUseItem((int)evadeSpell.ItemId)))
                    || (evadeSpell.CheckSpellName && MyHero.Spellbook.GetSpell(evadeSpell.SpellKey).Name != evadeSpell.SpellName))
           
                {
                    continue; //can't use spell right now               
                }


                float evadeTime, spellHitTime = 0;
                spell.CanHeroEvade(MyHero, out evadeTime, out spellHitTime);

                float finalEvadeTime = (spellHitTime - evadeTime);

                if (checkSpell)
                {
                    var mode = Properties.GetSpell(evadeSpell.Name).EvadeSpellMode;

                    switch (mode)
                    {
                        case 0:
                            continue;
                        case 1:
                            if (spellActivationTime < finalEvadeTime)
                            {
                                continue;
                            }
                            break;
                    }
                }
                else
                {
                    //if (ObjectCache.menuCache.cache[evadeSpell.name + "LastResort"].Cast<CheckBox>().CurrentValue)
                    if (evadeSpell.SpellDelay <= 50 && evadeSpell.EvadeType != EvadeType.Dash)
                    {
                        var path = MyHero.Path;
                        if (path.Length > 0)
                        {
                            var movePos = path[path.Length - 1].To2D();
                            var posInfo = EvadeHelper.CanHeroWalkToPos(movePos, GameData.HeroInfo.MoveSpeed, 0, 0);

                            if (GetSpellDangerLevel(evadeSpell) > posInfo.PosDangerLevel)
                            {
                                continue;
                            }
                        }
                    }
                }

                if (evadeSpell.EvadeType != EvadeType.Dash && spellHitTime > evadeSpell.SpellDelay + 100 + Game.Ping +
                    Properties.GetData<int>("ExtraPingBuffer"))
                {
                    processSpell = false;

                    if (checkSpell == false)
                    {
                        continue;
                    }
                }

                if (evadeSpell.IsSpecial == true)
                {
                    if (evadeSpell.UseSpellFunc != null)
                    {
                        if (evadeSpell.UseSpellFunc(evadeSpell, processSpell))
                        {
                            return true;
                        }
                    }

                    continue;
                }
                else if (evadeSpell.EvadeType == EvadeType.Blink)
                {
                    if (evadeSpell.CastType == CastType.Position)
                    {
                        var posInfo = EvadeHelper.GetBestPositionBlink();
                        if (posInfo != null)
                        {
                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, posInfo.Position), processSpell);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                    else if (evadeSpell.CastType == CastType.Target)
                    {
                        var posInfo = EvadeHelper.GetBestPositionTargetedDash(evadeSpell);
                        if (posInfo != null && posInfo.Target != null && posInfo.PosDangerLevel == 0)
                        {
                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, posInfo.Target), processSpell);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                }
                else if (evadeSpell.EvadeType == EvadeType.Dash)
                {
                    if (evadeSpell.CastType == CastType.Position)
                    {
                        var posInfo = EvadeHelper.GetBestPositionDash(evadeSpell);
                        if (posInfo != null && CompareEvadeOption(posInfo, checkSpell))
                        {
                            if (evadeSpell.IsReversed)
                            {
                                var dir = (posInfo.Position - GameData.HeroInfo.ServerPos2D).Normalized();
                                var range = GameData.HeroInfo.ServerPos2D.Distance(posInfo.Position);
                                var pos = GameData.HeroInfo.ServerPos2D - dir * range;

                                posInfo.Position = pos;
                            }

                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, posInfo.Position), processSpell);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                    else if (evadeSpell.CastType == CastType.Target)
                    {
                        var posInfo = EvadeHelper.GetBestPositionTargetedDash(evadeSpell);
                        if (posInfo != null && posInfo.Target != null && posInfo.PosDangerLevel == 0)
                        {
                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, posInfo.Target), processSpell);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                }
                else if (evadeSpell.EvadeType == EvadeType.WindWall)
                {
                    if (spell.HasProjectile() || evadeSpell.SpellName == "FioraW") //temp fix, don't have fiora :'(
                    {
                        var dir = (spell.StartPos - GameData.HeroInfo.ServerPos2D).Normalized();
                        var pos = GameData.HeroInfo.ServerPos2D + dir * 100;

                        CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, pos), processSpell);
                        return true;
                    }
                }
                else if (evadeSpell.EvadeType == EvadeType.SpellShield)
                {
                    if (evadeSpell.IsItem)
                    {
                        CastEvadeSpell(() => Items.UseItem((int)evadeSpell.ItemId), processSpell);
                        return true;
                    }
                    else
                    {
                        if (evadeSpell.CastType == CastType.Target)
                        {
                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell, MyHero), processSpell);
                            return true;
                        }
                        else if (evadeSpell.CastType == CastType.Self)
                        {
                            CastEvadeSpell(() => EvadeCommand.CastSpell(evadeSpell), processSpell);
                            return true;
                        }
                    }
                }
                else if (evadeSpell.EvadeType == EvadeType.MovementSpeedBuff)
                {
                    
                }
            }

            return false;
        }

        public static void CastEvadeSpell(Callback func, bool process = true)
        {
            if (process)
            {
                func();
            }
        }

        public static bool CompareEvadeOption(PositionInfo posInfo, bool checkSpell = false)
        {
            if (checkSpell)
            {
                if (posInfo.PosDangerLevel == 0)
                {
                    return true;
                }
            }

            return posInfo.IsBetterMovePos();
        }

        private static bool ShouldActivateEvadeSpell(Spell spell)
        {
            if (AdEvade.LastPosInfo == null)
                return false;

            if (Properties.Keys["DodgeSkillShots"].CurrentValue)
            {
                if (AdEvade.LastPosInfo.UndodgeableSpells.Contains(spell.SpellId)
                && GameData.HeroInfo.ServerPos2D.InSkillShot(spell, GameData.HeroInfo.BoundingRadius))
                {
                    return true;
                }
            }
            else
            {
                if (GameData.HeroInfo.ServerPos2D.InSkillShot(spell, GameData.HeroInfo.BoundingRadius))
                {
                    return true;
                }
            }


            /*float activationTime = Evade.menu.SubMenu("MiscSettings").SubMenu("EvadeSpellMisc").Item("EvadeSpellActivationTime")
                .Cast<Slider>().CurrentValue + ObjectCache.gamePing;

            if (spell.spellHitTime != float.MinValue && activationTime > spell.spellHitTime - spell.evadeTime)
            {
                return true;
            }*/

            return false;
        }

        public static int GetSpellDangerLevel(EvadeSpellData spell)
        {
            return Properties.GetSpell(spell.SpellName).DangerLevel;
        }

        private SpellSlot GetSummonerSlot(string spellName)
        {
            if (MyHero.Spellbook.GetSpell(SpellSlot.Summoner1).SData.Name == spellName)
            {
                return SpellSlot.Summoner1;
            }
            else if (MyHero.Spellbook.GetSpell(SpellSlot.Summoner2).SData.Name == spellName)
            {
                return SpellSlot.Summoner2;
            }

            return SpellSlot.Unknown;
        }

        private void LoadEvadeSpellList()
        {

            foreach (var spell in EvadeSpellDatabase.Spells.Where(
                s => (s.CharName == MyHero.ChampionName || s.CharName == Constants.AllChampions)))
            {

                if (spell.IsSummonerSpell)
                {
                    SpellSlot spellKey = GetSummonerSlot(spell.SpellName);
                    if (spellKey == SpellSlot.Unknown)
                    {
                        continue;
                    }
                    else
                    {
                        spell.SpellKey = spellKey;
                    }
                }

                if (spell.IsItem)
                {
                    ItemSpells.Add(spell);
                    continue;
                }

                if (spell.IsSpecial)
                {
                    SpecialEvadeSpell.LoadSpecialSpell(spell);
                }

                EvadeSpells.Add(spell);

                var newSpellMenu = CreateEvadeSpellMenu(spell);
            }

            EvadeSpells.Sort((a, b) => a.Dangerlevel.CompareTo(b.Dangerlevel));
        }
    }
}
