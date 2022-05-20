using UnityEngine;

namespace Ketchapp.CrowdTerritory
{
    public class CameraZoom : MonoBehaviour
    {
        private float _startFieldOfView;
        private float _standardHeightWidthRatio;

        private void Awake()
        {
            _startFieldOfView = GetComponent<Camera>().fieldOfView;
            _standardHeightWidthRatio = 1920f / 1080f;
            ZoomCameraDependsOnScreenWidth();
        }

        private void ZoomCameraDependsOnScreenWidth()
        {
            var currentHeightWidthRatio = Screen.height / (float)Screen.width;
            var screensRatio = currentHeightWidthRatio / _standardHeightWidthRatio;
            GetComponent<Camera>().fieldOfView = _startFieldOfView * screensRatio;
        }
    }
}
