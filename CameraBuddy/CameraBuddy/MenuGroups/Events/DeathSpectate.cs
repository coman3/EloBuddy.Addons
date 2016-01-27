using CameraBuddy.Camera;
using CameraBuddy.Spectate;
using EloBuddy;
using EloBuddy.SDK.Menu;

namespace CameraBuddy.MenuGroups.Events
{
    public class DeathSpectate : MenuGroup 
    {
        public bool IsPlayerDead { get { return Player.Instance.IsDead; } }
        public Spectator Spectator { get; set; }
        public DeathSpectate()
        {
            Spectator = new Spectator();
            EloBuddy.Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(System.EventArgs args)
        {
            if(Enabled)
                Spectator.Start();
            else
                Spectator.Stop();

        }

        public override void AddToMenu(Menu menuBase)
        {
            var menu = menuBase.Parent.AddSubMenu("     Death Spectate");
            base.AddToMenu(menu);
            

        }

        public override string GetUniqueId()
        {
            return "DeathSpectate";
        }
    }
}