using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdT.Level;
using Pathfinding;
using UnityEngine;

namespace CrowdT
{
    public abstract class UnitsSpawner : MonoBehaviour
    {
        [SerializeField] private SingleUnit unitPrefab;

        [SerializeField] private float spawnInterval;

        private LevelController _levelController;

        private void Awake()
        {
            _levelController = FindObjectOfType<LevelController>();
        }

        public async void Spawn(int amount, Vector3 targetPos)
        {
            for (var i = 0; i != amount; ++i)
            {
                var cooldown = 0f;

                while (cooldown < spawnInterval)
                {
                    cooldown += Time.deltaTime;
                    await Task.Yield();
                }

                var unit = Instantiate(unitPrefab, transform.position, Quaternion.identity);
                _levelController.crowdController.RegisterUnit(unit);
                
                unit.pathData.path = MakePath(new Vector3(transform.position.x, 0, transform.position.z), targetPos);
            }
        }

        // calculates a path and copies it from A*PFP's vectorPath to a FixedList4096
        private List<Vector3> MakePath(Vector3 fromPos, Vector3 toPos)
        {
            var destPath = new List<Vector3>();

            var unitGraph = GetUnitGraph();

            var fromNode = unitGraph.GetNearest(fromPos, NNConstraint.Default).node;
            var toNode = unitGraph.GetNearest(toPos, NNConstraint.Default).node;

            if (PathUtilities.IsPathPossible(fromNode, toNode))
            {
                ABPath path = null;
                path = ABPath.Construct((Vector3) fromNode.position, (Vector3) toNode.position);
                AstarPath.StartPath(path);
                path.BlockUntilCalculated();

                for (var i = 0; i != path.vectorPath.Count; ++i)
                {
                    destPath.Add(path.vectorPath[i]);
                }
            }

            return destPath;
        }

        protected abstract NavGraph GetUnitGraph();
    }
}