using System;
using System.Linq;
using CameraBuddy.Camera;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
namespace CameraBuddy.MenuGroups
{
    public class AutoPositionCamera : IntelligenceMenuGroup
    {
        public KeyBind DirectCameraTowardsEnemy { get; set; }
        public KeyBind DirectCameraTowardsMinions { get; set; }
        public KeyBind DirectCameraTowardsMouse { get; set; }
        public KeyBind DirectCameraTowardsEntry { get; set; }

        public Slider EntityDetectionRange { get; set; }

        public CheckBox AllowZooming { get; set; }
        public CheckBox SnapToChamp { get; set; }
        public Slider MaxExtraZoom { get; set; }
        public Slider ExtraDistance { get; set; }
        public Slider SnapLockDistance { get; set; }
        public Vector2[] Crosshair;
        public readonly float CrosshairSize = 10f;
        public AutoPositionCamera()
        {
            Drawing.OnDraw += Draw;
            Crosshair = new Vector2[5];
            Crosshair[0] = new Vector2(Drawing.Width / 2f - CrosshairSize, Drawing.Height / 2f);
            Crosshair[1] = new Vector2(Drawing.Width / 2f + CrosshairSize, Drawing.Height / 2f);
            Crosshair[2] = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f);
            Crosshair[3] = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f + CrosshairSize);
            Crosshair[4] = new Vector2(Drawing.Width / 2f, Drawing.Height / 2f - CrosshairSize);

        }

        private void Draw(System.EventArgs args)
        {
            if(!Enabled) return;
            Line.DrawLine(Color.Black, Crosshair);
            Line.DrawLine(Color.Blue, Crosshair[2], Game.CursorPos2D);

            Line.DrawLine(Color.Red, Player.Instance.Position, new Vector3(Crosshair[2].ScreenToWorld().X, Crosshair[2].ScreenToWorld().Y + 700, Crosshair[2].ScreenToWorld().Z));
            if (SnapToChamp.CurrentValue)
                Circle.Draw(new ColorBGRA(255, 255, 255, 127), SnapLockDistance.CurrentValue, Player.Instance.Position);
            if (DirectCameraTowardsMouse.CurrentValue)
            {
                var camState = new CameraState();

                if (Game.CursorPos.Distance(Player.Instance.Position) > SnapLockDistance.CurrentValue)
                    camState.Position = Player.Instance.Position.Extend(Game.CursorPos, ExtraDistance.CurrentValue);
                else if (SnapToChamp.CurrentValue)
                    camState.Position = Player.Instance.Position.To2D();
                else
                    camState.Position = Player.Instance.Position.Extend(Game.CursorPos, ExtraDistance.CurrentValue / 4f);
                camState.Set(false);

            }
            else if (DirectCameraTowardsEnemy.CurrentValue)
            {
                var playerPos = Player.Instance.Position;
                var pos = new CameraState();
                var heroes = EntityManager.Heroes.Enemies.Where(x=> x.Position.Distance(playerPos) < EntityDetectionRange.CurrentValue && x.IsHPBarRendered).ToList();
                if (heroes.Count <= 0)
                {
                    if(!SnapToChamp.CurrentValue) return;
                    pos.Position = playerPos.To2D();
                    pos.Set(false);
                    return;
                }
                var averagePos = heroes.AveragePosition();
                Obj_AI_Base furthestItem;
                var maxDistance = heroes.MaxDistance(playerPos, out furthestItem);
                if (furthestItem == null) return;
                if (averagePos.Distance(Player.Instance.Position) > SnapLockDistance.CurrentValue)
                {
                    pos.Position = playerPos.Extend(averagePos, ExtraDistance.CurrentValue);
                    pos.Set(false);
                }
                else if (SnapToChamp.CurrentValue)
                {
                    pos.Position = Player.Instance.Position.To2D();
                    pos.Set(false);
                }
            }
            else if (DirectCameraTowardsMinions.CurrentValue)
            {
                var playerPos = Player.Instance.Position;
                var pos = new CameraState();
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Position.Distance(playerPos) < EntityDetectionRange.CurrentValue / 2f && x.IsHPBarRendered).ToList();
                if (minions.Count <= 0)
                {
                    if (!SnapToChamp.CurrentValue) return;
                    pos.Position = playerPos.To2D();
                    pos.Set(false);
                    return;
                }
                var averagePos = minions.AveragePosition();
                Obj_AI_Base furthestItem;
                var maxDistance = minions.MaxDistance(playerPos, out furthestItem);
                if (furthestItem == null) return;
                if (averagePos.Distance(Player.Instance.Position) > SnapLockDistance.CurrentValue)
                {
                    pos.Position = playerPos.Extend(averagePos, ExtraDistance.CurrentValue);
                    pos.Set(false);
                }
                else if (SnapToChamp.CurrentValue)
                {
                    pos.Position = Player.Instance.Position.To2D();
                    pos.Set(false);
                }
            }
            //else if (DirectCameraTowardsEntry.CurrentValue)
            //{
                
            //}
        }

        public override void AddToMenu(Menu menuBase)
        {
            var menu = menuBase.Parent.AddSubMenu("     - Auto Position");
            menu.AddLabel("'Auto Position' will automatically move the camera relatively towards Enemy heroes, Minions or Lane entries.");
            menu.AddLabel("It will always keep your player visible within the view port.");
            menu.AddLabel("Snap is when the focus point is nearby the player, and moving the camera around would cause distortion, ");
            menu.AddLabel("making the camera 'wobble'. This is usually best to have set to your heroes attack range.");
            menu.AddSeparator(10);

            DirectCameraTowardsEnemy = AddKeyBind(new KeyBind("Direct Towards Heroes", false, KeyBind.BindTypes.HoldActive));
            DirectCameraTowardsMinions = AddKeyBind(new KeyBind("Direct Towards Minions", false, KeyBind.BindTypes.HoldActive));
            DirectCameraTowardsMouse = AddKeyBind(new KeyBind("Direct Towards Mouse", false, KeyBind.BindTypes.HoldActive));
            //DirectCameraTowardsEntry = AddKeyBind(new KeyBind("Direct Towards Entry", false, KeyBind.BindTypes.HoldActive));

            EntityDetectionRange = AddSlider(new Slider("Entity Detection Range", 3000, 500, 5000));

            AllowZooming = AddCheckbox(new CheckBox("Allow Zooming"));
            MaxExtraZoom = AddSlider(new Slider("Maximum Extra Zoom", 300, 0, 2000));
            ExtraDistance = AddSlider(new Slider("Extra Distance towards focus point", 100, 0, 1000));
            SnapLockDistance = AddSlider(new Slider("Distance of Snap (see above)", 500, 500, 1500));
            SnapToChamp = AddCheckbox(new CheckBox("Snap To Player (Disabled: Free Cam)"));

            base.AddToMenu(menu);


        }
        public override string GetUniqueId()
        {
            return "AutoPositionCamera";
        }
    }
}