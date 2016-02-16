using System;
using System.Collections.Generic;
using AdEvade.Config;
using AdEvade.Config.Controls;
using AdEvade.Data;
using AdEvade.Data.Spells;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
using Spell = AdEvade.Data.Spells.Spell;

namespace AdEvade.Draw
{
    internal class SpellDrawer
    {
        public static Menu Menu;

        private static AIHeroClient MyHero { get { return ObjectManager.Player; } }
        public ColorPicker[] DangerColorPickers { get; set; }

        public SpellDrawer(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Menu = mainMenu;
            DangerColorPickers = new ColorPicker[4];
            Game_OnGameLoad();

            
        }

        private void Game_OnGameLoad()
        {
            //ConsoleDebug.WriteLine("SpellDrawer loaded");
            

            Menu drawMenu = Menu.IsSubMenu ? Menu.Parent.AddSubMenu("Draw", "Draw") : Menu.AddSubMenu("Draw", "Draw");
            drawMenu.Add(ConfigValue.DrawSkillShots.Name(), new DynamicCheckBox(ConfigDataType.Data, ConfigValue.DrawSkillShots, "Draw SkillShots", true).CheckBox);
            drawMenu.Add(ConfigValue.DrawEvadeStatus.Name(), new DynamicCheckBox(ConfigDataType.Data, ConfigValue.DrawEvadeStatus, "Show Evade Status", true).CheckBox);
            drawMenu.Add(ConfigValue.DrawSpellPosition.Name(), new DynamicCheckBox(ConfigDataType.Data, ConfigValue.DrawSpellPosition, "Draw Spell Position", false).CheckBox);
            drawMenu.Add(ConfigValue.DrawEvadePosition.Name(), new DynamicCheckBox(ConfigDataType.Data, ConfigValue.DrawEvadePosition, "Draw Evade Position", false).CheckBox);

            Menu dangerMenu = drawMenu.Parent.AddSubMenu("Danger Drawings", "DangerLevelDrawings");

            Menu lowDangerMenu = dangerMenu.Parent.AddSubMenu(" Low", "LowDrawing");
            lowDangerMenu.Add(ConfigValue.LowDangerDrawWidth.Name(), new DynamicSlider(ConfigDataType.Data, ConfigValue.LowDangerDrawWidth, "Line Width", 3, 1, 15).Slider);
            lowDangerMenu.AddGroupLabel("Color");
            DangerColorPickers[(int)SpellDangerLevel.Low] = lowDangerMenu.Add("LowDangerColorConfig", new ColorPicker("Low Danger Line Color", Color.LightGray));
            Menu normalDangerMenu = dangerMenu.Parent.AddSubMenu(" Normal", "NormalDrawing");
            normalDangerMenu.Add(ConfigValue.NormalDangerDrawWidth.Name(), new DynamicSlider(ConfigDataType.Data, ConfigValue.NormalDangerDrawWidth, "Line Width", 3, 1, 15).Slider);
            normalDangerMenu.AddGroupLabel("Color");
            DangerColorPickers[(int)SpellDangerLevel.Normal] = normalDangerMenu.Add("NormalDangerColorConfig", new ColorPicker("Normal Danger Line Color", Color.White));
            
            Menu highDangerMenu = dangerMenu.Parent.AddSubMenu(" High", "HighDrawing");
            highDangerMenu.Add(ConfigValue.HighDangerDrawWidth.Name(), new DynamicSlider(ConfigDataType.Data, ConfigValue.HighDangerDrawWidth, "Line Width", 4, 1, 15).Slider);
            highDangerMenu.AddGroupLabel("Color");
            DangerColorPickers[(int) SpellDangerLevel.High] = highDangerMenu.Add("HighDangerColorConfig", new ColorPicker("High Danger Line Color", Color.DarkOrange));

            Menu extremeDangerMenu = dangerMenu.Parent.AddSubMenu(" Extreme", "ExtremeDrawing");
            extremeDangerMenu.Add(ConfigValue.ExtremeDangerDrawWidth.Name(), new DynamicSlider(ConfigDataType.Data, ConfigValue.ExtremeDangerDrawWidth, "Line Width", 4, 1, 15).Slider);
            extremeDangerMenu.AddGroupLabel("Color");
            DangerColorPickers[(int) SpellDangerLevel.Extreme] = extremeDangerMenu.Add("ExtremeDangerColorConfig", new ColorPicker("Extreme Danger Line Color", Color.OrangeRed));
            /*
            Menu undodgeableDangerMenu = new Menu("Undodgeable", "Undodgeable");
            undodgeableDangerMenu.AddItem(new MenuItem("Width", "Line Width").SetValue(new Slider(6, 1, 15)));
            undodgeableDangerMenu.AddItem(new MenuItem("Color", "Color").SetValue(new Circle(true, Color.FromArgb(255, 255, 0, 0))));*/
        }

        private void DrawLineRectangle(Vector2 start, Vector2 end, int radius, int width, Color color)
        {
            var dir = (end - start).Normalized();
            var pDir = dir.Perpendicular();

            var rightStartPos = start + pDir * radius;
            var leftStartPos = start - pDir * radius;
            var rightEndPos = end + pDir * radius;
            var leftEndPos = end - pDir * radius;

            var rStartPos = Drawing.WorldToScreen(new Vector3(rightStartPos.X, rightStartPos.Y, MyHero.Position.Z));
            var lStartPos = Drawing.WorldToScreen(new Vector3(leftStartPos.X, leftStartPos.Y, MyHero.Position.Z));
            var rEndPos = Drawing.WorldToScreen(new Vector3(rightEndPos.X, rightEndPos.Y, MyHero.Position.Z));
            var lEndPos = Drawing.WorldToScreen(new Vector3(leftEndPos.X, leftEndPos.Y, MyHero.Position.Z));

            Drawing.DrawLine(rStartPos, rEndPos, width, color);
            Drawing.DrawLine(lStartPos, lEndPos, width, color);
            Drawing.DrawLine(rStartPos, lStartPos, width, color);
            Drawing.DrawLine(lEndPos, rEndPos, width, color);
        }

        private void DrawEvadeStatus()
        {
            if (ConfigValue.DrawEvadeStatus.GetBool())
            {
                var heroPos = Drawing.WorldToScreen(ObjectManager.Player.Position);
                var dimension = Drawing.GetTextEntent("Evade: ON", 12);

                if (ConfigValue.DodgeSkillShots.GetBool())
                {
                    if (AdEvade.IsDodging)
                    {
                        Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Red, "Evade: ON");
                    }
                    else
                    {
                        if (AdEvade.IsDodgeDangerousEnabled())
                            Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Yellow, "Evade: ON");
                        else
                            Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.White, "Evade: ON");
                    }
                }
                else
                {
                    if (ConfigValue.ActivateEvadeSpells.GetBool())
                    {
                        Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Purple, "Evade: Spell");
                    }
                    else
                    {
                        Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Gray, "Evade: OFF");
                    }
                }



            }
        }

        private Color GetSpellColor(SpellDangerLevel dangerLevel)
        {
            return DangerColorPickers[(int) dangerLevel].CurrentValue;
        }

        private void Drawing_OnDraw(EventArgs args)
        {

            if (ConfigValue.DrawEvadePosition.GetBool())
            {
                //Render.Circle.DrawCircle(myHero.Position.ExtendDir(dir, 500), 65, Color.Red, 10);

                /*foreach (var point in myHero.Path)
                {
                    Render.Circle.DrawCircle(point, 65, Color.Red, 10);
                }*/

                if (AdEvade.LastPosInfo != null)
                {
                    var pos = AdEvade.LastPosInfo.Position; //Evade.lastEvadeCommand.targetPosition;
                    Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, MyHero.Position.Z), 65, Color.Red, 10);
                }
            }
            //DrawPlayerPath();
            DrawEvadeStatus();

            if (!ConfigValue.DrawSkillShots.GetBool())
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.DrawSpells)
            {
                Spell spell = entry.Value;

                var width = 0;
                switch (spell.Dangerlevel)
                {
                    case SpellDangerLevel.Low:
                        width = ConfigValue.LowDangerDrawWidth.GetInt();
                        break;
                    case SpellDangerLevel.Normal:
                        width = ConfigValue.NormalDangerDrawWidth.GetInt();
                        break;
                    case SpellDangerLevel.High:
                        width = ConfigValue.HighDangerDrawWidth.GetInt();
                        break;
                    case SpellDangerLevel.Extreme:
                        width = ConfigValue.ExtremeDangerDrawWidth.GetInt();
                        break;
                }
                if (Config.Properties.GetSpell(spell.Info.SpellName).Draw)
                {
                    if (spell.SpellType == SpellType.Line)
                    {
                        Vector2 spellPos = spell.CurrentSpellPosition;
                        Vector2 spellEndPos = spell.GetSpellEndPosition();

                        DrawLineRectangle(spellPos, spellEndPos, (int) spell.Radius, width, GetSpellColor(spell.Dangerlevel));

                        /*foreach (var hero in ObjectManager.Get<AIHeroClient>())
                        {
                            Render.Circle.DrawCircle(new Vector3(hero.ServerPosition.X, hero.ServerPosition.Y, myHero.Position.Z), (int)spell.radius, Color.Red, 5);
                        }*/

                        if (ConfigValue.DrawSpellPosition.GetBool() && spell.SpellObject != null)
                        {
                            //spellPos = SpellDetector.GetCurrentSpellPosition(spell, true, ObjectCache.gamePing);

                            /*if (true)
                            {
                                var spellPos2 = spell.startPos + spell.direction * spell.info.projectileSpeed * (Evade.GetTickCount - spell.startTime - spell.info.spellDelay) / 1000 + spell.direction * spell.info.projectileSpeed * ((float)ObjectCache.gamePing / 1000);
                                Render.Circle.DrawCircle(new Vector3(spellPos2.X, spellPos2.Y, myHero.Position.Z), (int)spell.radius, Color.Red, 8);
                            }*/

                            /*if (spell.spellObject != null && spell.spellObject.IsValid && spell.spellObject.IsVisible &&
                                  spell.spellObject.Position.To2D().Distance(ObjectCache.myHeroCache.serverPos2D) < spell.info.range + 1000)*/

                            Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, MyHero.Position.Z), (int) spell.Radius, GetSpellColor(spell.Dangerlevel), width);
                        }
                    }
                    else if (spell.SpellType == SpellType.Circular)
                    {
                        Render.Circle.DrawCircle(new Vector3(spell.EndPos.X, spell.EndPos.Y, spell.Height), (int) spell.Radius, GetSpellColor(spell.Dangerlevel), width);

                        if (spell.Info.SpellName == "VeigarEventHorizon")
                        {
                            Render.Circle.DrawCircle(new Vector3(spell.EndPos.X, spell.EndPos.Y, spell.Height), (int) spell.Radius - 125, GetSpellColor(spell.Dangerlevel), width);
                        }
                    }
                    else if (spell.SpellType == SpellType.Arc)
                    {
                        /*var spellRange = spell.startPos.Distance(spell.endPos);
                        var midPoint = spell.startPos + spell.direction * (spellRange / 2);

                        Render.Circle.DrawCircle(new Vector3(midPoint.X, midPoint.Y, myHero.Position.Z), (int)spell.radius, spellDrawingConfig.Color, spellDrawingWidth);
                        
                        Drawing.DrawLine(Drawing.WorldToScreen(spell.startPos.To3D()),
                                         Drawing.WorldToScreen(spell.endPos.To3D()), 
                                         spellDrawingWidth, spellDrawingConfig.Color);*/
                    }
                    else if (spell.SpellType == SpellType.Cone)
                    {
                    }
                }
            }
        }

        private void DrawPlayerPath()
        {
            Line.DrawLine(Color.White, GameData.MyHero.Path);
        }
    }
}
