using Pathfinding;

namespace CrowdT
{
    public class EnemyUnitsSpawner : UnitsSpawner
    {
        protected override NavGraph GetUnitGraph() => AstarPath.active.data.graphs[1];
    }
}