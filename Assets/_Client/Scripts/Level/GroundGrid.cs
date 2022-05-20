using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace CrowdT.Level
{
    public class GroundGrid : MonoBehaviour
    {
        [SerializeField] private Color playerCellsColor;
        [SerializeField] private Color enemyCellsColor;

        public GroundCell[] grid;
        [HideInInspector] public GroundCell playerCell;
        [HideInInspector] public GroundCell enemyCell;

        [HideInInspector] public int newOpenedCellsAmount;
        [HideInInspector] public int playerCellsAmount;
        [HideInInspector] public int enemyCellsAmount;

        private void Awake()
        {
            InitGroundGrid();
        }

        private void InitGroundGrid()
        {
            grid = FindObjectsOfType<GroundCell>();
        }

        public void ColorCellsByType(GroundCellType type, params GroundCell[] cells)
        {
            if (type == GroundCellType.Player)
                ColorCells(playerCellsColor);

            else if (type == GroundCellType.Enemy)
                ColorCells(enemyCellsColor);

            void ColorCells(Color color)
            {
                for (var i = 0; i != cells.Length; ++i)
                {
                    cells[i].GetComponent<Renderer>().material.color = color;
                }
            }
        }

        public GroundCell[] GetReachableCells(NavGraph graph, Vector3 from)
        {
            var reachableCells = new List<GroundCell>();

            for (var i = 0; i != grid.Length; ++i)
            {
                if (grid[i].curType != GroundCellType.Free) continue;

                if (!grid[i].IsFreeAndReachable(graph, from)) continue;

                reachableCells.Add(grid[i]);
            }

            return reachableCells.ToArray();
        }

        public GroundCell[] GetNewOpenedCellsIn(GroundCell[] cells)
        {
            var newCells = new List<GroundCell>();

            for (var i = 0; i != cells.Length; ++i)
            {
                if (cells[i].curType == GroundCellType.Free)
                    newCells.Add(cells[i]);
            }

            newOpenedCellsAmount = newCells.Count;

            return newCells.ToArray();
        }

        public bool IsBarrierBetweenArmies()
        {
            var totalGraph = AstarPath.active.data.graphs[2];
            totalGraph.Scan();

            return !playerCell.IsReachable(totalGraph, enemyCell.transform.position);
        }

        public bool AreThereFreeCells()
        {
            for (var i = 0; i != grid.Length; ++i)
            {
                if (grid[i].curType == GroundCellType.Free)
                    return true;
            }

            return false;
        }
    }
}