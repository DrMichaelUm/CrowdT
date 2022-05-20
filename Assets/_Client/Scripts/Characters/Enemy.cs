using CrowdT.Level;
using UnityEngine;

namespace CrowdT
{
    public class Enemy : Character
    {
        protected override void Awake()
        {
            base.Awake();
            
            characterEnemy = FindObjectOfType<Player>();
        }

        protected override GroundCell[] GetReachableCells()
        {
            // Get "Enemy Grid Graph" (1 is index) from Pathfinding component on the scene
            var enemyGraph = AstarPath.active.data.graphs[1];
            return levelController.groundGrid.GetReachableCells(enemyGraph, transform.position);
        }

        protected override void SetCellsType(GroundCell[] cells)
        {
            for (var i = 0; i != cells.Length; ++i)
            {
                cells[i].curType = GroundCellType.Enemy;
                cells[i].gameObject.layer = LayerMask.NameToLayer("EnemyGround");
                levelController.groundGrid.enemyCellsAmount++;
            }
        }

        protected override void ColorCellsByType(GroundCell[] cells)
        {
            levelController.groundGrid.ColorCellsByType(GroundCellType.Enemy, cells);
        }
    }
}