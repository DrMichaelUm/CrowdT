using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	//TODO: Add summaries.

	public static class ObjectX
	{
		/// <summary>
		/// Destroy object using Destroy or DestroyImmediate functions.
		/// </summary>
		/// <returns>this Transform</returns>
		public static T SafeDestroy<T>(T obj, bool forceImmediate = false) where T : Object //TODO: Move location?
		{
			#if UNITY_EDITOR
			//Debug.Log("SafeDestroying " + obj.name);
			if (!Application.isPlaying || forceImmediate)
				Object.DestroyImmediate(obj);
			else
			#endif
				Object.Destroy(obj);

			return null;
		}
	}
}
