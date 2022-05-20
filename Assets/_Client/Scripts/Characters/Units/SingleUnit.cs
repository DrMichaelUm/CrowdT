using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrowdT
{
    [Serializable]
    public class PathData
    {
        public List<Vector3> path;
        public int waypointIndex;
        public bool reachedEndOfPath;
    }

    public class SingleUnit : MonoBehaviour
    {
        [SerializeField] private float pathSpeed;
        [SerializeField] private float battleSpeed;

        [HideInInspector] public int health = 1;
        [SerializeField] private int damage;
        [ReadOnly] public bool isDead;
        
        [SerializeField] protected SingleUnitView view;

        [ReadOnly] public PathData pathData;

        [ReadOnly] public SingleUnit targetUnit;
        [ReadOnly] public SingleUnit attackedBy;

        [HideInInspector] public bool targetsExist = true;
        
        private MovementByPath _movementByPath;
        private MovementToTarget _movementToTarget;
        [HideInInspector] public CrowdController crowdController;

        private void Awake()
        {
            _movementByPath = GetComponent<MovementByPath>();
            _movementToTarget = GetComponent<MovementToTarget>();
        }

        private void OnEnable()
        {
            _movementByPath.OnStart += () => view.SetIdle(false);
            _movementByPath.OnComplete += () => crowdController.unitsOccupiedTheirCellsAmount++;
            _movementByPath.OnComplete += () => view.SetIdle(true);
            _movementToTarget.OnComplete += Attack;
        }

        private async void Start()
        {
            await _movementByPath.Move(pathData, pathSpeed);
        }

        public void FindTarget()
        {
            if (isDead) return;

            targetUnit = FindClosestTarget();

            if (!targetUnit)
            {
                targetsExist = false;

                return;
            }

            targetUnit.attackedBy = this;

            StartCheckingForTargetAlive();
            MoveToTargetUnit();
        }

        protected virtual SingleUnit FindClosestTarget() => null;

        private async void StartCheckingForTargetAlive()
        {
            while (targetsExist && !targetUnit.isDead)
            {
                await Task.Yield();
            }

            view.SetIdle(true);

            if (targetsExist)
                FindTarget();
        }

        private async void MoveToTargetUnit()
        {
            if (targetUnit == null) return;

            view.SetIdle(false);
            
            await _movementToTarget.MoveTo(targetUnit, battleSpeed);
        }

        private async void Attack()
        {
            view.SetIdle(true);
            view.SetAttack();

            const int delayBetweenHits = 1;

            while (!isDead)
            {
                await new WaitForSeconds(delayBetweenHits);

                if (targetUnit)
                    Hit();
            }

            FindTarget();
        }

        protected virtual void Hit()
        {
            targetUnit.TakeDamage(damage);
        }

        private void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
                Die();
        }

        private void Die()
        {
            if (isDead) return;

            view.SetRandomDeath();

            isDead = true;
            
            crowdController.UnregisterUnit(this);
        }

        public void Win()
        {
            view.SetRandomDance();
        }
        
        private void OnDisable()
        {
            _movementByPath.OnStart -= () => view.SetIdle(false);
            _movementByPath.OnComplete -= () => view.SetIdle(true);
            _movementByPath.OnComplete -= () => crowdController.unitsOccupiedTheirCellsAmount++;
            _movementToTarget.OnComplete -= Attack;
        }
    }
}