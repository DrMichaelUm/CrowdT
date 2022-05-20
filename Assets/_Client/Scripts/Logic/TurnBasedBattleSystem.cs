using CrowdT.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrowdT
{
    public enum TurnState
    {
        PlayerTurn,
        EnemyTurn,
        Busy
    }

    public class TurnBasedBattleSystem : MonoBehaviour
    {
        private Character _player;
        private Character _enemy;
        
        [ReadOnly] public TurnState state;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _enemy = FindObjectOfType<Enemy>();
            
            StartPlayerTurn();
        }

        private async void StartPlayerTurn()
        {
            if (LevelController.IsEnd) return;
            
            state = TurnState.PlayerTurn;
            
            await _player.ExecuteTurn();
            
            StartEnemyTurn();
        }

        private async void StartEnemyTurn()
        {
            if (LevelController.IsEnd) return;

            state = TurnState.EnemyTurn;
            
            await _enemy.ExecuteTurn();
            
            StartPlayerTurn();
        }
    }
}