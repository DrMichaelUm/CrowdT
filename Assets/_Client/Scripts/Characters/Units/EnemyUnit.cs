namespace CrowdT
{
    public class EnemyUnit : SingleUnit
    {
        protected override SingleUnit FindClosestTarget() =>
            crowdController.PlayerUnits.FindClosest(transform.position);
    }
}