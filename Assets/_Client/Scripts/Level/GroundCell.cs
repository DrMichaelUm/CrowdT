using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrowdT.Level
{
    public enum GroundCellType
    {
        Free,
        Obstacle,
        Player,
        Enemy
    }

    public class GroundCell : MonoBehaviour
    {
        [ReadOnly] public GroundCellType curType = GroundCellType.Free;

        public void MarkAs(GroundCellType type)
        {
            curType = type;
        }

        public bool IsFreeAndReachable(NavGraph graph, Vector3 from) =>
            curType == GroundCellType.Free && IsReachable(graph, from);

        public bool IsReachable(NavGraph graph, Vector3 from)
        {
            var fromNode = graph.GetNearest(from, NNConstraint.Default).node;
            var toNode = graph.GetNearest(transform.position, NNConstraint.Default).node;

            return PathUtilities.IsPathPossible(fromNode, toNode);
        }
    }
}