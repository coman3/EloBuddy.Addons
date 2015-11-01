using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using PerfectWard.Draw;
using SharpDX;
using Color = System.Drawing.Color;
namespace PerfectWard.Data
{
    public static class Wards
    {
        private static List<WardSpot> _wardSpots;
        public static AIHeroClient Player = ObjectManager.Player;
        public static List<InventorySlot> WardItems;
        public static List<WardSpot> WardSpots
        {
            get
            {
                if (_wardSpots == null)
                {
                    WardPositions.InitializeWardSpots(ref _wardSpots);
                }

                return _wardSpots;
            }
        }

        static Wards()
        {
            UpdateWardItems();
            Shop.OnBuyItem += (sender, args) => UpdateWardItems();
        }

        public static void UpdateWardItems()
        {
            WardItems = new List<InventorySlot>();
            foreach (var item in Player.InventoryItems)
            {
                if(item != null)
                    if (item.IsWard || item.Id == ItemId.Vision_Ward || item.Id == ItemId.Greater_Vision_Totem_Trinket)
                        WardItems.Add(item);
            }
        }

        public static InventorySlot GetPinkSlot()
        {
            var wardIds = new[] { ItemId.Vision_Ward, ItemId.Greater_Vision_Totem_Trinket };
            return WardItems.FirstOrDefault(i => i.CanUseItem() && wardIds.Contains(i.Id));
        }
        public static InventorySlot GetWardSlot()
        {
            var wardIds = new[] { ItemId.Vision_Ward, ItemId.Greater_Vision_Totem_Trinket };
            return WardItems.FirstOrDefault(i => i.CanUseItem() && !wardIds.Contains(i.Id));
        }
        

        public static bool TryFindNearestWardSpot(Vector3 cursorPosition, out WardSpot wardSpot)
        {
            foreach (WardSpot wardPosition in WardSpots)
            {
                if (wardPosition.MagneticPosition.IsInRange(cursorPosition, Config.Properties.GetData<int>("WardSnapRadius")) && Player.IsInRange(wardPosition.MagneticPosition, 650))
                {
                    wardSpot = wardPosition;
                    return true;
                }
            }
            wardSpot = null;
            return false;
        }

        public static bool TryFindNearestSafeWardSpot(Vector3 cursorPosition, out WardSpot outWardSpot)
        {
            foreach (WardSpot wardSpot in WardSpots.Where(x => x.IsSnapWard))
            {
                if (wardSpot.MagneticPosition.IsInRange(cursorPosition, Config.Properties.GetData<int>("WardSnapRadius")))
                {
                    outWardSpot = wardSpot;
                    return true;
                }
            }
            outWardSpot = null;
            return false;
        }

        public static void UpdateWardSpotObjects()
        {
            if (WardSpots != null)
            {
                foreach (WardSpot wardPos in WardSpots)
                {
                    if (wardPos.IsSnapWard)
                    {
                        var isInPoint = wardPos.MagneticPosition.IsInRange(Game.CursorPos,
                            Config.Properties.GetData<int>("WardSnapRadius"));
                        var isNearPlayer =
                            wardPos.MagneticPosition.IsNearPlayer(
                                Config.Properties.GetData<int>("WardSpotDrawDistance")*1000);

                        wardPos.MagneticCircle.Color = isInPoint ? Color.OrangeRed : Color.DarkOrange;

                        wardPos.WardCircle.Visable = isInPoint && isNearPlayer;
                        wardPos.MagneticCircle.Visable = isNearPlayer;
                        wardPos.ArrowLine.Visable = isInPoint && isNearPlayer;

                        wardPos.ArrowLine.Start =
                            new Vector3(wardPos.MagneticPosition.X, wardPos.MagneticPosition.Y, 0).WorldToScreen();
                        wardPos.ArrowLine.End =
                            new Vector3(wardPos.WardPosition.X, wardPos.WardPosition.Y, 0).WorldToScreen();
                    }
                    else
                    {
                        wardPos.MagneticCircle.Color = wardPos.MagneticPosition.IsInRange(Game.CursorPos, Config.Properties.GetData<int>("WardSnapRadius"))
                        ? Color.OrangeRed
                        : Color.Blue;
                        wardPos.MagneticCircle.Visable =
                            wardPos.MagneticPosition.IsNearPlayer(Config.Properties.GetData<int>("WardSpotDrawDistance") *
                                                                  1000);
                    }
                }
            }
        }

        public static void CreateWardSpotObjects()
        {
            foreach (WardSpot wardPos in WardSpots)
            {
                if (wardPos.IsSnapWard)
                {

                    //Vector2 screenPos = Drawing.WorldToScreen(safeWardSpot.MagneticPosition);
                    RenderObjects.Add(
                        wardPos.MagneticCircle =
                            new RenderCircle(wardPos.MagneticPosition.To2D(), Color.DarkOrange, 30, 3));
                    RenderObjects.Add(
                        wardPos.WardCircle = new RenderCircle(wardPos.WardPosition.To2D(), Color.Blue, 5, 3));
                    //new Circle(new ColorBGRA(255, 0, 255, 255), 5).Draw(safeWardSpot.WardPosition);

                    Vector2 screenMagneticPos = wardPos.MagneticPosition.WorldToScreen();
                    Vector2 screenDirectionVector = wardPos.WardPosition.WorldToScreen();

                    RenderObjects.Add(
                        wardPos.ArrowLine =
                            new RenderLine(screenMagneticPos, screenDirectionVector, Color.Green, 1.5f)
                            {
                                Visable = false
                            });
                }
                else
                {
                    RenderObjects.Add(
                        wardPos.MagneticCircle = new RenderCircle(wardPos.MagneticPosition.To2D(), Color.Blue, 30, 3));
                }
            }
        }

    }
}
