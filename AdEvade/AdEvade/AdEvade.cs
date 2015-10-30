using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AdEvade.Config;
using AdEvade.Data;
using AdEvade.Data.EvadeSpells;
using AdEvade.Draw;
using AdEvade.Helpers;
using AdEvade.Testing;
using AdEvade.Utils;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using CheckBox = EloBuddy.SDK.Menu.Values.CheckBox;
using PositionInfo = AdEvade.Data.PositionInfo;
using SpellData = AdEvade.Data.Spells.SpellData;
using Debug = AdEvade.Draw.Debug;
using MainMenu = EloBuddy.SDK.Menu.MainMenu;
using Menu = EloBuddy.SDK.Menu.Menu;
using Spell = AdEvade.Data.Spells.Spell;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace AdEvade
{
    public class AdEvade
    {
        public const string LastUpdate = "3:09 PM Firday, 30 October 2015";
        public static SpellDetector SpellDetector;
        private static SpellDrawer _spellDrawer;
        //private static EvadeTester _evadeTester;
        private static EvadeSpell _evadeSpell;
        private static SpellTester _spellTester;
        private static AutoSetPing _autoSetPing;

        public static SpellSlot LastSpellCast;
        public static float LastSpellCastTime = 0;

        public static float LastWindupTime = 0;

        public static float LastTickCount = 0;
        public static float LastStopEvadeTime = 0;

        public static Vector3 LastMovementBlockPos = Vector3.Zero;
        public static float LastMovementBlockTime = 0;

        public static float LastEvadeOrderTime = 0;
        public static float LastIssueOrderGameTime = 0;
        public static float LastIssueOrderTime = 0;
        public static PlayerIssueOrderEventArgs LastIssueOrderArgs = null;

        public static Vector2 LastMoveToPosition = Vector2.Zero;
        public static Vector2 LastMoveToServerPos = Vector2.Zero;
        public static Vector2 LastStopPosition = Vector2.Zero;

        public static DateTime AssemblyLoadTime = DateTime.Now;

        public static bool IsDodging = false;
        public static bool DodgeOnlyDangerous = false;

        public static bool HasGameEnded = false;
        public static bool IsChanneling = false;
        public static Vector2 ChannelPosition = Vector2.Zero;

        public static Data.PositionInfo LastPosInfo;

        public static EvadeCommand LastEvadeCommand = new EvadeCommand
        {
            IsProcessed = true,
            Timestamp = EvadeUtils.TickCount
        };

        public static EvadeCommand LastBlockedUserMoveTo = new EvadeCommand
        {
            IsProcessed = true,
            Timestamp = EvadeUtils.TickCount
        };

        public static float LastDodgingEndTime = 0;

        public static Menu Menu;

        public static float SumCalculationTime = 0;
        public static float NumCalculationTime = 0;
        public static float AvgCalculationTime = 0;
        public AdEvade()
        {
            Load();
        }

        private void Load()
        {
            Loading.OnLoadingComplete += Game_OnGameLoad;
        }

        private void Game_OnGameLoad(EventArgs args)
        {
            //Console.Write("ezEvade loading....");

            try
            {

                Menu = MainMenu.AddMenu("AdEvade", "AdEvade");
                
                Menu.AddGroupLabel("AdEvade (EzEvade Port)");
                Menu.AddLabel("Please report any bugs or anything you think is a ");
                Menu.AddLabel("problem / issue, on the GitHub Issues Section");
                Menu.Add("OpenGithub", new CheckBox("Open Githubs Issues Section in browser", false)).OnValueChange +=
                    delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs changeArgs)
                    {
                        if (changeArgs.OldValue == false && changeArgs.NewValue)
                        {
                            sender.CurrentValue = false;
                            Process.Start(@"https://github.com/coman3/EloBuddy.Addons/issues");
                        }
                    };
                Menu.AddSeparator();
                Menu.AddLabel("All Credit for the actual evading (Movement and detection) in this assembly ");
                Menu.AddLabel("goes to the creator of EzEvade.");
                Menu.AddSeparator(100);

                Menu.AddLabel("Created By: Coman3");
                Menu.AddLabel("     Github: https://github.com/coman3/");
                Menu.Add("OpenGithubComan3", new CheckBox("Open Coman3's Github in Browser", false)).OnValueChange +=
                   delegate (ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs changeArgs)
                   {
                       if (changeArgs.OldValue == false && changeArgs.NewValue)
                       {
                           sender.CurrentValue = false;
                           Process.Start(@"https://github.com/coman3/");
                       }
                   };
                Menu.AddLabel("Last Update: " + LastUpdate);

                Menu mainMenu = Menu.AddSubMenu("Main", "Main");
                mainMenu.Add("DodgeSkillShots", new DynamicKeyBind("DodgeSkillShots", "Dodge SkillShots", true, KeyBind.BindTypes.PressToggle, 'K').KeyBind);
                mainMenu.Add("ActivateEvadeSpells", new DynamicKeyBind("ActivateEvadeSpells", "Use Evade Spells", true, KeyBind.BindTypes.PressToggle, 'K').KeyBind);
                mainMenu.Add("DodgeDangerous", new DynamicCheckBox(ConfigDataType.Data, "DodgeDangerous", "Dodge Only Dangerous", false).CheckBox);
                mainMenu.Add("DodgeFOWSpells", new DynamicCheckBox(ConfigDataType.Data, "DodgeFOWSpells", "Dodge FOW SkillShots", true).CheckBox);
                mainMenu.Add("DodgeCircularSpells", new DynamicCheckBox(ConfigDataType.Data, "DodgeCircularSpells", "Dodge Circular SkillShots", true).CheckBox);
                mainMenu.AddSeparator();
                mainMenu.Add("DodgeDangerousKeyEnabled", new DynamicCheckBox(ConfigDataType.Data, "DodgeDangerousKeyEnabled", "Enable Dodge Only Dangerous Keys", false).CheckBox);

                mainMenu.Add("DodgeDangerousKey", new DynamicKeyBind("DodgeDangerousKey", "Dodge Only Dangerous Key", false, KeyBind.BindTypes.HoldActive, 32).KeyBind);
                mainMenu.Add("DodgeDangerousKey2", new DynamicKeyBind("DodgeDangerousKey2", "Dodge Only Dangerous Key 2", false, KeyBind.BindTypes.HoldActive, 'V').KeyBind);

                mainMenu.AddSeparator();
                mainMenu.AddGroupLabel("Evade Mode");

                var sliderEvadeMode = mainMenu.Add("EvadeMode", new Slider("Smooth", 0, 0, 2));
                var modeArray = new[] { "Smooth", "Fastest", "Very Smooth" };

                sliderEvadeMode.DisplayName = modeArray[sliderEvadeMode.CurrentValue];
                sliderEvadeMode.OnValueChange +=
                    delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                    {
                        sender.DisplayName = modeArray[changeArgs.NewValue];
                        OnEvadeModeChange(sender, changeArgs);
                    };

                SpellDetector = new SpellDetector(Menu);
                _evadeSpell = new EvadeSpell(Menu);

                Menu miscMenu = Menu.AddSubMenu("Misc Settings", "MiscSettings");
                miscMenu.Add("HigherPrecision", new DynamicCheckBox(ConfigDataType.Data, "HigherPrecision", "Enhanced Dodge Precision", false).CheckBox);
                miscMenu.Add("RecalculatePosition", new DynamicCheckBox(ConfigDataType.Data, "RecalculatePosition", "Recalculate Path", true).CheckBox);
                miscMenu.Add("ContinueMovement", new DynamicCheckBox(ConfigDataType.Data, "ContinueMovement", "Continue Last Movement", true).CheckBox);
                miscMenu.Add("CalculateWindupDelay", new DynamicCheckBox(ConfigDataType.Data, "CalculateWindupDelay", "Calculate Windup Delay", true).CheckBox);
                miscMenu.Add("CheckSpellCollision", new DynamicCheckBox(ConfigDataType.Data, "CheckSpellCollision", "Check Spell Collision", false).CheckBox);
                miscMenu.Add("PreventDodgingUnderTower", new DynamicCheckBox(ConfigDataType.Data, "PreventDodgingUnderTower", "Prevent Dodging Under Tower", false).CheckBox);
                miscMenu.Add("PreventDodgingNearEnemy", new DynamicCheckBox(ConfigDataType.Data, "PreventDodgingNearEnemy", "Prevent Dodging Near Enemies", false).CheckBox);
                miscMenu.Add("AdvancedSpellDetection", new DynamicCheckBox(ConfigDataType.Data, "AdvancedSpellDetection", "Advanced Spell Detection", false).CheckBox);
                miscMenu.Add("ExtraDetectionRange", new DynamicSlider(ConfigDataType.Data, "ExtraDetectionRange", "Extra Detection / Drawing Range", 1000, 500, 5000).Slider);
                miscMenu.AddSeparator(100);
                miscMenu.AddGroupLabel("Reset");
                miscMenu.Add("ResetConfig", new DynamicCheckBox(ConfigDataType.Data, "ResetConfig", "Reset Properties", false).CheckBox);

                Menu fastEvadeMenu = Menu.AddSubMenu("Fast Evade", "FastEvade");
                fastEvadeMenu.Add("FastMovementBlock", new DynamicCheckBox(ConfigDataType.Data, "FastMovementBlock", "Fast Movement Block", false).CheckBox);
                fastEvadeMenu.Add("FastEvadeActivationTime", new DynamicSlider(ConfigDataType.Data, "FastEvadeActivationTime", "FastEvade Activation Time", 65, 0, 500).Slider);
                fastEvadeMenu.Add("SpellActivationTime", new DynamicSlider(ConfigDataType.Data, "SpellActivationTime", "Spell Activation Time", 200, 0, 1000).Slider);
                fastEvadeMenu.Add("RejectMinDistance", new DynamicSlider(ConfigDataType.Data, "RejectMinDistance", "Collision Distance Buffer", 10, 0, 100).Slider);

                Menu limiterMenu = Menu.AddSubMenu("Humanizer", "Limiter");
                limiterMenu.Add("ClickOnlyOnce", new DynamicCheckBox(ConfigDataType.Data, "ClickOnlyOnce", "Click Only Once", true).CheckBox);
                limiterMenu.Add("EnableEvadeDistance", new DynamicCheckBox(ConfigDataType.Data, "EnableEvadeDistance", "Extended Evade", false).CheckBox);
                limiterMenu.Add("TickLimiter", new DynamicSlider(ConfigDataType.Data, "TickLimiter", "Tick Limiter", 100, 0, 500).Slider);
                limiterMenu.Add("SpellDetectionTime", new DynamicSlider(ConfigDataType.Data, "SpellDetectionTime", "Spell Detection Time", 0, 0, 1000).Slider);
                limiterMenu.Add("ReactionTime", new DynamicSlider(ConfigDataType.Data, "ReactionTime", "Reaction Time", 0, 0, 500).Slider);
                limiterMenu.Add("DodgeInterval", new DynamicSlider(ConfigDataType.Data, "DodgeInterval", "Dodge Interval", 0, 0, 2000).Slider);
                

                Menu bufferMenu = Menu.AddSubMenu("Adv. Humanizer", "ExtraBuffers");
                bufferMenu.Add("ExtraPingBuffer", new DynamicSlider(ConfigDataType.Data, "ExtraPingBuffer", "Extra Ping Buffer", 65, 0, 200).Slider);
                bufferMenu.Add("ExtraCPADistance", new DynamicSlider(ConfigDataType.Data, "ExtraCPADistance", "Extra Collision Distance", 10, 0, 150).Slider);
                bufferMenu.Add("ExtraSpellRadius", new DynamicSlider(ConfigDataType.Data, "ExtraSpellRadius", "Extra Spell Radius", 0, 0, 100).Slider);
                bufferMenu.Add("ExtraEvadeDistance", new DynamicSlider(ConfigDataType.Data, "ExtraEvadeDistance", "Extra Evade Distance", 100, 0, 300).Slider);
                bufferMenu.Add("ExtraAvoidDistance", new DynamicSlider(ConfigDataType.Data, "ExtraAvoidDistance", "Extra Avoid Distance", 50, 0, 300).Slider);
                bufferMenu.Add("MinComfortZone", new DynamicSlider(ConfigDataType.Data, "MinComfortZone", "Min Distance to Champion", 550, 0, 1000).Slider);

                Menu debugMenu = Menu.AddSubMenu("Debug", "DebugMenu");

                debugMenu.AddGroupLabel("Debug");
                debugMenu.Add("ShowDebugForm", new CheckBox("Show Debug / Config Data Form", false)).OnValueChange +=
                    delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs changeArgs)
                    {
                        if (!changeArgs.OldValue && changeArgs.NewValue)
                        {
                            Debug.ShowValueForm();
                            sender.CurrentValue = false;
                        }
                    };
                debugMenu.AddSeparator();
                debugMenu.Add("DebugShow", new DynamicCheckBox(ConfigDataType.Data, "DebugShow", "Show Debug Info", false).CheckBox);
                debugMenu.Add("DebugWithMySpells", new DynamicCheckBox(ConfigDataType.Data, "DebugWithMySpells", "Detect and draw my spells", false).CheckBox);

                debugMenu.AddSeparator();
                debugMenu.Add("EnableSpellTester",
                    new DynamicCheckBox(ConfigDataType.Data, "EnableSpellTester", "Enable Spell Tester", false).CheckBox);
                debugMenu.AddLabel("Press F5 after enabling / disabling the Spell Tester to load / unload it.");
                _spellDrawer = new SpellDrawer(Menu);
                _spellTester = new SpellTester(Menu);

                Debug.DrawTopLeft("Showing Debug info...");


                Player.OnIssueOrder += Game_OnIssueOrder;
                Spellbook.OnCastSpell += Game_OnCastSpell;
                Game.OnUpdate += Game_OnGameUpdate;

                AIHeroClient.OnProcessSpellCast += Game_OnProcessSpell;

                Game.OnEnd += Game_OnGameEnd;
                SpellDetector.OnProcessDetectedSpells += SpellDetector_OnProcessDetectedSpells;
                Orbwalker.OnPreAttack += Orbwalking_BeforeAttack;

                Chat.Print("AdEvade Loaded");


            }
            catch (Exception e)
            {
                Chat.Print(e);
            }
        }

        private void OnEvadeModeChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
        {
            var mode = sender.DisplayName;

            if (mode == "Very Smooth")
            {
                Config.Properties.SetData("FastEvadeActivationTime", 0);
                Config.Properties.SetData("RejectMinDistance", 0);
                Config.Properties.SetData("ExtraCPADistance", 0);
                Config.Properties.SetData("ExtraPingBuffer", 40);
            }
            else if (mode == "Smooth")
            {
                Config.Properties.SetData("FastEvadeActivationTime", 65);
                Config.Properties.SetData("RejectMinDistance",10);
                Config.Properties.SetData("ExtraCPADistance",10);
                Config.Properties.SetData("ExtraPingBuffer", 65);
            }
        }

        private void Game_OnGameEnd(GameEndEventArgs args)
        {
            HasGameEnded = true;
        }

        private void Game_OnCastSpell(Spellbook spellbook, SpellbookCastSpellEventArgs args)
        {
            if (!spellbook.Owner.IsMe)
                return;

            var sData = spellbook.GetSpell(args.Slot);
            string name;

            if (SpellDetector.ChanneledSpells.TryGetValue(sData.Name, out name))
            {
                //Evade.isChanneling = true;
                //Evade.channelPosition = GameData.HeroInfo.serverPos2D;
                LastStopEvadeTime = EvadeUtils.TickCount + Game.Ping + 100;
            }

            if (EvadeSpell.LastSpellEvadeCommand != null &&
                EvadeSpell.LastSpellEvadeCommand.Timestamp + Game.Ping + 150 > EvadeUtils.TickCount)
            {
                args.Process = false;
            }

            LastSpellCast = args.Slot;
            LastSpellCastTime = EvadeUtils.TickCount;

            //moved from processPacket

            /*if (args.Slot == SpellSlot.Recall)
            {
                lastStopPosition = GameData.MyHero.ServerPosition.To2D();
            }*/

            if (Situation.ShouldDodge())
            {
                if (IsDodging && SpellDetector.Spells.Count() > 0)
                {
                    foreach (KeyValuePair<String, SpellData> entry in SpellDetector.WindupSpells)
                    {
                        SpellData spellData = entry.Value;

                        if (spellData.SpellKey == args.Slot) //check if it's a spell that we should block
                        {
                            args.Process = false;
                            return;
                        }
                    }
                }
            }

            foreach (var evadeSpell in EvadeSpell.EvadeSpells)
            {
                if (evadeSpell.IsItem == false && evadeSpell.SpellKey == args.Slot)
                {
                    if (evadeSpell.EvadeType == EvadeType.Blink
                        || evadeSpell.EvadeType == EvadeType.Dash)
                    {
                        //Block spell cast if flashing/blinking into spells
                        if (args.EndPosition.To2D().CheckDangerousPos(6, true)) //for blink + dash
                        {
                            args.Process = false;
                            return;
                        }

                        if (evadeSpell.EvadeType == EvadeType.Dash)
                        {
                            var extraDelayBuffer = Config.Properties.GetData<int>("ExtraPingBuffer");
                            var extraDist = Config.Properties.GetData<int>("ExtraCPADistance");

                            var dashPos = Game.CursorPos.To2D(); //real pos?

                            if (evadeSpell.FixedRange)
                            {
                                var dir = (dashPos - GameData.MyHero.ServerPosition.To2D()).Normalized();
                                dashPos = GameData.MyHero.ServerPosition.To2D() + dir * evadeSpell.Range;
                            }

                            //Draw.RenderObjects.Add(new Draw.RenderPosition(dashPos, 1000));

                            var posInfo = EvadeHelper.CanHeroWalkToPos(dashPos, evadeSpell.Speed,
                                extraDelayBuffer + Game.Ping, extraDist);

                            if (posInfo.PosDangerLevel > 0)
                            {
                                args.Process = false;
                                return;
                            }
                        }

                        LastPosInfo = PositionInfo.SetAllUndodgeable(); //really?

                        if (IsDodging || EvadeUtils.TickCount < LastDodgingEndTime + 500)
                        {
                            EvadeCommand.MoveTo(Game.CursorPos.To2D()); //block moveto
                            LastStopEvadeTime = EvadeUtils.TickCount + Game.Ping + 100;
                        }
                    }
                    return;
                }
            }
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, PlayerIssueOrderEventArgs args)
        {

            if (!hero.IsMe)
                return;

            if (!Situation.ShouldDodge())
                return;

            if (args.Order == GameObjectOrder.MoveTo)
            {
                //movement block code goes in here
                if (IsDodging && SpellDetector.Spells.Count > 0)
                {
                    CheckHeroInDanger();

                    LastBlockedUserMoveTo = new EvadeCommand
                    {
                        Order = EvadeOrderCommand.MoveTo,
                        TargetPosition = args.TargetPosition.To2D(),
                        Timestamp = EvadeUtils.TickCount,
                        IsProcessed = false,
                    };

                    args.Process = false; //Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    var extraDelay = Config.Properties.GetData<int>("ExtraPingBuffer");
                    if (EvadeHelper.CheckMovePath(movePos, Game.Ping + extraDelay))
                    {
                        /*if (() Properties.Properties.Data["AllowCrossing"].Cast<CheckBox>().CurrentValue)
                        {
                            var extraDelayBuffer = () Properties.Properties.Data["ExtraPingBuffer"]
                                 + 30;
                            var extraDist = () Properties.Properties.Data["ExtraCPADistance"]
                                 + 10;

                            var tPosInfo = EvadeHelper.CanHeroWalkToPos(movePos, GameData.HeroInfo.moveSpeed, extraDelayBuffer + Game.Ping, extraDist);

                            if (tPosInfo.posDangerLevel == 0)
                            {
                                lastPosInfo = tPosInfo;
                                return;
                            }
                        }*/

                        LastBlockedUserMoveTo = new EvadeCommand
                        {
                            Order = EvadeOrderCommand.MoveTo,
                            TargetPosition = args.TargetPosition.To2D(),
                            Timestamp = EvadeUtils.TickCount,
                            IsProcessed = false,
                        };

                        args.Process = false; //Block the command

                        if (EvadeUtils.TickCount - LastMovementBlockTime < 500 &&
                            LastMovementBlockPos.Distance(args.TargetPosition) < 100)
                        {
                            return;
                        }

                        LastMovementBlockPos = args.TargetPosition;
                        LastMovementBlockTime = EvadeUtils.TickCount;

                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            EvadeCommand.MoveTo(posInfo.Position);
                        }
                        return;
                    }
                    else
                    {
                        LastBlockedUserMoveTo.IsProcessed = true;
                    }
                }
            }
            else //need more logic
            {
                if (IsDodging)
                {
                    args.Process = false; //Block the command
                }
                else
                {
                    if (args.Order == GameObjectOrder.AttackUnit)
                    {
                        var target = args.Target;
                        if (target != null && target.GetType() == typeof(Obj_AI_Base) && ((Obj_AI_Base)target).IsValid())
                        {
                            var baseTarget = target as Obj_AI_Base;
                            if (GameData.HeroInfo.ServerPos2D.Distance(baseTarget.ServerPosition.To2D()) >
                                GameData.MyHero.AttackRange + GameData.HeroInfo.BoundingRadius + baseTarget.BoundingRadius)
                            {
                                var movePos = args.TargetPosition.To2D();
                                var extraDelay = Config.Properties.GetData<int>("ExtraPingBuffer");
                                if (EvadeHelper.CheckMovePath(movePos, Game.Ping + extraDelay))
                                {
                                    args.Process = false; //Block the command
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            if (args.Process == true)
            {
                LastIssueOrderGameTime = Game.Time * 1000;
                LastIssueOrderTime = EvadeUtils.TickCount;
                LastIssueOrderArgs = args;

                if (args.Order == GameObjectOrder.MoveTo)
                {
                    LastMoveToPosition = args.TargetPosition.To2D();
                    LastMoveToServerPos = GameData.MyHero.ServerPosition.To2D();
                }

                if (args.Order == GameObjectOrder.Stop)
                {
                    LastStopPosition = GameData.MyHero.ServerPosition.To2D();
                }
            }
        }

        private void Orbwalking_BeforeAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (IsDodging)
            {
                args.Process = false; //Block orbwalking
            }
        }

        private void Game_OnProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (!hero.IsMe)
            {
                return;
            }

            /*if (args.SData.Name.Contains("Recall"))
            {
                var distance = lastStopPosition.Distance(args.Start.To2D());
                float moveTime = 1000 * distance / GameData.MyHero.MoveSpeed;

                Console.WriteLine("Extra dist: " + distance + " Extra Delay: " + moveTime);
            }*/

            string name;
            if (SpellDetector.ChanneledSpells.TryGetValue(args.SData.Name, out name))
            {
                IsChanneling = true;
                ChannelPosition = GameData.MyHero.ServerPosition.To2D();
            }
            if (Config.Properties.GetData<bool>("CalculateWindupDelay"))
            {
                var castTime = (hero.Spellbook.CastTime - Game.Time) * 1000;

                if (castTime > 0 && !EloBuddy.SDK.Constants.AutoAttacks.IsAutoAttack(args.SData.Name)
                    && Math.Abs(castTime - GameData.MyHero.AttackCastDelay * 1000) > 1)
                {
                    LastWindupTime = EvadeUtils.TickCount + castTime - Game.Ping / 2;
                    if (IsDodging)
                    {
                        SpellDetector_OnProcessDetectedSpells(); //reprocess
                    }
                }
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            try
            {
                CheckHeroInDanger();

                if (IsChanneling && ChannelPosition.Distance(GameData.HeroInfo.ServerPos2D) > 50
                    ) //TODO: !GameData.MyHero.IsChannelingImportantSpell()
                {
                    IsChanneling = false;
                }

                //if (() Properties.Properties.Data["ResetConfig"].Cast<CheckBox>().CurrentValue)
                //{
                //    ResetConfig();
                //    menu["ResetConfig"].Cast<CheckBox>().CurrentValue = false;
                //}

                //if (() Properties.Properties.Data["ResetConfig200"].Cast<CheckBox>().CurrentValue)
                //{
                //    SetPatchConfig();
                //    menu["ResetConfig200"].Cast<CheckBox>().CurrentValue = false;
                //}

                var limitDelay = Config.Properties.GetData<int>("TickLimiter");
                //Tick limiter                
                if (EvadeUtils.TickCount - LastTickCount > limitDelay
                    && EvadeUtils.TickCount > LastStopEvadeTime)
                {
                    DodgeSkillShots(); //walking           

                    ContinueLastBlockedCommand();
                    LastTickCount = EvadeUtils.TickCount;
                }

                EvadeSpell.UseEvadeSpell(); //using spells
                CheckDodgeOnlyDangerous();
                RecalculatePath();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void RecalculatePath()
        {
            if (Config.Properties.GetData<bool>("RecalculatePosition") && IsDodging) //recheck path
            {
                if (LastPosInfo != null && !LastPosInfo.RecalculatedPath)
                {
                    var path = GameData.MyHero.Path;
                    if (path.Length > 0)
                    {
                        var movePos = path.Last().To2D();

                        if (movePos.Distance(LastPosInfo.Position) < 5) //more strict checking
                        {
                            var posInfo = EvadeHelper.CanHeroWalkToPos(movePos, GameData.HeroInfo.MoveSpeed, 0, 0,
                                false);
                            if (posInfo.PosDangerCount > LastPosInfo.PosDangerCount)
                            {
                                LastPosInfo.RecalculatedPath = true;

                                if (EvadeSpell.PreferEvadeSpell())
                                {
                                    LastPosInfo = PositionInfo.SetAllUndodgeable();
                                }
                                else
                                {
                                    var newPosInfo = EvadeHelper.GetBestPosition();
                                    if (newPosInfo.PosDangerCount < posInfo.PosDangerCount)
                                    {
                                        LastPosInfo = newPosInfo;
                                        CheckHeroInDanger();
                                        DodgeSkillShots();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ContinueLastBlockedCommand()
        {
            if (Config.Properties.GetData<bool>("ContinueMovement") && Situation.ShouldDodge())
            {
                var movePos = LastBlockedUserMoveTo.TargetPosition;
                var extraDelay = Config.Properties.GetData<int>("ExtraPingBuffer");

                if (IsDodging == false && LastBlockedUserMoveTo.IsProcessed == false
                    && EvadeUtils.TickCount - LastEvadeCommand.Timestamp > Game.Ping + extraDelay
                    && EvadeUtils.TickCount - LastBlockedUserMoveTo.Timestamp < 1500)
                {
                    movePos = movePos + (movePos - GameData.HeroInfo.ServerPos2D).Normalized()
                              * EvadeUtils.Random.NextFloat(1, 65);

                    if (!EvadeHelper.CheckMovePath(movePos, Game.Ping + extraDelay))
                    {
                        //Console.WriteLine("Continue Movement");
                        //GameData.MyHero.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D());
                        EvadeCommand.MoveTo(movePos);
                        LastBlockedUserMoveTo.IsProcessed = true;
                    }
                }
            }
        }

        private void CheckHeroInDanger()
        {
            bool playerInDanger = false;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.Spells)
            {
                Spell spell = entry.Value;

                if (LastPosInfo != null) //&& lastPosInfo.dodgeableSpells.Contains(spell.spellID))
                {
                    if (GameData.MyHero.ServerPosition.To2D().InSkillShot(spell, GameData.HeroInfo.BoundingRadius))
                    {
                        playerInDanger = true;
                        break;
                    }

                    if (Config.Properties.GetData<bool>("EnableEvadeDistance") && EvadeUtils.TickCount < LastPosInfo.EndTime)
                    {
                        playerInDanger = true;
                        break;
                    }
                }
            }

            if (IsDodging && !playerInDanger)
            {
                LastDodgingEndTime = EvadeUtils.TickCount;
            }

            if (IsDodging == false && !Situation.ShouldDodge())
                return;

            IsDodging = playerInDanger;
        }

        private void DodgeSkillShots()
        {
            if (!Situation.ShouldDodge())
            {
                IsDodging = false;
                return;
            }

            /*
            if (isDodging && playerInDanger == false) //serverpos test
            {
                GameData.MyHero.IssueOrder(GameObjectOrder.HoldPosition, myHero, false);
            }*/

            if (IsDodging)
            {

                if (LastPosInfo != null)
                {

                    /*foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                    {
                        Spell spell = entry.Value;

                        Console.WriteLine("" + (int)(TickCount-spell.startTime));
                    }*/


                    Vector2 lastBestPosition = LastPosInfo.Position;

                    if (Config.Properties.GetData<bool>("ClickOnlyOnce") == false
                        || !(GameData.MyHero.Path.Count() > 0 && LastPosInfo.Position.Distance(GameData.MyHero.Path.Last().To2D()) < 5))
                    //|| lastPosInfo.timestamp > lastEvadeOrderTime)
                    {
                        EvadeCommand.MoveTo(lastBestPosition);
                        LastEvadeOrderTime = EvadeUtils.TickCount;
                    }
                }
            }
            else //if not dodging
            {
                //Check if hero will walk into a skillshot
                var path = GameData.MyHero.Path;
                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();

                    if (EvadeHelper.CheckMovePath(movePos))
                    {
                        /*if (() Properties.Properties.Data["AllowCrossing"].Cast<CheckBox>().CurrentValue)
                        {
                            var extraDelayBuffer = () Properties.Properties.Data["ExtraPingBuffer"]
                                 + 30;
                            var extraDist = () Properties.Properties.Data["ExtraCPADistance"]
                                 + 10;

                            var tPosInfo = EvadeHelper.CanHeroWalkToPos(movePos, GameData.HeroInfo.moveSpeed, extraDelayBuffer + Game.Ping, extraDist);

                            if (tPosInfo.posDangerLevel == 0)
                            {
                                lastPosInfo = tPosInfo;
                                return;
                            }
                        }*/

                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            EvadeCommand.MoveTo(posInfo.Position);
                        }
                        return;
                    }
                }
            }
        }

        public void CheckLastMoveTo()
        {
            if (Config.Properties.GetData<bool>("FastMovementBlock"))
            {
                if (IsDodging == false && LastIssueOrderArgs != null
                    && LastIssueOrderArgs.Order == GameObjectOrder.MoveTo
                    && Game.Time * 1000 - LastIssueOrderGameTime < 500)
                {
                    Game_OnIssueOrder(GameData.MyHero, LastIssueOrderArgs);
                    LastIssueOrderArgs = null;
                }
            }
        }

        public static bool IsDodgeDangerousEnabled()
        {
            if (Config.Properties.GetData<bool>("DodgeDangerous"))
            {
                return true;
            }

            if (Config.Properties.GetData<bool>("DodgeDangerousKeyEnabled"))
            {
                if (Config.Properties.Keys["DodgeDangerousKey"].CurrentValue || Config.Properties.Keys["DodgeDangerousKey2"].CurrentValue)
                    return true;
            }

            return false;
        }

        public static void CheckDodgeOnlyDangerous() //Dodge only dangerous event
        {
            bool bDodgeOnlyDangerous = IsDodgeDangerousEnabled();

            if (DodgeOnlyDangerous == false && bDodgeOnlyDangerous)
            {
                SpellDetector.RemoveNonDangerousSpells();
                DodgeOnlyDangerous = true;
            }
            else
            {
                DodgeOnlyDangerous = bDodgeOnlyDangerous;
            }
        }

        public static void SetAllUndodgeable()
        {
            LastPosInfo = PositionInfo.SetAllUndodgeable();
        }

        private void SpellDetector_OnProcessDetectedSpells()
        {
            GameData.HeroInfo.UpdateInfo();
            if (!Config.Properties.Keys["DodgeSkillShots"].CurrentValue)
            {
                LastPosInfo = PositionInfo.SetAllUndodgeable();
                EvadeSpell.UseEvadeSpell();
                return;
            }
            if (GameData.HeroInfo.ServerPos2D.CheckDangerousPos(0)
                || GameData.HeroInfo.ServerPos2DExtra.CheckDangerousPos(0))
            {
                if (EvadeSpell.PreferEvadeSpell())
                {
                    LastPosInfo = PositionInfo.SetAllUndodgeable();
                }
                else
                {
                    var calculationTimer = EvadeUtils.TickCount;

                    var posInfo = EvadeHelper.GetBestPosition();

                    var caculationTime = EvadeUtils.TickCount - calculationTimer;

                    if (NumCalculationTime > 0)
                    {
                        SumCalculationTime += caculationTime;
                        AvgCalculationTime = SumCalculationTime / NumCalculationTime;
                    }
                    NumCalculationTime += 1;

                    //Console.WriteLine("CalculationTime: " + caculationTime);

                    /*if (EvadeHelper.GetHighestDetectedSpellID() > EvadeHelper.GetHighestSpellID(posInfo))
                    {
                        return;
                    }*/
                    if (posInfo != null)
                    {
                        LastPosInfo = posInfo.CompareLastMovePos();

                        var travelTime = GameData.HeroInfo.ServerPos2DPing.Distance(LastPosInfo.Position) /
                                         GameData.MyHero.MoveSpeed;

                        LastPosInfo.EndTime = EvadeUtils.TickCount + travelTime * 1000 - 100;
                    }

                    CheckHeroInDanger();
                    DodgeSkillShots(); //walking
                    CheckLastMoveTo();
                    EvadeSpell.UseEvadeSpell(); //using spells
                }
            }
            else
            {
                LastPosInfo = PositionInfo.SetAllDodgeable();
                CheckLastMoveTo();
            }


            //Console.WriteLine("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.undodgeableSpells.Count);            
        }
    }
}