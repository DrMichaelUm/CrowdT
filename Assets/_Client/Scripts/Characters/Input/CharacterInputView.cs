using DG.Tweening;
using KetchappTools.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace CrowdT
{
    public class CharacterInputView : MonoBehaviour
    {
        public float delayBeforeFadeIn;
        public float fadeInDuration;
        public float delayBeforeTouch;
        public float delayBeforeFadeOut;
        public float fadeOutDuration;

        [SerializeField] private GameObject hand;
        private Animator _animator;
        private Image _handImage;
        
        [SerializeField] private Image tapCircle;
        private Animator _tapCircleAnimator;

        private RectTransform _rectTransform;

        private RectTransform _canvasRect;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _animator = GetComponent<Animator>();
            _tapCircleAnimator = tapCircle.GetComponent<Animator>();

            _handImage = hand.GetComponent<Image>();

            _canvasRect = MenuManager.Instance.MenuList.GameWindow.GetComponent<RectTransform>();
        }

        public void Show(Vector3 worldPos)
        {
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPos);

            var worldObjectScreenPosition =
                new Vector2(((viewportPosition.x * _canvasRect.sizeDelta.x) - (_canvasRect.sizeDelta.x * 0.5f)),
                            ((viewportPosition.y * _canvasRect.sizeDelta.y) - (_canvasRect.sizeDelta.y * 0.5f)));

            _rectTransform.anchoredPosition = worldObjectScreenPosition;
            
            Show();
        }

        private async void Show()
        {
            await new WaitForSeconds(delayBeforeFadeIn);
            
            if (IsShouldBeAborted()) return;

            PlayAppearanceEffect();
        }

        protected virtual bool IsShouldBeAborted() => false;

        private async void PlayAppearanceEffect()
        {
            _handImage.DOFade(1, fadeInDuration);;
            tapCircle.DOFade(1, fadeInDuration);

            await new WaitForSeconds(delayBeforeTouch);
            
            _animator.enabled = true;
            _tapCircleAnimator.enabled = true;
            _animator.Rebind();
            _tapCircleAnimator.Rebind();
        }

        public async void Hide()
        {
            _animator.enabled = false;
            _tapCircleAnimator.enabled = false;
            
            await new WaitForSeconds(delayBeforeFadeOut);
            
            tapCircle.DOFade(0, fadeOutDuration);
            _handImage.DOFade(0, fadeOutDuration);
        }
    }
}