using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CrowdT
{
    public class MovementToTarget : MonoBehaviour
    {
        private SingleUnit _unit;
        public Action OnComplete;

        private void Awake()
        {
            _unit = GetComponent<SingleUnit>();
        }

        public async Task MoveTo(SingleUnit target, float speed)
        {
            while (!target.isDead && !IsReached(target.transform.position))
            {
                if (_unit.isDead) break;

                var dir = target.transform.position - transform.position;
                var velocity = dir.normalized * speed;

                transform.forward = dir.normalized;

                var newPos = transform.position + velocity * Time.deltaTime;

                transform.position = newPos;

                await Task.Yield();
            }

            if (!target.isDead && IsReached(target.transform.position))
                OnComplete?.Invoke();

            bool IsReached(Vector3 pos)
            {
                return (transform.position - pos).sqrMagnitude < 1f;
            }
        }
    }
}