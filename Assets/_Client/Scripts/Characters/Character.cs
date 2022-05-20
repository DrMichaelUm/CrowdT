using System.Threading.Tasks;
using CrowdT.Level;
using UnityEngine;

namespace CrowdT
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private UnitsSpawner _unitsSpawner;

        protected Character characterEnemy;

        private CharacterInput _input;
        private TentView _view;

        protected LevelController levelController;

        protected virtual void Awake()
        {
            _input = GetComponent<CharacterInput>();
            _view = GetComponentInChildren<TentView>();

            levelController = FindObjectOfType<LevelController>();
        }

        private async void Start()
        {
            await Task.Yield(); // Delay to wait while everything is initialized
            FindAndTakeFreeCells();
        }

        private async void FindAndTakeFreeCells()
        {
            await new WaitForSeconds(.01f); // Delay to wait to initialize units path

            UpdatePathfindingGrid();

            this.TakeCells();
            characterEnemy.TakeCells();

            UpdatePathfindingGrid();

            await new WaitForSeconds(.2f); // Delay to wait while all spawned units will be registered 

            await levelController.TryEndGame();
        }

        public async Task ExecuteTurn()
        {
            _input.StartTurn();

            do
            {
                await Task.Yield();
            } 
            while (await _input.IsExecuted() == false);

            if (!LevelController.CrowdBattleStarted)
            {
                FindAndTakeFreeCells();
            }
        }

        private void UpdatePathfindingGrid()
        {
            AstarPath.active.data.graphs[0].Scan();
            AstarPath.active.data.graphs[1].Scan();
        }

        private void TakeCells()
        {
            var allReachableCells = GetReachableCells();

            var newCells = levelController.groundGrid.GetNewOpenedCellsIn(allReachableCells);

            if (newCells.Length == 0) return;

            SetCellsType(newCells);

            ColorCellsByType(newCells);

            SpawnUnits(newCells);
        }

        protected abstract GroundCell[] GetReachableCells();

        protected abstract void SetCellsType(GroundCell[] cells);

        protected abstract void ColorCellsByType(GroundCell[] cells);

        private void SpawnUnits(GroundCell[] targetCells)
        {
            _view.OpenDoor();

            for (var i = 0; i != levelController.groundGrid.newOpenedCellsAmount; ++i)
            {
                var previousRandOffset = Vector3.zero;

                for (var j = 0; j != levelController.crowdController.unitsPerCell; ++j)
                {
                    var randomOffset = GetRandomCellOffset();

                    while (UnitsAreTooClose())
                        randomOffset = GetRandomCellOffset();

                    previousRandOffset = randomOffset;

                    var targetPos = targetCells[i].transform.position + randomOffset;

                    _unitsSpawner.Spawn(1, targetPos);

                    bool UnitsAreTooClose()
                    {
                        return (previousRandOffset - randomOffset).sqrMagnitude <
                               levelController.crowdController.unitsOffset.x *
                               levelController.crowdController.unitsOffset.x;
                    }
                }
            }

            _view.CloseDoor();
        }

        private Vector3 GetRandomCellOffset()
        {
            var randomXOffset = Random.Range(-levelController.crowdController.unitsOffset.x,
                                             levelController.crowdController.unitsOffset.x);

            var randomZOffset = Random.Range(-levelController.crowdController.unitsOffset.z,
                                             levelController.crowdController.unitsOffset.z);

            var randomOffset = new Vector3(randomXOffset, 0, randomZOffset);

            return randomOffset;
        }
    }
}