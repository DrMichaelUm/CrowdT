using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KetchappTools.SimpleFeedbacks
{
    public class AutoDestroy : MonoBehaviour
    {
        public float duration = 2f;
        float _timeCpt = 0f;


        /// <summary>
        /// Update
        /// </summary>
        /// 
        void Update()
        {
            _timeCpt += Time.deltaTime;
            if (_timeCpt >= duration) Destroy(gameObject);
        }
    }
}
