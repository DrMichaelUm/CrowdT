using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.Extensions
{
    public static class TransformX
    {
		/// <summary>
		/// Destroy transform's children using Destroy or DestroyImmediate functions.
		/// </summary>
		/// <returns>this Transform</returns>
		public static void SafeDestroyChildren(this Transform container, bool forceImmediate = false)
		{
			Transform[] toDestroy = container.GetOnlyChildren();
			for (int i = 0; i < toDestroy.Length; i++)
			{
				if (container != toDestroy[i])
					ObjectX.SafeDestroy(toDestroy[i].gameObject, forceImmediate);
			}
		}

		/// <summary>
		/// Destroy transform's children with the specific Component using Destroy or DestroyImmediate functions.
		/// </summary>
		/// <returns>this Transform</returns>
		public static void SafeDestroyChildrenOfType<T>(this Transform container, bool forceImmediate = false) where T : Component
		{
			Transform[] toDestroy = container.GetOnlyChildrenOfType<T>();
			for (int i = 0; i < toDestroy.Length; i++)
			{
				if (container != toDestroy[i])
					ObjectX.SafeDestroy(toDestroy[i].gameObject, forceImmediate);
			}
		}

		/// <summary>
		/// Return an array of children's Transform.
		/// </summary>
		public static Transform[] GetOnlyChildren(this Transform container)
		{
			Transform[]	children = new Transform[container.childCount];

			for (int i = 0; i < children.Length; i++)
				children[i] = container.GetChild(i);

			return children;
		}

		/// <summary>
		/// Return an array of children's Transform with the specific Component.
		/// </summary>
		public static Transform[] GetOnlyChildrenOfType<T>(this Transform container) where T : Component
		{
			List<Transform>	children = new List<Transform>();

			for (int i = 0; i < container.childCount; i++)
			{
				if (container.GetChild(i).GetComponent<T>())
					children.Add(container.GetChild(i));
			}

			return children.ToArray();
		}
	}
}