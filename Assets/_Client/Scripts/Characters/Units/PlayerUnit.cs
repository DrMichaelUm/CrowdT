using KetchappTools.SimpleFeedbacks;
using UnityEngine;

namespace CrowdT
{
    public class PlayerUnit : SingleUnit
    {
        protected override SingleUnit FindClosestTarget() => crowdController.EnemyUnits.FindClosest(transform.position);

        private static int _hitsAmount = 0;
        
        protected override void Hit()
        {
            base.Hit();

            _hitsAmount++;

            // Play hit view only every second view 
            if (_hitsAmount % 2 == 0)
            {
                view.PlayHit();

                // Bound haptics amount, because without it the phone vibrates too much
                if (CrowdController.TotalPlayerUnitsHitsDuringLastSecond <= 7)
                {
                    Feedbacks.Play("Hit", transform.position, Vector3.zero);
                    CrowdController.TotalPlayerUnitsHitsDuringLastSecond++;
                }
                else
                {
                    CrowdController.TotalPlayerUnitsHitsDuringLastSecond--;
                }
            }
        } 
    }
}