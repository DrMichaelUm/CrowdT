using System;
using System.Collections.Generic;
using System.Linq;
using KetchappTools.SimpleFeedbacks;
using UnityEngine;

namespace CrowdT.Level
{
    public class BarrierController : MonoBehaviour
    {
        public List<Barrier> barriers;

        private void Awake()
        {
            barriers = GetComponentsInChildren<Barrier>().ToList();
        }

        public void RemoveAll()
        {
            Feedbacks.Play("SimpleRemoveBarrier", transform.position, Vector3.zero);
            
            for (var i = barriers.Count - 1; i != -1; --i)
            {
                barriers[i].Remove();
            }
        }

        public void Remove(Barrier barrier)
        {
            barriers.Remove(barrier);
        }
    }
}