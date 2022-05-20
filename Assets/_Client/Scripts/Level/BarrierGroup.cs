using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrowdT.Level
{
    public class BarrierGroup : MonoBehaviour
    {
        private List<Barrier> barriers;

        private void Awake()
        {
            barriers = GetComponentsInChildren<Barrier>().ToList();
        }

        public void RemoveTheGroupCompletely()
        {
            for (var i = barriers.Count - 1; i != -1; --i)
            {
                barriers[i].Remove();
                barriers.RemoveAt(i);
            }
        }
    }
}