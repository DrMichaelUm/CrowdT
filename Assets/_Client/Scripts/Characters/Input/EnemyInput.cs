using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdT.Level;
using KetchappTools.SimpleFeedbacks;
using UnityEngine;

namespace CrowdT
{
    public class EnemyInput : CharacterInput
    {
        [SerializeField] private bool useAI;

        [SerializeField] private float delayBeforeTurn = 1;

        [SerializeField] private List<Barrier> barriersRemoveOrder;

        private BarrierController _barrierController;
        private TurnBasedBattleSystem _turnBasedBattleSystem;

        private void Awake()
        {
            _barrierController = FindObjectOfType<BarrierController>();
        }

        private void Start()
        {
            _turnBasedBattleSystem = FindObjectOfType<TurnBasedBattleSystem>();
            view = FindObjectOfType<EnemyInputView>();
        }

        public override async Task<bool> IsExecuted() => await TryMakeTurn();

        private async Task<bool> TryMakeTurn()
        {
            if (!useAI)
                return true;

            await new WaitForSeconds(delayBeforeTurn);

            if (LevelController.IsEnd) 
                return true;

            barriersRemoveOrder = GetAvailableBarriers();

            if (barriersRemoveOrder.Count > 0)
            {
                var barrierToRemove = barriersRemoveOrder?[0];

                if (barrierToRemove != null)
                {
                    view.Show(barrierToRemove.transform.position);

                    // Wait while hand press animation play to the press moment
                    await new WaitForSeconds(view.delayBeforeFadeIn + view.fadeInDuration + view.delayBeforeTouch);

                    barrierToRemove.RemoveWithNotification();
                    Feedbacks.Play("EnemyRemoveBarrier", transform.position, Vector3.zero);

                    await new WaitForSeconds(.26f); // Wait for hand touch up animation

                    view.Hide();
                }
            }

            return true;
        }

        private List<Barrier> GetAvailableBarriers()
        {
            if (barriersRemoveOrder == null || _barrierController.barriers == null) return null;

            var result = barriersRemoveOrder.Intersect(_barrierController.barriers).ToList();

            return result;
        }

        public override void UpdateObserver()
        {
            if (_turnBasedBattleSystem.state == TurnState.EnemyTurn)
                base.UpdateObserver();
        }
    }
}