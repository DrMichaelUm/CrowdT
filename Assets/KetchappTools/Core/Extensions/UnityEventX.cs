using UnityEngine;
using UnityEngine.Events;

namespace KetchappTools.Core.Extensions
{
	public static partial class UnityEventX
	{
		[System.Serializable]
		public class UEGeneric<T> : UnityEvent<T> {}

		[System.Serializable]
		public class UEGeneric<T1, T2> : UnityEvent<T1, T2> {}

		[System.Serializable]
		public class UEGeneric<T1, T2, T3> : UnityEvent<T1, T2, T3> {}

		[System.Serializable]
		public class UEGeneric<T1, T2, T3, T4> : UnityEvent<T1, T2, T3, T4> {}


		[System.Serializable]
		public class UEBool : UnityEvent<bool> {}

		[System.Serializable]
		public class UEDouble : UnityEvent<double> {}

		[System.Serializable]
		public class UEFloat : UnityEvent<float> {}

		[System.Serializable]
		public class UEInt : UnityEvent<int> {}

		[System.Serializable]
		public class UEString : UnityEvent<string> {}

		[System.Serializable]
		public class UECollider : UnityEvent<Collider> {}

		[System.Serializable]
		public class UECollision : UnityEvent<Collision> {}

		[System.Serializable]
		public class UEVector2 : UnityEvent<Vector2> {}

		[System.Serializable]
		public class UEVector3 : UnityEvent<Vector3> {}
	}
}