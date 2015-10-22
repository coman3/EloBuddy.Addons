using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EzEvade.Config;
using EzEvade.Data;
using SharpDX;
using Color = System.Drawing.Color;

namespace EzEvade.Draw
{
    internal class SpellDrawer
    {
        public static Menu Menu;

        private static AIHeroClient MyHero { get { return ObjectManager.Player; } }


        public SpellDrawer(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;

            Menu = mainMenu;
            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            //Console.WriteLine("SpellDrawer loaded");
            

            Menu drawMenu = Menu.IsSubMenu ? Menu.Parent.AddSubMenu("Draw", "Draw") : Menu.AddSubMenu("Draw", "Draw");
            drawMenu.Add("DrawSkillShots", new DynamicCheckBox(ConfigDataType.Data, "DrawSkillShots", "Draw SkillShots", true).CheckBox);
            drawMenu.Add("ShowStatus", new DynamicCheckBox(ConfigDataType.Data, "ShowStatus", "Show Evade Status", true).CheckBox);
            drawMenu.Add("DrawSpellPos", new DynamicCheckBox(ConfigDataType.Data, "DrawSpellPos", "Draw Spell Position", false).CheckBox);
            drawMenu.Add("DrawEvadePosition", new DynamicCheckBox(ConfigDataType.Data, "DrawEvadePosition", "Draw Evade Position", false).CheckBox);

            Menu dangerMenu = drawMenu.Parent.AddSubMenu("DangerLevel Drawings", "DangerLevelDrawings");
            Menu lowDangerMenu = dangerMenu.Parent.AddSubMenu("   Low", "LowDrawing");
            
            lowDangerMenu.Add("LowWidth", new DynamicSlider(ConfigDataType.Data, "LowWidth", "Line Width", 3, 1, 15).Slider);

            Menu normalDangerMenu = dangerMenu.Parent.AddSubMenu("    Normal", "NormalDrawing");
            normalDangerMenu.Add("NormalWidth", new DynamicSlider(ConfigDataType.Data, "NormalWidth", "Line Width", 3, 1, 15).Slider);

            Menu highDangerMenu = dangerMenu.Parent.AddSubMenu("    High", "HighDrawing");
            highDangerMenu.Add("HighWidth", new DynamicSlider(ConfigDataType.Data, "HighWidth", "Line Width", 4, 1, 15).Slider);

            Menu extremeDangerMenu = dangerMenu.Parent.AddSubMenu("    Extreme", "ExtremeDrawing");
            extremeDangerMenu.Add("ExtremeWidth", new DynamicSlider(ConfigDataType.Data, "ExtremeWidth", "Line Width", 4, 1, 15).Slider);

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
            if (Config.Config.GetData<bool>("ShowStatus"))
            {
                var heroPos = Drawing.WorldToScreen(ObjectManager.Player.Position);
                var dimension = Drawing.GetTextEntent("Evade: ON", 12);

                if (Config.Config.GetData<bool>("DodgeSkillShots"))
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
                    if (Config.Config.Keys["ActivateEvadeSpells"].CurrentValue)
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

        private void Drawing_OnDraw(EventArgs args)
        {

            if (Config.Config.GetData<bool>("DrawEvadePosition"))
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

            DrawEvadeStatus();

            if (Config.Config.GetData<bool>("DrawSkillShots") == false)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.DrawSpells)
            {
                Spell spell = entry.Value;

                var dangerStr = spell.GetSpellDangerString();
                //var spellDrawingConfig = ObjectCache.menuCache.cache[dangerStr + "Color"].GetValue<Circle>();
                var spellDrawingWidth = Config.Config.GetData<int>(dangerStr + "Width");

                if (Config.Config.GetSpell(spell.Info.SpellName).Draw)
                {
                    if (spell.SpellType == SpellType.Line)
                    {
                        Vector2 spellPos = spell.CurrentSpellPosition;
                        Vector2 spellEndPos = spell.GetSpellEndPosition();

                        DrawLineRectangle(spellPos, spellEndPos, (int)spell.Radius, spellDrawingWidth, Color.Red);

                        /*foreach (var hero in ObjectManager.Get<AIHeroClient>())
                        {
                            Render.Circle.DrawCircle(new Vector3(hero.ServerPosition.X, hero.ServerPosition.Y, myHero.Position.Z), (int)spell.radius, Color.Red, 5);
                        }*/

                        if (Config.Config.GetData<bool>("DrawSpellPos"))// && spell.spellObject != null)
                        {
                            //spellPos = SpellDetector.GetCurrentSpellPosition(spell, true, ObjectCache.gamePing);

                            /*if (true)
                            {
                                var spellPos2 = spell.startPos + spell.direction * spell.info.projectileSpeed * (Evade.GetTickCount - spell.startTime - spell.info.spellDelay) / 1000 + spell.direction * spell.info.projectileSpeed * ((float)ObjectCache.gamePing / 1000);
                                Render.Circle.DrawCircle(new Vector3(spellPos2.X, spellPos2.Y, myHero.Position.Z), (int)spell.radius, Color.Red, 8);
                            }*/

                            /*if (spell.spellObject != null && spell.spellObject.IsValid && spell.spellObject.IsVisible &&
                                  spell.spellObject.Position.To2D().Distance(ObjectCache.myHeroCache.serverPos2D) < spell.info.range + 1000)*/

                            Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, MyHero.Position.Z), (int)spell.Radius, Color.Red, spellDrawingWidth);
                        }

                    }
                    else if (spell.SpellType == SpellType.Circular)
                    {
                        Render.Circle.DrawCircle(new Vector3(spell.EndPos.X, spell.EndPos.Y, spell.Height), (int)spell.Radius, Color.Red, spellDrawingWidth);

                        if (spell.Info.SpellName == "VeigarEventHorizon")
                        {
                            Render.Circle.DrawCircle(new Vector3(spell.EndPos.X, spell.EndPos.Y, spell.Height), (int)spell.Radius - 125, Color.Red, spellDrawingWidth);
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
    }
}
