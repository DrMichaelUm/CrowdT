using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class RectX
	{
		/// <summary>
		/// Returns a random Vector2 (world position) inside the Rect area.
		/// </summary>
		public static Vector2 GetRandomPosition(this Rect rect)
		{
			return new Vector2(rect.position.x + Random.Range(-rect.width * 0.5F, rect.width * 0.5F),
							   rect.position.y + Random.Range(-rect.height * 0.5F, rect.height * 0.5F));
		}
	}
}