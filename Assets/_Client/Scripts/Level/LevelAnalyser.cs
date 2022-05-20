using UnityEngine;

namespace CrowdT.Level
{
    public class LevelAnalyser : MonoBehaviour
    {
        public LevelController LevelController;

        private float _startTime;
        private float _stopTime;

        private void OnEnable()
        {
            LevelController.OnPlayerStartPlaying += StartTimer;
            LevelController.OnLevelEnd += StopTimer;
        }

        private void OnDisable()
        {
            LevelController.OnPlayerStartPlaying -= StartTimer;
            LevelController.OnLevelEnd -= StopTimer;
        }

        public void StartTimer()
        {
            _startTime = Time.time;
        }

        public void StopTimer()
        {
            _stopTime = Time.time;
        }
        
        public float GetLevelDuration()
        {
            if (_stopTime == 0)
                _stopTime = Time.time;

            return _stopTime - _startTime;
        }
    }
}