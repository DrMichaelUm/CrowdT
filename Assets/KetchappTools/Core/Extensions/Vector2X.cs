using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class Vector2X
	{
		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector2 Clamp (Vector2 p_vector, Vector2 p_min, Vector2 p_max)
		{
			return new Vector2
			(
				MathX.Clamp(p_vector.x, p_min.x, p_max.x),
				MathX.Clamp(p_vector.y, p_min.y, p_max.y)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector2 Clamp (Vector2 p_vector, float p_min, Vector2 p_max)
		{
			return new Vector2
			(
				MathX.Clamp(p_vector.x, p_min, p_max.x),
				MathX.Clamp(p_vector.y, p_min, p_max.y)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector2 Clamp (Vector2 p_vector, Vector2 p_min, float p_max)
		{
			return new Vector2
			(
				MathX.Clamp(p_vector.x, p_min.x, p_max),
				MathX.Clamp(p_vector.y, p_min.y, p_max)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector2 Clamp (Vector2 p_vector, float p_min, float p_max)
		{
			return new Vector2
			(
				MathX.Clamp(p_vector.x, p_min, p_max),
				MathX.Clamp(p_vector.y, p_min, p_max)
			);
		}

		/// <summary>
		/// Compute the cross product of two vectors.
		/// </summary>
		public static float Cross (Vector2 p_lhs, Vector2 p_rhs)
		{
			return p_lhs.x * p_lhs.y - p_lhs.y * p_lhs.x;
		}

		/// <summary>
		///	Compute the distance squared between two Vectors.
		/// </summary>
		public static float DistanceSqr (Vector2 p_lhs, Vector2 p_rhs)
		{
			return Vector2.Dot(p_lhs, p_rhs);
		}

		/// <summary>
		/// Extract and return parts from a vector that are pointing in the same direction as '_direction';
		/// </summary>
		public static Vector2 ExtractDotVector(Vector2 p_vector, Vector2 p_direction)
		{
			//Normalize vector if necessary;
			if (p_direction.sqrMagnitude != 1)
				p_direction.Normalize();

			float amount = Vector2.Dot(p_vector, p_direction);
			return p_direction * amount;
		}

		/// <summary>
		///	Create the vector from a Vector2 'A' to an another Vector2 'B'.
		/// </summary>
		public static Vector2 FromTo (Vector2 p_from, Vector2 p_to)
		{
			return p_to - p_from;
		}

		/// <summary>
		///	Compute a vector with the absolute of these components.
		/// </summary>
		public static Vector2 GetAbs (this Vector2 p_this)
		{
			return new Vector2(MathX.Abs(p_this.x), MathX.Abs(p_this.y));
		}

		/// <summary>
		///	Compute the maximum value between the absolute of these components.
		/// </summary>
		public static float GetAbsMax (this Vector2 p_this)
		{
			return MathX.Max(MathX.Abs(p_this.x), MathX.Abs(p_this.y));
		}

		/// <summary>
		///	Compute the minimum value between the absolute of these components.
		/// </summary>
		public static float GetAbsMin (this Vector2 p_this)
		{
			return MathX.Min(MathX.Abs(p_this.x), MathX.Abs(p_this.y));
		}

		/// <summary>
		///	Get the vector given with its magnitude clamped between minimum and maximum magnitude given.
		/// </summary>
		public static Vector2 GetClampedMagnitude (this Vector2 p_this, float p_minMagnitude, float p_maxMagnitude)
		{
			float thisMagnitude = p_this.magnitude;
			if (MathX.IsNearlyZero(thisMagnitude))
				return Vector2.zero;

			return MathX.Clamp(thisMagnitude, p_minMagnitude, p_maxMagnitude) * p_this / thisMagnitude;
		}

		/// <summary>
		///	Get the vector given with its magnitude clamped to maximum magnitude given.
		/// </summary>
		public static Vector2 GetClampedToMaxMagnitude (this Vector2 p_this, float p_maxMagnitude)
		{
			p_maxMagnitude = MathX.Max(p_maxMagnitude, 0.0f);
			float magnitudeSquared = p_this.sqrMagnitude;

			return magnitudeSquared > MathX.Square(p_maxMagnitude) ? p_this * (MathX.InvSqrt(magnitudeSquared) * p_maxMagnitude) : p_this;
		}

		/// <summary>
		///	Get the vector given with its magnitude clamped to minimum magnitude given.
		/// </summary>
		public static Vector2 GetClampedToMinMagnitude (this Vector2 p_this, float p_minMagnitude)
		{
			p_minMagnitude = MathX.Max(p_minMagnitude, 0.0f);
			float magnitudeSquared = p_this.sqrMagnitude;

			return magnitudeSquared < MathX.Square(p_minMagnitude) ? p_this * (MathX.InvSqrt(magnitudeSquared) * p_minMagnitude) : p_this;
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,J,X).
		/// </summary>
		public static Vector3 GetIJX (this Vector2 p_this, float p_i = 0.0f, float p_j = 0.0f)
		{
			return new Vector3(p_i, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,J,Y).
		/// </summary>
		public static Vector3 GetIJY (this Vector2 p_this, float p_i = 0.0f, float p_j = 0.0f)
		{
			return new Vector3(p_i, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(I,X).
		/// </summary>
		public static Vector2 GetIX (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector2(p_i, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,K).
		/// </summary>
		public static Vector3 GetIXK (this Vector2 p_this, float p_i = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,X).
		/// </summary>
		public static Vector3 GetIXX (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,Y).
		/// </summary>
		public static Vector3 GetIXY (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(I,Y).
		/// </summary>
		public static Vector2 GetIY (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector2(p_i, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,K).
		/// </summary>
		public static Vector3 GetIYK (this Vector2 p_this, float p_i = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,X).
		/// </summary>
		public static Vector3 GetIYX (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,Y).
		/// </summary>
		public static Vector3 GetIYY (this Vector2 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_this.y);
		}

		/// <summary>
		///	Return the highest component of this vector.
		/// </summary>
		public static float GetMax(this Vector2 p_this)
		{
			return MathX.Max(p_this.x, p_this.y);
		}

		/// <summary>
		///	Return the lowest component of this vector.
		/// </summary>
		public static float GetMin(this Vector2 p_this)
		{
			return MathX.Max(p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,J).
		/// </summary>
		public static Vector2 GetXJ (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector2(p_this.x, p_j);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,K).
		/// </summary>
		public static Vector3 GetXJK (this Vector2 p_this, float p_j = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,K).
		/// </summary>
		public static Vector3 GetXJX (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,Y).
		/// </summary>
		public static Vector3 GetXJY (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,X).
		/// </summary>
		public static Vector2 GetXX (this Vector2 p_this)
		{
			return new Vector2(p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,K).
		/// </summary>
		public static Vector3 GetXXK (this Vector2 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,X).
		/// </summary>
		public static Vector3 GetXXX (this Vector2 p_this)
		{
			return new Vector3(p_this.x, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,Y).
		/// </summary>
		public static Vector3 GetXXY (this Vector2 p_this)
		{
			return new Vector3(p_this.x, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,K).
		/// </summary>
		public static Vector3 GetXYK (this Vector2 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,X).
		/// </summary>
		public static Vector3 GetXYX (this Vector2 p_this)
		{
			return new Vector3(p_this.x, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,Y).
		/// </summary>
		public static Vector3 GetXYY (this Vector2 p_this)
		{
			return new Vector3(p_this.x, p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,J).
		/// </summary>
		public static Vector2 GetYJ (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector2(p_this.y, p_j);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,K).
		/// </summary>
		public static Vector3 GetYJK (this Vector2 p_this, float p_j = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,X).
		/// </summary>
		public static Vector3 GetYJX (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,Y).
		/// </summary>
		public static Vector3 GetYJY (this Vector2 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,X).
		/// </summary>
		public static Vector2 GetYX (this Vector2 p_this)
		{
			return new Vector2(p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,K).
		/// </summary>
		public static Vector3 GetYXK (this Vector2 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,X).
		/// </summary>
		public static Vector3 GetYXX (this Vector2 p_this)
		{
			return new Vector3(p_this.y, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,Y).
		/// </summary>
		public static Vector3 GetYXY (this Vector2 p_this)
		{
			return new Vector3(p_this.y, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,Y).
		/// </summary>
		public static Vector2 GetYY (this Vector2 p_this)
		{
			return new Vector2(p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,K).
		/// </summary>
		public static Vector3 GetYYK (this Vector2 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,X).
		/// </summary>
		public static Vector3 GetYYX (this Vector2 p_this)
		{
			return new Vector3(p_this.y, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,Y).
		/// </summary>
		public static Vector3 GetYYY (this Vector2 p_this)
		{
			return new Vector3(p_this.y, p_this.y, p_this.y);
		}

		/// <summary>
		/// Checks if two Vectors are collinear
		/// </summary>
		public static bool IsCollinear(Vector2 p_lhs, Vector2 p_rhs)
		{
			return Cross(p_lhs, p_rhs).IsZero();
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx (Vector2 p_value, Vector2 p_minExclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue.x) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue.y);
		}
		
		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx (Vector2 p_value, float p_minExclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue.x) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx (Vector2 p_value, Vector2 p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx (Vector2 p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn (Vector2 p_value, Vector2 p_minExclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue.x) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn (Vector2 p_value, float p_minExclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue.x) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn(Vector2 p_value, Vector2 p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn (Vector2 p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn (Vector2 p_value, Vector2 p_minInclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue.x) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn (Vector2 p_value, float p_minInclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return MathX.IsInsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue.x) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn (Vector2 p_value, Vector2 p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn (Vector2 p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx (Vector2 p_value, Vector2 p_minInclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue.x) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx (Vector2 p_value, float p_minInclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue.x) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx (Vector2 p_value, Vector2 p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx (Vector2 p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		/// Check if two vectors are nearly equal within a specified tolerance. 
		/// </summary>
		public static bool IsNearlyEqual (Vector2 p_lhs, Vector2 p_rhs, float p_tolerance = float.Epsilon)
		{
			return IsNearlyEqual(p_lhs, p_rhs, Vector2.one * p_tolerance);
		}

		/// <summary>
		/// Check if two vectors are nearly equal within a specified tolerance. 
		/// </summary>
		public static bool IsNearlyEqual (Vector2 p_lhs, Vector2 p_rhs, Vector2 p_tolerance)
		{
			return	MathX.IsNearlyEqual(p_lhs.x, p_rhs.x, p_tolerance.x) &&
					MathX.IsNearlyEqual(p_lhs.y, p_rhs.y, p_tolerance.y);
		}

		/// <summary>
		/// Checks whether vector is near to zero within a specified tolerance.
		/// </summary>
		public static bool IsNearlyZero (this Vector2 p_this, float p_tolerance = float.Epsilon)
		{
			return IsNearlyZero(p_this, Vector2.one * p_tolerance);
		}

		/// <summary>
		/// Checks whether vector is near to zero within a specified tolerance.
		/// </summary>
		public static bool IsNearlyZero (this Vector2 p_this, Vector2 p_tolerance)
		{
			return	MathX.IsNearlyZero(p_this.x, p_tolerance.x) &&
					MathX.IsNearlyZero(p_this.y, p_tolerance.y);
		}

		/// <summary>
		/// Checks if two vectors are perpendicular / orthogonal.
		/// </summary>
		/// <returns></returns>
		public static bool IsOrthogonal(Vector2 p_lhs, Vector2 p_rhs)
		{
			return MathX.IsZero(Vector2.Dot(p_lhs, p_rhs));
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx (Vector2 p_value, Vector2 p_minExclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue.x) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx (Vector2 p_value, float p_minExclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue.x) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx (Vector2 p_value, Vector2 p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx (Vector2 p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn (Vector2 p_value, Vector2 p_minExclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue.x) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn (Vector2 p_value, float p_minExclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue.x) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn (Vector2 p_value, Vector2 p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn (Vector2 p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn (Vector2 p_value, Vector2 p_minInclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue.x) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn (Vector2 p_value, float p_minInclusiveValue, Vector2 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue.x) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn (Vector2 p_value, Vector2 p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn (Vector2 p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx (Vector2 p_value, Vector2 p_minInclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue.x) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx (Vector2 p_value, float p_minInclusiveValue, Vector2 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue.x) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx (Vector2 p_value, Vector2 p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx (Vector2 p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Check if the Vector2 is equal to Vector2.zero.
		/// </summary>
		public static bool IsZero (this Vector2 p_this)
		{
			return p_this == Vector2.zero;
		}

		/// <summary>
		/// Compute a vector made from the largest components of two vectors
		/// </summary>
		public static Vector2 Max (Vector2 p_lhs, Vector2 p_rhs)
		{
			return new Vector2
			(
				MathX.Max(p_lhs.x, p_rhs.x),
				MathX.Max(p_lhs.y, p_rhs.y)
			);
		}

		/// <summary>
		/// Compute a vector made from the smallest components of two vectors
		/// </summary>
		public static Vector2 Min (Vector2 p_lhs, Vector2 p_rhs)
		{
			return new Vector2
			(
				MathX.Min(p_lhs.x, p_rhs.x),
				MathX.Min(p_lhs.y, p_rhs.y)
			);
		}

		/// <summary>
		/// Project a point onto a line defined by 'p_lineStartPosition' and 'p_lineDirection';
		/// </summary>
		public static Vector2 ProjectPointOntoLine (Vector2 p_lineStartPosition, Vector2 p_lineDirection, Vector2 p_point)
		{
			//Compute vector pointing from 'p_lineDirection' to 'p_point';
			Vector2 projectLine = p_point - p_lineStartPosition;

			float dotProduct = Vector2.Dot(projectLine, p_lineDirection);

			return p_lineStartPosition + p_lineDirection * dotProduct;
		}

		/// <summary>
		/// Remove all parts from a vector that are pointing in the same direction as '_direction';
		/// </summary>
		public static Vector2 RemoveDotVector (Vector2 p_vector, Vector2 p_direction)
		{
			return p_vector - ExtractDotVector(p_vector, p_direction);
		}

		/// <summary>
		/// Returns this Vector2 with the specified magnitude.
		/// </summary>
		public static Vector2 WithMagnitude (this Vector2 _vector, float _magnitude)
		{
			return _vector.normalized * _magnitude;
		}
	}
}