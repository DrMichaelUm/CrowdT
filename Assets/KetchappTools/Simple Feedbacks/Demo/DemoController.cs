using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.SimpleFeedbacks
{
    public class DemoController : MonoBehaviour
    {
        /// <summary>
        /// SoundToggleDidChange
        /// </summary>
        /// 
        public void SoundToggleDidChange(bool value)
        {
            Feedbacks.EnableSound(value);
        }


        /// <summary>
        /// SoundToggleDidChange
        /// </summary>
        /// 
        public void HapticToggleDidChange(bool value)
        {
            Feedbacks.EnableHaptic(value);
        }


        /// <summary>
        /// ButtonHasBeenPushed
        /// </summary>
        /// <param name="label"></param>
        /// 
        public void ButtonHasBeenPushed(string label)
        {
            Feedbacks.Play(label, new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 2f), 2f + Random.Range(0f, 5f)), Vector3.zero);
        }
    }
}
