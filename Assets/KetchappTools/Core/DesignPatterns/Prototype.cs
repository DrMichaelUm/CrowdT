using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns
{
    public abstract class Prototype<T> : MonoBehaviour where T : Prototype<T>
    {
        #region Properties

        protected T clonedFrom = null;
        protected T original { get { if (isOriginal) return (T)this; return clonedFrom.Original; } }
        public T Original => original;

        protected bool isOriginal => clonedFrom == null;
        public bool IsOriginal => isOriginal;

        private List<T> poolClones = new List<T>();

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            // Disable if original
            if (isOriginal)
                gameObject.SetActive(false);
        }

        protected virtual void OnDestroy()
        {
            // Delete pool clones if delete original
            foreach (T iClone in poolClones)
                Destroy(iClone.gameObject);
        }

        #endregion

        #region Methods

        public T Clone()
        {
            T clone = null;

            // Re-use from the pool
            if(poolClones.Count > 0)
            {
                int index = poolClones.Count - 1;
                clone = poolClones[index];
                poolClones.RemoveAt(index);
            }

            // Use fresh instance
            else
            {
                clone = UnityEngine.Object.Instantiate((T)this);

                Transform originalTransform = transform;
                Transform cloneTransform = clone.transform;
                cloneTransform.SetParent(originalTransform.parent, worldPositionStays: false);
                cloneTransform.localPosition = originalTransform.localPosition;
                cloneTransform.localRotation = originalTransform.localRotation;
                cloneTransform.localScale = originalTransform.localScale;

                RectTransform originalRectTransform = (RectTransform)originalTransform;
                if (originalRectTransform)
                {
                    RectTransform cloneRectTransform = (RectTransform)cloneTransform;
                    cloneRectTransform.anchorMin = originalRectTransform.anchorMin;
                    cloneRectTransform.anchorMax = originalRectTransform.anchorMax;
                    cloneRectTransform.pivot = originalRectTransform.pivot;
                    cloneRectTransform.sizeDelta = originalRectTransform.sizeDelta;
                }

                clone.clonedFrom = (T)this;
            }

            // Activate clone
            clone.gameObject.SetActive(true);
            clone.OnClone();

            return clone;
        }

        public void Unclone()
        {
            // Be sure it's not the original
            if(!isOriginal)
            {
                // Disable clone
                gameObject.SetActive(false);
                OnUnclone();

                // Send back to the pool
                Original.poolClones.Add((T)this);
            }
        }

        #endregion

        #region Event Methods

        protected abstract void OnClone();

        protected abstract void OnUnclone();

        #endregion
    }
}