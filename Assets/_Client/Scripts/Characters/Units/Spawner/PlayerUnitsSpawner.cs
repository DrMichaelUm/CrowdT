using Pathfinding;

namespace CrowdT
{
    public class PlayerUnitsSpawner : UnitsSpawner
    {
        protected override NavGraph GetUnitGraph() => AstarPath.active.data.graphs[0];
    }
}