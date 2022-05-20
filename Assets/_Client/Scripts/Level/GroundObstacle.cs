using System;
using UnityEngine;

namespace CrowdT.Level
{
    public class GroundObstacle : MonoBehaviour
    {
        private enum ObstacleType
        {
            Obstacle,
            PlayerTent,
            EnemyTent
        }
        
        [SerializeField] private ObstacleType type;

        private void Start()
        {
            SetCellTypeBelow();
        }

        private void SetCellTypeBelow()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return;
            
            var cell = hit.collider.GetComponent<GroundCell>();

            if (!cell) return;
            
            var groundGrid = FindObjectOfType<LevelController>().groundGrid;

            switch (type)
            {
                case ObstacleType.Obstacle:
                    cell.MarkAs(GroundCellType.Obstacle);

                    break;

                case ObstacleType.PlayerTent:
                    cell.MarkAs(GroundCellType.Player);
                    groundGrid.ColorCellsByType(GroundCellType.Player, cell);
                    groundGrid.playerCell = cell;
                    cell.gameObject.layer = LayerMask.NameToLayer("PlayerGround");

                    break;

                case ObstacleType.EnemyTent:
                    cell.MarkAs(GroundCellType.Enemy);
                    groundGrid.ColorCellsByType(GroundCellType.Enemy, cell);
                    groundGrid.enemyCell = cell;
                    cell.gameObject.layer = LayerMask.NameToLayer("EnemyGround");

                    break;
            }
        }
    }
}