using System;
using Ketchapp.MayoSDK;
using UnityEngine;

namespace CrowdT.Level
{
    public class LevelAnalytics : MonoBehaviour
    {
        [SerializeField]
        private LevelController _levelController;

        private void OnEnable()
        {
            _levelController.OnLevelStart += OnLevelStart;
            _levelController.OnLevelSuccess += OnLevelSuccess;
            _levelController.OnLevelFail += OnLevelFail;
            _levelController.OnLevelEnd += OnLevelEnd;
        }

        private void OnLevelStart()
        {
            KetchappSDK.Analytics.GetLevel(Save.data.level + 1).ProgressionStart();
        }

        private void OnLevelSuccess()
        {
            KetchappSDK.Analytics.GetLevel(Save.data.level + 1).ProgressionComplete();
        }

        private void OnLevelFail()
        {
            KetchappSDK.Analytics.GetLevel(Save.data.level + 1).ProgressionFailed();
        }

        private void OnLevelEnd()
        {
            // var levelDuration = _levelAnalyser.GetLevelDuration();
            // KetchappSDK.Analytics.CustomEvent($"Real Time:{Save.data.level + 1}", levelDuration);
        }

        private void OnDisable()
        {
            _levelController.OnLevelStart -= OnLevelStart;
            _levelController.OnLevelSuccess -= OnLevelSuccess;
            _levelController.OnLevelFail -= OnLevelFail;
            _levelController.OnLevelEnd -= OnLevelEnd;
        }
    }
}