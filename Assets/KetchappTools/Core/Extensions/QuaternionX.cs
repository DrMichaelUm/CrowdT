using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class QuaternionX
	{
		/// <summary>
		/// Returns True if these Quaternions are approximately equal, otherwise false.
		/// </summary>
		public static bool			IsEqualTo(this Quaternion _quaternion, Quaternion _other, float tolerance)
		{
			float angle = Quaternion.Angle(_quaternion, _other);

			if (MathX.IsInsideIn(angle,-tolerance, tolerance))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Returns this Quaternion with only X rotation.
		/// </summary>
		public static Quaternion	OnlyX(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns this Quaternion with only Y rotation.
		/// </summary>
		public static Quaternion	OnlyY(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns this Quaternion with only Z rotation.
		/// </summary>
		public static Quaternion	OnlyZ(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns this Quaternion without X rotation.
		/// </summary>
		public static Quaternion	WithoutX(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns this Quaternion without X rotation.
		/// </summary>
		public static Quaternion	WithoutY(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns this Quaternion without X rotation.
		/// </summary>
		public static Quaternion	WithoutZ(this Quaternion _quaternion)
		{
			//TODO

			return Quaternion.identity;
		}

		/// <summary>
		/// Returns a random Quaternion.
		/// </summary>
		public static Quaternion	RandomQuaternion()
		{
			float x = 180.0F * Random.Range(-1.0F, 1.0F);
			float y = 180.0F * Random.Range(-1.0F, 1.0F);
			float z = 180.0F * Random.Range(-1.0F, 1.0F);
			
			return Quaternion.Euler(x, y, z);
		}
	}
}