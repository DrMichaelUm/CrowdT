using DG.Tweening;
using KetchappTools.Menus.Transitions;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KetchappTools.Menus
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class Menu : MonoBehaviour
    {
        #region Enums

        public enum States
        {
            NotInit,
            Hidden,
            Hide,
            Shown,
            Show
        }

        #endregion

        #region Properties

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/KetchappTools/Menus/Manager", false, 1)]
        public static void CreateMenuManager()
        {
            GameObject menuManagerGameObject = new GameObject(typeof(MenuManager).Name);
            UnityEditor.Undo.RegisterCreatedObjectUndo(menuManagerGameObject, "Create " + typeof(MenuManager).Name);
            menuManagerGameObject.AddComponent<MenuManager>();
            UnityEditor.Selection.activeGameObject = menuManagerGameObject;
        }

        [Button("Show"), FoldoutGroup("Debug", order: 100), DisableIf("DisableIfEditorShowMenu")]
        private void EditorShowMenu()
        {
            Show();
        }
        private bool DisableIfEditorShowMenu()
        {
            return state != States.Hidden;
        }

        [Button("Hide"), FoldoutGroup("Debug", order: 100), DisableIf("DisableIfEditorHideMenu")]
        private void EditorHideMenu()
        {
            Hide();
        }
        private bool DisableIfEditorHideMenu()
        {
            return state != States.Shown;
        }
#endif

        public string ID => gameObject.name;

        protected States state = States.NotInit;
        /// <summary>
        /// Return the state of the menu :
        /// Shown -> displayed,
        /// Show -> transition to be shown,
        /// Hidden -> not displayed,
        /// Hide -> transition to be hidden
        /// </summary>
        public States State => state;

        /// <summary>
        /// Return true when the menu is displayed, including during the transitions in and out
        /// </summary>
        public bool IsDisplayed => state != States.NotInit || state != States.Hide;

        /// <summary>
        /// Return true when the menu is shown or during the in transition
        /// </summary>
        public bool IsShownOrAlmost => state == States.Show || state == States.Shown;

        /// <summary>
        /// Return true when the menu is hidden or during the out transition
        /// </summary>
        public bool IsHiddenOrAlmost => state == States.Hide || state == States.Hidden;

        [SerializeField, FoldoutGroup("Basics")]
        protected Transition transitionIn = null;

        [SerializeField, FoldoutGroup("Basics")]
        protected Transition transitionOut = null;

        [SerializeField, FoldoutGroup("Basics")]
        protected bool showAtInit = false;

        [SerializeField, FoldoutGroup("Basics")]
        protected Body body = null;
        public Body Body => body;

#if UNITY_EDITOR
        public void EditorSetBody(Body _body)
        {
            body = _body;
        }
#endif

        [SerializeField, FoldoutGroup("Events")]
        public UnityEvent OnShowStartEvent = null;

        [SerializeField, FoldoutGroup("Events")]
        public UnityEvent OnShowEndEvent = null;

        [SerializeField, FoldoutGroup("Events")]
        public UnityEvent OnHideStartEvent = null;

        [SerializeField, FoldoutGroup("Events")]
        public UnityEvent OnHideEndEvent = null;

        #endregion

        #region Methods

        protected virtual void OnShowStart() { }
        protected virtual void OnShowEnd() { }
        protected virtual void OnHideStart() { }
        protected virtual void OnHideEnd() { }

        public virtual void Initialize()
        {
            if (showAtInit)
                ShowImmediately();
            else
                HideImmediately();
        }

        /// <summary>
        /// Show he menu without transition
        /// </summary>
        public void ShowImmediately(Action callback = null, bool closeShownMenus = false)
        {
            if(closeShownMenus)
                MenuManager.Instance.HideAllMenusImmediately();

            state = States.Shown;
            gameObject.SetActive(true);
            OnShowStart();
            OnShowStartEvent?.Invoke();
            callback?.Invoke();
            OnShowEnd();
            OnShowEndEvent?.Invoke();
        }

        /// <summary>
        /// Show the menu with transition
        /// </summary>
        public void Show(Action callback = null, bool closeShownMenus = false, Transition transition = null, bool autoBlock = true)
        {
            if (closeShownMenus)
                MenuManager.Instance.HideAllMenus();

            if (state != States.Hidden)
            {
                Debug.Log("The menu is already shown");
                OnShowStart();
                OnShowStartEvent?.Invoke();
                callback?.Invoke();
                OnShowEnd();
                OnShowEndEvent?.Invoke();
                return;
            }

            // Get transition by priority
            if (!transition)
            {
                if (transitionIn)
                    transition = transitionIn;
                else
                    transition = MenuManager.Instance.TransitionIn;
            }

            // If any transition found show immediately
            if (!transition)
                ShowImmediately(callback);
            else
            {
                state = States.Show;
                OnShowStart();
                OnShowStartEvent?.Invoke();
                gameObject.SetActive(true);
                if (autoBlock)
                    MenuManager.Instance.Block();
                transition.Play(this, ()=>
                {
                    state = States.Shown;
                    if (autoBlock)
                        MenuManager.Instance.Unblock();
                    callback?.Invoke();
                    OnShowEnd();
                    OnShowEndEvent?.Invoke();
                });
            }
        }

        /// <summary>
        /// Hide the menu without transition
        /// </summary>
        public void HideImmediately(Action callback = null)
        {
            state = States.Hidden;
            gameObject.SetActive(false);
            OnHideStart();
            OnHideStartEvent?.Invoke();
            callback?.Invoke();
            OnHideEnd();
            OnHideEndEvent?.Invoke();
        }

        /// <summary>
        /// Hide the menu with transition
        /// </summary>
        public void Hide(Action callback = null, Transition transition = null, bool autoBlock = true)
        {
            if (state != States.Shown)
            {
                Debug.Log("The menu is already hidden");
                OnHideStart();
                OnHideStartEvent?.Invoke();
                callback?.Invoke();
                OnHideEnd();
                OnHideEndEvent?.Invoke();
                return;
            }

            // Get transition by priority
            if (!transition)
            {
                if (transitionOut)
                    transition = transitionOut;
                else
                    transition = MenuManager.Instance.TransitionOut;
            }

            // If any transition found hide immediately
            if (!transition)
                HideImmediately(callback);
            else
            {
                state = States.Hide;
                OnHideStart();
                OnHideStartEvent?.Invoke();
                if (autoBlock)
                    MenuManager.Instance.Block();
                transition.Play(this, () =>
                {
                    state = States.Hidden;
                    gameObject.SetActive(false);
                    transition.SetToFrom(this);
                    if (autoBlock)
                        MenuManager.Instance.Unblock();
                    callback?.Invoke();
                    OnHideEnd();
                    OnHideEndEvent?.Invoke();
                });
            }
        }

        #endregion
    }
}