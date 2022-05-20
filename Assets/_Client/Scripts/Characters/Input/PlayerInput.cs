using System.Threading.Tasks;
using CrowdT.Level;
using KetchappTools.SimpleFeedbacks;
using UnityEngine;

namespace CrowdT
{
    public class PlayerInput : CharacterInput
    {
        [SerializeField] private Barrier barrierToRemoveInTutorial;
        private Vector3 _barrierToRemoveInTutorialPos;

        private TurnBasedBattleSystem _turnBasedBattleSystem;
        private LevelController _levelController;

        private Camera _mainCam;

        private void Start()
        {
            view = FindObjectOfType<PlayerInputView>();

            if (barrierToRemoveInTutorial)
                _barrierToRemoveInTutorialPos = barrierToRemoveInTutorial.transform.position;

            _turnBasedBattleSystem = FindObjectOfType<TurnBasedBattleSystem>();
            _levelController = FindObjectOfType<LevelController>();

            _mainCam = Camera.main;
        }

        private void Update()
        {
            CheckForClickOnBarrier();
        }

        private void CheckForClickOnBarrier()
        {
            if (Input.GetMouseButtonDown(0) && _turnBasedBattleSystem.state == TurnState.PlayerTurn)
            {
                var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

                if (!Physics.Raycast(ray, out var hit)) return;

                var barrier = hit.collider.GetComponent<Barrier>();

                if (barrier)
                {
                    barrier.RemoveWithNotification();
                    Feedbacks.Play("PlayerRemoveBarrier", transform.position, Vector3.zero);
                }
            }
        }

        public override void StartTurn()
        {
            base.StartTurn();

            TryShowTutorial();
        }

        private void TryShowTutorial()
        {
            // show the tutorial on the first player turn of the first level of the session
            if (!FirstPlayerTurnOnLevelMadeTracker.IsMade)
            {
                view.Show(_barrierToRemoveInTutorialPos);
            }
        }

        public override async Task<bool> IsExecuted()
        {
            if (!turnMade) return false;

            turnMade = false;

            if (!FirstPlayerTurnOnLevelMadeTracker.IsMade)
            {
                view.Hide();

                _levelController.OnPlayerStartPlaying?.Invoke();
            }

            return true;
        }

        public override void UpdateObserver()
        {
            if (_turnBasedBattleSystem.state == TurnState.PlayerTurn)
                base.UpdateObserver();
        }
    }
}