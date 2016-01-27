using System;
using CameraBuddy.Spectate.Data;
using CameraBuddy.Spectate.Situation;
using EloBuddy;
using Player = CameraBuddy.Spectate.Situation.Player;

namespace CameraBuddy.Spectate.Core
{
    /// <summary>
    /// Base class for every champion plugin
    /// </summary>
    public abstract class ChampionPlugin
    {
        public bool CheckRegionIn { get; set; }
        protected ChampionPlugin()
        {
            CheckRegionIn = false;
            AIHeroClient.OnSpawn += OnSpawn;
            EloBuddy.Player.OnIssueOrder += OnIssueOrderBase;
            Obj_AI_Base.OnLevelUp += OnLevelUp;
            AttackableUnit.OnDamage += OnDamage;
            AIHeroClient.OnDeath += OnDeath;
            Obj_AI_Base.OnSurrender += OnSurrender;
            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLose;

            Drawing.OnBeginScene += OnBeginDraw;
            Drawing.OnDraw += OnDraw;
            Drawing.OnEndScene += OnEndDraw;
            //Game Events
            EloBuddy.Game.OnTick += OnGameTick;
            EloBuddy.Game.OnUpdate += OnGameUpdateBase;
            EloBuddy.Game.OnEnd += OnGameEnd;
        }
        public Region.Location RegionMovingTo { get; private set; }
        public Region.Location CurrentRegion { get; private set; }

        private Lane _playerLane;
        public Lane PlayerLane
        {
            get { return _playerLane; }
            set
            {
                _playerLane = value;
                OnPlayerLaneChanged(value);
            }
        }

        private bool _onAriveAtShopCalled;
        private bool _onArriveAtRegionCalled;
        private bool _onGateDownCalled;
        //Game Events
        public void OnGameUpdateBase(EventArgs args)
        {
            #region PosistionEvents

            //OnAriveAtShop
            if (Player.IsAtShop() && !_onAriveAtShopCalled)
            {
                OnAriveAtShop();
                _onAriveAtShopCalled = true;
            }
            else if(_onAriveAtShopCalled && !Player.IsAtShop()) _onAriveAtShopCalled = false;

            //OnArriveAtRegion
            //Fps Drops if enabled.
            if (CheckRegionIn)
            {
                var regionIn = Player.RegionIn;
                if (CurrentRegion != regionIn && !_onArriveAtRegionCalled)
                {
                    OnArriveAtRegion(CurrentRegion, regionIn);
                    CurrentRegion = regionIn;
                    _onArriveAtRegionCalled = true;
                }
                else _onArriveAtRegionCalled = false;
            }

            #endregion

            #region Game Events
            //When spawn gate falls
            if(!Timing.IsGateUp && !_onGateDownCalled)
            {
                OnGateDown();
                _onGateDownCalled = true;
            }
            #endregion
            switch (Situation.Game.GameState)
            {
                case GameState.GameStart:
                    OnGameStart();
                    break;
                case GameState.MinionsSpawning:
                    OnMinionsSpawning();
                    break;
                case GameState.JungleSpawning:
                    OnJungleSpawning();
                    break;
                case GameState.FirstMinionContact:
                    OnMinionContact();
                    break;
                case GameState.Laning:
                    OnLaning();
                    break;
                case GameState.AllyGrouping:
                    OnAllyGrouping();
                    break;
                case GameState.EnemyGrouping:
                    OnEnemyGrouping();
                    break;
                case GameState.AllyFinishing:
                    OnAllyFinishing();
                    break;
                case GameState.EnemyFinishing:
                    OnEnemyFinishing();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnGameUpdate(args);
        }
    
        #region Ai Logic Methods
        protected virtual void OnEnemyFinishing() {}
        protected virtual void OnAllyFinishing() { }
        protected virtual void OnEnemyGrouping() { }
        protected virtual void OnAllyGrouping() { }
        protected virtual void OnLaning() { }
        protected virtual void OnMinionContact() { }
        protected virtual void OnJungleSpawning() { }
        protected virtual void OnMinionsSpawning() { }
        protected virtual void OnGameStart() { }
        protected virtual void OnPlayerLaneChanged(Lane value) { }
        #endregion
        public abstract void OnGameUpdate(EventArgs args);
        public virtual void OnGameTick(EventArgs args) { }
        public abstract void OnGameEnd(GameEndEventArgs args);

        /// <summary>
        /// Called when the gate is down (will be called when the addon is reloaded!)
        /// </summary>
        public virtual void OnGateDown() { }

        #region Player Events
        public virtual void OnAriveAtShop()
        {
            BuyItems(Situation.Game.GameState, Player.GetPlayerState());
        }
        public virtual void OnArriveAtRegion(Region.Location prevoiusRegion, Region.Location currentRegion) { }

        public void OnIssueOrderBase(Obj_AI_Base sender, PlayerIssueOrderEventArgs eventArgs)
        {
            if (sender.IsMe &&
                (eventArgs.Order == GameObjectOrder.AttackTo || eventArgs.Order == GameObjectOrder.MoveTo))
            {
                
            }

            OnIssueOrder(sender, eventArgs);
        }
        public virtual void OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs eventArgs) { }
        public virtual void OnSpawn(Obj_AI_Base sender) { }
        public virtual void OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args) { }
        public virtual void OnDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args) { }
        public virtual void OnDeath(Obj_AI_Base sender, OnHeroDeathEventArgs args) { }
        public virtual void OnSurrender(Obj_AI_Base sender, Obj_AI_BaseSurrenderVoteEventArgs args) { }
        public virtual void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args) { }
        public virtual void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args) { }
        #endregion

        #region Drawing Events
        //Drawing
        public virtual void OnBeginDraw(EventArgs args) { }
        public abstract void OnDraw(EventArgs args);
        public virtual void OnEndDraw(EventArgs args) { }
        #endregion

        //Actions
        public abstract void BuyItems(GameState gameState, PlayerState playerstate);

    }

    public class ChampionPluginAttribute : Attribute
    {
        public string ChampionName { get; set; }
        public string SuportedVersion { get; set; }

    }
}