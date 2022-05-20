using UnityEngine;

namespace KetchappTools.Core.Extensions
{
    public static class LayerMaskX
    {
		/// <summary>
		/// Returns true if this LayerMask contains the specific Layer.
		/// </summary>
		public static bool Contains(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
	}
}