using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class RectTransformX
	{
		/// <summary>
		/// Set the X offsetMin of the RecTransform.
		/// </summary>
		public static void SetLeft(this RectTransform rt, float left)
		{
			rt.offsetMin = new Vector2(left, rt.offsetMin.y);
		}

		/// <summary>
		/// Set the X offsetMax of the RecTransform.
		/// </summary>
		public static void SetRight(this RectTransform rt, float right)
		{
			rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
		}

		/// <summary>
		/// Set the Y offsetMax of the RecTransform.
		/// </summary>
		public static void SetTop(this RectTransform rt, float top)
		{
			rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
		}

		/// <summary>
		/// Set the Y offsetMin of the RecTransform.
		/// </summary>
		public static void SetBottom(this RectTransform rt, float bottom)
		{
			rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
		}
	}
}