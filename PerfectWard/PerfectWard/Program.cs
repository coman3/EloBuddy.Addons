using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using PerfectWard.Config;
using PerfectWard.Config.Interfaces;
using PerfectWard.Data;
using SharpDX;

namespace PerfectWard
{
    class Program
    {
        public static Menu menu;
        private static WardSpot _placingWardSpot;
        private static bool _placePinkWard;
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;

        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            
            menu = MainMenu.AddMenu("Perfect Ward", "PerfectWard");
            menu.AddGroupLabel("Perfect Ward");
            menu.AddLabel("By Coman3");
            menu.AddSeparator();
            if (Game.MapId == GameMapId.SummonersRift)
            {
                menu.AddDynamicControl("Enable", new DynamicCheckBox("Enable", "Enable Perfect Ward", true));
                menu.AddDynamicControl("PlaceWard",
                    new DynamicKeyBind("PlaceWard", "Place a perfect ward", false, KeyBind.BindTypes.HoldActive))
                    .OnValueChange += Program_PlaceWard_OnValueChange;
                menu.AddDynamicControl("PlacePinkWard",
                    new DynamicKeyBind("PlacePinkWard", "Place a perfect Pink ward", false, KeyBind.BindTypes.HoldActive))
                    .OnValueChange += Program_PlacePinkWard_OnValueChange;
                menu.AddDynamicControl("WardSpotDrawDistance",
                    new DynamicSlider("WardSpotDrawDistance", "Draw ward spot range from player (value * 1000 )", 3, 1,
                        20));
                menu.AddDynamicControl("WardSnapRadius",
                    new DynamicSlider("WardSnapRadius", "Distance to snap to large circle from mouse position", 100, 50,
                        300));
                Game.OnUpdate += Game_OnUpdate;

                Wards.CreateWardSpotObjects();
            }
            else
            {
                menu.AddLabel("Disabled! (Map is not Summoners Rift)");
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if(!Properties.GetData<bool>("Enable")) return;
            Wards.UpdateWardSpotObjects();
            if (_placingWardSpot != null)
            {
                //if (!Player.Instance.Path.Contains(_placingWardSpot.MovePosition)) { _placingWardSpot = null; return;}
                if (Player.Instance.Position.IsInRange(_placingWardSpot.MovePosition, 20) || Player.Instance.Position.IsInRange(_placingWardSpot.ClickPosition, 1100))
                {

                    var item = _placePinkWard ? Wards.GetPinkSlot() : Wards.GetWardSlot();

                    if (item != null)
                    {
                        Player.CastSpell(item.SpellSlot, _placingWardSpot.ClickPosition);
                    }
                    _placingWardSpot = null;
                    _placePinkWard = false;
                    Chat.Print(_placePinkWard ? "Placed pink ward!" : "Placed normal ward!");
                }
            }
        }

        private static void Program_PlaceWard_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (!Properties.GetData<bool>("Enable")) return;
            if (!args.OldValue && args.NewValue)
            {
                WardSpot spot;
                if (Wards.TryFindNearestSafeWardSpot(Game.CursorPos, out spot))
                {
                    if (spot.ClickPosition.IsInRange(Player.Instance, 1100))
                    {
                        var item = Wards.GetWardSlot();
                        if (item != null)
                        {
                            if(Player.CastSpell(item.SpellSlot, spot.ClickPosition))
                                Chat.Print(_placePinkWard ? "Placed pink ward!" : "Placed normal ward!");
                            return;
                        }
                    }
                    Player.IssueOrder(GameObjectOrder.MoveTo, spot.MovePosition, false);
                    _placingWardSpot = spot;
                }else if (Wards.TryFindNearestWardSpot(Game.CursorPos, out spot))
                {
                    var item = Wards.GetWardSlot();
                    if (item != null)
                        Player.CastSpell(item.SpellSlot, spot.MagneticPosition);
                }
            }
        }
        private static void Program_PlacePinkWard_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (!Properties.GetData<bool>("Enable")) return;
            if (!args.OldValue && args.NewValue)
            {
                WardSpot spot;
                if (Wards.TryFindNearestSafeWardSpot(Game.CursorPos, out spot))
                {
                    if (spot.ClickPosition.IsInRange(Player.Instance, 1100))
                    {
                        var item = Wards.GetPinkSlot();
                        if (item != null)
                        {
                            if (Player.CastSpell(item.SpellSlot, spot.ClickPosition))
                                Chat.Print(_placePinkWard ? "Placed pink ward!" : "Placed normal ward!");
                            return;
                        }
                    }
                    Player.IssueOrder(GameObjectOrder.MoveTo, spot.MovePosition, false);
                    _placingWardSpot = spot;
                    _placePinkWard = true;
                }
                else if (Wards.TryFindNearestWardSpot(Game.CursorPos, out spot))
                {
                    var item = Wards.GetPinkSlot();
                    if (item == null) return;
                    if(Player.CastSpell(item.SpellSlot, spot.MagneticPosition))
                        Chat.Print(_placePinkWard ? "Placed pink ward!" : "Placed normal ward!");
                }
            }
        }
    }
}
