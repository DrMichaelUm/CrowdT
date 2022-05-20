using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CrowdT
{
    public class DancingUnit : MonoBehaviour
    {
        [SerializeField] private int danceVariant;
        [SerializeField] private SingleUnitView view;

        private async void OnEnable()
        {
            await Task.Yield(); // Wait for initialization
            view.SetDance(danceVariant);
        }
    }
}