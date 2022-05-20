using CrowdT.Level;
using UnityEngine;

namespace CrowdT
{
    public class CrowdController : MonoBehaviour
    {
        public static int TotalPlayerUnitsHitsDuringLastSecond;

        public int unitsPerCell;
        public Vector3 unitsOffset;

        public KdTree<SingleUnit> PlayerUnits = new KdTree<SingleUnit>();
        public KdTree<SingleUnit> EnemyUnits = new KdTree<SingleUnit>();

        [HideInInspector] 
        public int unitsOccupiedTheirCellsAmount;
        [HideInInspector] 
        public int playerUnitsAliveAmount;
        [HideInInspector] 
        public int enemyUnitsAliveAmount;

        private LevelController _levelController;

        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();

            TotalPlayerUnitsHitsDuringLastSecond = 0;
        }

        public void RegisterUnit(SingleUnit unit)
        {
            if (unit is PlayerUnit)
            {
                PlayerUnits.Add(unit);
                playerUnitsAliveAmount++;
            }

            else if (unit is EnemyUnit)
            {
                EnemyUnits.Add(unit);
                enemyUnitsAliveAmount++;
            }

            unit.crowdController = this;
        }

        public void UnregisterUnit(SingleUnit unit)
        {
            if (unit is PlayerUnit)
            {
                playerUnitsAliveAmount--;
            }

            else if (unit is EnemyUnit)
            {
                enemyUnitsAliveAmount--;
            }

            _levelController.TryEndCrowdBattle();
        }

        public void StartBattle()
        {
            for (var i = 0; i != PlayerUnits.Count; ++i)
            {
                PlayerUnits[i].FindTarget();
            }

            for (var i = 0; i != EnemyUnits.Count; ++i)
            {
                EnemyUnits[i].FindTarget();
            }
        }

        public void IncreaseWinnerHealth(Character winner)
        {
            // This method need to be ensure that winner units will be alive at the end of the battle
            // Because for now truly defeated units (in cells calculating) can win in the battle due to simple crowd battle logic.
            // Simple crowd battle logic means that multiple units can attack single unit.
            if (winner is Player)
            {
                for (var i = 0; i != PlayerUnits.Count; ++i)
                    PlayerUnits[i].health = 10;

                for (var i = 0; i != EnemyUnits.Count; ++i)
                    EnemyUnits[i].health = 2;
            }

            else if (winner is Enemy)
            {
                for (var i = 0; i != EnemyUnits.Count; ++i)
                    EnemyUnits[i].health = 10;

                for (var i = 0; i != PlayerUnits.Count; ++i)
                    PlayerUnits[i].health = 2;
            }
        }

        public void PlayPlayerCrowdVictory()
        {
            for (var i = 0; i != PlayerUnits.Count; ++i)
            {
                if (!PlayerUnits[i].isDead)
                    PlayerUnits[i].Win();
            }
        }

        public bool HaveAllUnitsOccupiedCells() =>
            (unitsOccupiedTheirCellsAmount) >= (PlayerUnits.Count + EnemyUnits.Count);
    }
}