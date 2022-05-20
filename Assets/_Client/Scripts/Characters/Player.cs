using CrowdT.Level;
using UnityEngine;

namespace CrowdT
{
    public class Player : Character
    {
        protected override void Awake()
        {
            base.Awake();
            
            characterEnemy = FindObjectOfType<Enemy>();
        }

        protected override GroundCell[] GetReachableCells()
        {
            // Get "Player Grid Graph" (0 is index) from Pathfinding component on the scene
            var playerGraph = AstarPath.active.data.graphs[0];
            return levelController.groundGrid.GetReachableCells(playerGraph, transform.position);
        }

        protected override void SetCellsType(GroundCell[] cells)
        {
            for (var i = 0; i != cells.Length; ++i)
            {
                cells[i].curType = GroundCellType.Player;
                cells[i].gameObject.layer = LayerMask.NameToLayer("PlayerGround");
                levelController.groundGrid.playerCellsAmount++;
            }
        }

        protected override void ColorCellsByType(GroundCell[] cells)
        {
            levelController.groundGrid.ColorCellsByType(GroundCellType.Player, cells);
        }
    }
}