using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CrowdT.Level
{
    public class LevelController : MonoBehaviour
    {
        [HideInInspector] public GroundGrid groundGrid;
        [HideInInspector] public CrowdController crowdController;
        [HideInInspector] public BarrierController barrierController;

        [HideInInspector] public Player player;
        [HideInInspector] public Enemy enemy;

        private Character _winner;

        public static bool IsEnd;

        public static bool CrowdBattleStarted = false;

        public Action OnLevelStart;
        public Action OnPlayerStartPlaying;
        public Action OnLevelEnd;
        public Action OnLevelSuccess;
        public Action OnLevelFail;
        
        private void Awake()
        {
            IsEnd = false;
            CrowdBattleStarted = false;
        }

        private void OnEnable()
        {
            OnPlayerStartPlaying += PlayerStartPlaying;
        }

        private void Start()
        {
            groundGrid = FindObjectOfType<GroundGrid>();
            crowdController = FindObjectOfType<CrowdController>();
            barrierController = FindObjectOfType<BarrierController>();

            player = FindObjectOfType<Player>();
            enemy = FindObjectOfType<Enemy>();

            KetchappTools.Menus.MenuManager.Instance.HideAllMenusImmediately();
            KetchappTools.Menus.MenuManager.Instance.MenuList.GameWindow.Show();
            
            OnLevelStart?.Invoke();
        }

        private void PlayerStartPlaying()
        {
            FirstPlayerTurnOnLevelMadeTracker.IsMade = true;
        }

        public async Task TryEndGame()
        {
            if (IsEnd) return;

            if (groundGrid.IsBarrierBetweenArmies()) return;

            IsEnd = true;

            while (!crowdController.HaveAllUnitsOccupiedCells())
                await Task.Yield();

            _winner = GetWinner();

            CrowdBattleStarted = true;
            barrierController.RemoveAll();
            crowdController.IncreaseWinnerHealth(_winner);
            crowdController.StartBattle();
        }

        private Character GetWinner()
        {
            if (groundGrid.playerCellsAmount > groundGrid.enemyCellsAmount)
                return player;

            return enemy;
        }

        public void TryEndCrowdBattle()
        {
            if (crowdController.enemyUnitsAliveAmount == 0 || crowdController.playerUnitsAliveAmount == 0)
            {
                if (_winner == player)
                {
                    crowdController.PlayPlayerCrowdVictory();
                    KetchappTools.Menus.MenuManager.Instance.MenuList.VictoryWindow.ShowWithDelay();
                    
                    OnLevelSuccess?.Invoke();
                }

                else if (_winner == enemy)
                {
                    KetchappTools.Menus.MenuManager.Instance.MenuList.LoseWindow.ShowWithDelay();
                    
                    OnLevelFail?.Invoke();
                }
                
                OnLevelEnd?.Invoke();
            }
        }

        private void OnDisable()
        {
            OnPlayerStartPlaying -= PlayerStartPlaying;
        }
    }
}