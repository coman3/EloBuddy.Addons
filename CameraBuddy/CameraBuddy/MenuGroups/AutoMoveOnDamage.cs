using System;
using CameraBuddy.Camera;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CameraBuddy.MenuGroups
{
    public class AutoMoveOnDamage : IntelligenceMenuGroup
    {
        public Slider DamageRecivedCount { get; set; }
        public CheckBox IgnoreMinionDamage { get; set; }
        public CheckBox OnlyMoveOnCc { get; set; }
        public CameraState CameraState { get; private set; }

        public AutoMoveOnDamage()
        {
            AttackableUnit.OnDamage += Player_OnDamage;
            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (CrowdControlChecks() && OnlyMoveOnCc.CurrentValue) MoveCamera();
        }

        private static bool CrowdControlChecks()
        {
            return Player.Instance.IsRooted || Player.Instance.IsStunned || Player.Instance.IsCharmed || Player.Instance.IsFeared || Player.Instance.IsFleeing;
        }

        private void MoveCamera()
        {
            CameraState = new CameraState {Position = Player.Instance.Position.To2D()};
            CameraState.Set(false);
        }

        private void Player_OnDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args)
        {
            if(sender.IsMe || !args.Target.IsMe) return;
            if (args.Damage < DamageRecivedCount.CurrentValue) return;
            if (OnlyMoveOnCc.CurrentValue) return;
            if (args.Source is Obj_AI_Minion && IgnoreMinionDamage.CurrentValue) return;

            MoveCamera();

        }

        public override string GetUniqueId()
        {
            return "AutoMoveOnDamage";
        }

        public override void AddToMenu(Menu menuBase)
        {
            var menu = menuBase.Parent.AddSubMenu("     - On Hurt");
            menu.AddLabel("'On Hurt' will automatically move the camera towards your player if you \nreceive damage or CC (Crowd Control), within the specified parameters");
            menu.AddSeparator(10);

            DamageRecivedCount = AddSlider(new Slider("Total damage until activation", 60, 0, (int) Player.Instance.MaxHealth));
            OnlyMoveOnCc = AddCheckbox(new CheckBox("Only move the camera on crowd control", false));
            IgnoreMinionDamage = AddCheckbox(new CheckBox("Ignore Minion Damage"));


            base.AddToMenu(menu);

            
        }
    }
}