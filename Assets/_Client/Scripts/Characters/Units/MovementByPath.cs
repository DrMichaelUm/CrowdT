using System;
using System.Threading.Tasks;
using CrowdT.Level;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CrowdT
{
    public class MovementByPath : MonoBehaviour
    {
        [SerializeField] private float distanceToSwitchToNextWaypoint;

        public Action OnStart;
        public Action OnComplete;
        
        public async Task Move(PathData pathData, float speed)
        {
            if (pathData.path.Count == 0)
            {
                OnComplete?.Invoke();
                return;
            }

            OnStart?.Invoke();
            
            var targetPos = pathData.path[pathData.path.Count - 1];

            while (IsNotReached(targetPos))
            {
                if (LevelController.CrowdBattleStarted) return;
                
                TryMoveToNextWaypoint(pathData);

                var speedFactor = pathData.reachedEndOfPath ? 0 : 1;

                var dir = pathData.path[pathData.waypointIndex] - transform.position;
                var velocity = dir.normalized * speed * speedFactor;

                transform.forward = dir.normalized;

                var newPos = transform.position + velocity * Time.deltaTime;
                transform.position = newPos;

                // await UniTask.Yield();
                
                await Task.Yield();
            }
            
            OnComplete?.Invoke();

            bool IsNotReached(Vector3 pos)
            {
                return (transform.position - pos).sqrMagnitude >
                       distanceToSwitchToNextWaypoint * distanceToSwitchToNextWaypoint;
            }
        }

        private void TryMoveToNextWaypoint(PathData pathData)
        {
            var sqrDistanceToWaypoint = (transform.position - pathData.path[pathData.waypointIndex]).sqrMagnitude;

            if (sqrDistanceToWaypoint < distanceToSwitchToNextWaypoint * distanceToSwitchToNextWaypoint)
            {
                if (pathData.waypointIndex + 1 < pathData.path.Count)
                    pathData.waypointIndex++;
                else
                    pathData.reachedEndOfPath = true;
            }
        }
    }
}