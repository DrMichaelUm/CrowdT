using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class Vector3X
	{
		/// <summary>
		/// Convert a Cartesian coordinate system to Cylindrical Coordinate System.
		/// </summary>
		/// <param name="p_position"> The cartesian location to convert. </param>
		/// <param name="p_rho"> The axial distance or radial distance ρ is the Euclidean distance from the z-axis to the point P. </param>
		/// <param name="p_phi"> he azimuth φ is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.</param>
		/// <param name="p_z"> The axial coordinate or height z is the signed distance from the chosen plane to the point P. </param>
		public static void CartesianToCylindrical(Vector3 p_position, out float p_rho, out float p_phi, out float p_z)
		{
			p_rho = p_position.GetXY().magnitude;
			p_phi = MathX.Atan2(p_position.y, p_position.x);
			p_z = p_position.z;
		}

		/// <summary>
		/// Convert a Cartesian coordinate system to Spherical Coordinate System.
		/// </summary>
		/// <param name="p_position"> The cartesian location to convert. </param>
		/// <param name="p_radius"> The radius or radial distance is the Euclidean distance from the origin O to P.</param>
		/// <param name="p_theta"> The inclination (or polar angle) is the angle between the zenith direction and the line segment OP. </param>
		/// <param name="p_phi"> The azimuth(or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the line segment OP on the reference plane. </param>
		public static void CartesianToSpherical(Vector3 p_position, out float p_radius, out float p_theta, out float p_phi)
		{
			p_radius = p_position.magnitude;
			p_theta = MathX.Acos(p_position.z / p_radius);
			p_phi = MathX.Atan2(p_position.y, p_position.x);
		}

		/// <summary>
		/// Convert a Cylindrical coordinate system to Cartesian Coordinate System.
		/// </summary>
		/// <param name="p_rho"> The axial distance or radial distance ρ is the Euclidean distance from the z-axis to the point P. </param>
		/// <param name="p_phi"> he azimuth φ is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.</param>
		/// <param name="p_z"> The axial coordinate or height z is the signed distance from the chosen plane to the point P. </param>
		public static Vector3 CylindricalToCartesian(float p_rho, float p_phi, float p_z)
		{
			return new Vector3(p_rho * MathX.Cos(p_phi), p_rho * MathX.Sin(p_phi), p_z);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector3 Clamp(Vector3 p_vector, Vector3 p_min, Vector3 p_max)
		{
			return new Vector3
			(
				MathX.Clamp(p_vector.x, p_min.x, p_max.x),
				MathX.Clamp(p_vector.y, p_min.y, p_max.y),
				MathX.Clamp(p_vector.z, p_min.z, p_max.z)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector3 Clamp(Vector3 p_vector, float p_min, Vector3 p_max)
		{
			return new Vector3
			(
				MathX.Clamp(p_vector.x, p_min, p_max.x),
				MathX.Clamp(p_vector.y, p_min, p_max.y),
				MathX.Clamp(p_vector.z, p_min, p_max.z)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector3 Clamp(Vector3 p_vector, Vector3 p_min, float p_max)
		{
			return new Vector3
			(
				MathX.Clamp(p_vector.x, p_min.x, p_max),
				MathX.Clamp(p_vector.y, p_min.y, p_max),
				MathX.Clamp(p_vector.z, p_min.z, p_max)
			);
		}

		/// <summary>
		/// Compute a vector clamped between p_min and p_max.
		/// </summary>
		public static Vector3 Clamp(Vector3 p_vector, float p_min, float p_max)
		{
			return new Vector3
			(
				MathX.Clamp(p_vector.x, p_min, p_max),
				MathX.Clamp(p_vector.y, p_min, p_max),
				MathX.Clamp(p_vector.z, p_min, p_max)
			);
		}

		/// <summary>
		/// Extract and return parts from a vector that are pointing in the same direction as '_direction';
		/// </summary>
		public static Vector3 ExtractDotVector(Vector3 p_vector, Vector3 p_direction)
		{
			//Normalize vector if necessary;
			if (p_direction.sqrMagnitude != 1)
				p_direction.Normalize();

			float amount = Vector3.Dot(p_vector, p_direction);
			return p_direction * amount;
		}

		/// <summary>
		///	Compute the distance squared between two Vectors.
		/// </summary>
		public static float DistanceSqr(Vector3 p_lhs, Vector3 p_rhs)
		{
			return Vector3.Dot(p_lhs, p_rhs);
		}

		/// <summary>
		///	Create the vector from a Vector3 'A' to an another Vector3 'B'.
		/// </summary>
		public static Vector3 FromTo(Vector3 p_from, Vector3 p_to)
		{
			return p_to - p_from;
		}

		/// <summary>
		///	Compute a vector with the absolute of these components.
		/// </summary>
		public static Vector3 GetAbs(this Vector3 p_this)
		{
			return new Vector3(MathX.Abs(p_this.x), MathX.Abs(p_this.y), MathX.Abs(p_this.z));
		}

		/// <summary>
		///	Compute the maximum value between the absolute of these components.
		/// </summary>
		public static float GetAbsMax(this Vector3 p_this)
		{
			return MathX.Max(MathX.Abs(p_this.x), MathX.Abs(p_this.y), MathX.Abs(p_this.z));
		}

		/// <summary>
		///	Compute the minimum value between the absolute of these components.
		/// </summary>
		public static float GetAbsMin(this Vector3 p_this)
		{
			return MathX.Min(MathX.Abs(p_this.x), MathX.Abs(p_this.y), MathX.Abs(p_this.z));
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,J,X).
		/// </summary>
		public static Vector3 GetIJX(this Vector3 p_this, float p_i = 0.0f, float p_j = 0.0f)
		{
			return new Vector3(p_i, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,J,Y).
		/// </summary>
		public static Vector3 GetIJY(this Vector3 p_this, float p_i = 0.0f, float p_j = 0.0f)
		{
			return new Vector3(p_i, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,J,Z).
		/// </summary>
		public static Vector3 GetIJZ(this Vector3 p_this, float p_i = 0.0f, float p_j = 0.0f)
		{
			return new Vector3(p_i, p_j, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(I,Z).
		/// </summary>
		public static Vector2 GetIX(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector2(p_i, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,K).
		/// </summary>
		public static Vector3 GetIXK(this Vector3 p_this, float p_i = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,X).
		/// </summary>
		public static Vector3 GetIXX(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,Y).
		/// </summary>
		public static Vector3 GetIXY(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,Z).
		/// </summary>
		public static Vector3 GetIXZ(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.x, p_this.z);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,X,Y).
		/// </summary>
		public static Vector2 GetIY(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector2(p_i, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,K).
		/// </summary>
		public static Vector3 GetIYK(this Vector3 p_this, float p_i = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,X).
		/// </summary>
		public static Vector3 GetIYX(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,Y).
		/// </summary>
		public static Vector3 GetIYY(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Y,X).
		/// </summary>
		public static Vector3 GetIYZ(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.y, p_this.z);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Z,K).
		/// </summary>
		public static Vector3 GetIZK(this Vector3 p_this, float p_i = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_i, p_this.z, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Z,X).
		/// </summary>
		public static Vector3 GetIZX(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.z, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Z,Y).
		/// </summary>
		public static Vector3 GetIZY(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.z, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(I,Z,Z).
		/// </summary>
		public static Vector3 GetIZZ(this Vector3 p_this, float p_i = 0.0f)
		{
			return new Vector3(p_i, p_this.z, p_this.z);
		}

		/// <summary>
		///	Return the highest component of this vector.
		/// </summary>
		public static float GetMax(this Vector3 p_this)
		{
			return MathX.Max(p_this.x, p_this.y, p_this.z);
		}

		/// <summary>
		///	Compute the absolute of the highest component of this vector.
		/// </summary>
		public static float GetMaxAbs(this Vector3 p_this)
		{
			return MathX.Abs(p_this.GetMax());
		}

		/// <summary>
		///	Return the lowest component of this vector.
		/// </summary>
		public static float GetMin(this Vector3 p_this)
		{
			return MathX.Max(p_this.x, p_this.y, p_this.z);
		}

		/// <summary>
		///	Compute the absolute of the lowest component of this vector.
		/// </summary>
		public static float GetMinAbs(this Vector3 p_this)
		{
			return MathX.Abs(p_this.GetMin());
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,J).
		/// </summary>
		public static Vector2 GetXJ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector2(p_this.x, p_j);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,K).
		/// </summary>
		public static Vector3 GetXJK(this Vector3 p_this, float p_j = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,X).
		/// </summary>
		public static Vector3 GetXJX(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,Y).
		/// </summary>
		public static Vector3 GetXJY(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,J,Z).
		/// </summary>
		public static Vector3 GetXJZ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.x, p_j, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,X).
		/// </summary>
		public static Vector2 GetXX(this Vector3 p_this)
		{
			return new Vector2(p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,K).
		/// </summary>
		public static Vector3 GetXXK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,X).
		/// </summary>
		public static Vector3 GetXXX(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,Y).
		/// </summary>
		public static Vector3 GetXXY(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,X,Z).
		/// </summary>
		public static Vector3 GetXXZ(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.x, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,Y).
		/// </summary>
		public static Vector2 GetXY(this Vector3 p_this)
		{
			return new Vector2(p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,K).
		/// </summary>
		public static Vector3 GetXYK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,X).
		/// </summary>
		public static Vector3 GetXYX(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Y,Y).
		/// </summary>
		public static Vector3 GetXYY(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Z,K).
		/// </summary>
		public static Vector3 GetXZK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.x, p_this.z, p_k);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(X,Z).
		/// </summary>
		public static Vector2 GetXZ(this Vector3 p_this)
		{
			return new Vector2(p_this.x, p_this.z);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Z,X).
		/// </summary>
		public static Vector3 GetXZX(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.z, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Z,Y).
		/// </summary>
		public static Vector3 GetXZY(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.z, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(X,Z,Z).
		/// </summary>
		public static Vector3 GetXZZ(this Vector3 p_this)
		{
			return new Vector3(p_this.x, p_this.z, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,J).
		/// </summary>
		public static Vector2 GetYJ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector2(p_this.y, p_j);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,K).
		/// </summary>
		public static Vector3 GetYJK(this Vector3 p_this, float p_j = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,X).
		/// </summary>
		public static Vector3 GetYJX(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,Y).
		/// </summary>
		public static Vector3 GetYJY(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,J,Z).
		/// </summary>
		public static Vector3 GetYJZ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.y, p_j, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,X).
		/// </summary>
		public static Vector2 GetYX(this Vector3 p_this)
		{
			return new Vector2(p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,K).
		/// </summary>
		public static Vector3 GetYXK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,X).
		/// </summary>
		public static Vector3 GetYXX(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,Y).
		/// </summary>
		public static Vector3 GetYXY(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,X,Z).
		/// </summary>
		public static Vector3 GetYXZ(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.x, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,Y).
		/// </summary>
		public static Vector2 GetYY(this Vector3 p_this)
		{
			return new Vector2(p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,K).
		/// </summary>
		public static Vector3 GetYYK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,Y).
		/// </summary>
		public static Vector3 GetYYY(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Y,Z).
		/// </summary>
		public static Vector3 GetYYZ(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.y, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Y,Z).
		/// </summary>
		public static Vector2 GetYZ(this Vector3 p_this)
		{
			return new Vector2(p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Z,K).
		/// </summary>
		public static Vector3 GetYZK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.y, p_this.z, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Z,X).
		/// </summary>
		public static Vector3 GetYZX(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.z, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Z,Y).
		/// </summary>
		public static Vector3 GetYZY(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.z, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Y,Z,Z).
		/// </summary>
		public static Vector3 GetYZZ(this Vector3 p_this)
		{
			return new Vector3(p_this.y, p_this.z, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Z,J).
		/// </summary>
		public static Vector2 GetZJ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector2(p_this.z, p_j);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,J,K).
		/// </summary>
		public static Vector3 GetZJK(this Vector3 p_this, float p_j = 0.0f, float p_k = 0.0f)
		{
			return new Vector3(p_this.z, p_j, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,J,X).
		/// </summary>
		public static Vector3 GetZJX(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.z, p_j, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,J,Y).
		/// </summary>
		public static Vector3 GetZJY(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.z, p_j, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,J,Z).
		/// </summary>
		public static Vector3 GetZJZ(this Vector3 p_this, float p_j = 0.0f)
		{
			return new Vector3(p_this.z, p_j, p_this.z);
		}

		/// <summary>
		///	Compute the Vector2 as Vector2(Z,X).
		/// </summary>
		public static Vector2 GetZX(this Vector3 p_this)
		{
			return new Vector2(p_this.z, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,X,K).
		/// </summary>
		public static Vector3 GetZXK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.z, p_this.x, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,X,X).
		/// </summary>
		public static Vector3 GetZXX(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.x, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,X,Y).
		/// </summary>
		public static Vector3 GetZXY(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.x, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,X,Z).
		/// </summary>
		public static Vector3 GetZXZ(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.x, p_this.z);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Y,K).
		/// </summary>
		public static Vector3 GetZYK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.z, p_this.y, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Y,X).
		/// </summary>
		public static Vector3 GetZYX(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.y, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Y,Y).
		/// </summary>
		public static Vector3 GetZYY(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.y, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Y,Z).
		/// </summary>
		public static Vector3 GetZYZ(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.y, p_this.z);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Z,K).
		/// </summary>
		public static Vector3 GetZZK(this Vector3 p_this, float p_k = 0.0f)
		{
			return new Vector3(p_this.z, p_this.z, p_k);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Z,X).
		/// </summary>
		public static Vector3 GetZZX(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.z, p_this.x);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Z,Y).
		/// </summary>
		public static Vector3 GetZZY(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.z, p_this.y);
		}

		/// <summary>
		///	Compute the Vector3 as Vector3(Z,Z,Z).
		/// </summary>
		public static Vector3 GetZZZ(this Vector3 p_this)
		{
			return new Vector3(p_this.z, p_this.z, p_this.z);
		}

		/// <summary>
		/// Checks if two Vectors are collinear
		/// </summary>
		public static bool IsCollinear(Vector3 p_lhs, Vector3 p_rhs)
		{
			return Vector3.Cross(p_lhs, p_rhs).IsZero();
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx(Vector3 p_value, Vector3 p_minExclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue.x) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue.y) &&
					MathX.IsInsideEx(p_value.z, p_minExclusiveValue.z, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx(Vector3 p_value, float p_minExclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue.x) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue.y)&&
					MathX.IsInsideEx(p_value.z, p_minExclusiveValue, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx(Vector3 p_value, Vector3 p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.z, p_minExclusiveValue.z, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max[
		/// </summary>
		public static bool IsInsideEx(Vector3 p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideEx(p_value.z, p_minExclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn(Vector3 p_value, Vector3 p_minExclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue.x) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue.y) &&
					MathX.IsInsideExIn(p_value.z, p_minExclusiveValue.z, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn(Vector3 p_value, float p_minExclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue.x) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue.y) &&
					MathX.IsInsideExIn(p_value.z, p_minExclusiveValue, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn(Vector3 p_value, Vector3 p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.z, p_minExclusiveValue.z, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range ]p_min,p_max]
		/// </summary>
		public static bool IsInsideExIn(Vector3 p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideExIn(p_value.z, p_minExclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn(Vector3 p_value, Vector3 p_minInclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue.x) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue.y) &&
					MathX.IsInsideIn(p_value.z, p_minInclusiveValue.z, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn(Vector3 p_value, float p_minInclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue.x) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue.y) &&
					MathX.IsInsideIn(p_value.z, p_minInclusiveValue, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn(Vector3 p_value, Vector3 p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.z, p_minInclusiveValue.z, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max]
		/// </summary>
		public static bool IsInsideIn(Vector3 p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsInsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue) &&
					MathX.IsInsideIn(p_value.z, p_minInclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx(Vector3 p_value, Vector3 p_minInclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue.x) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue.y) &&
					MathX.IsInsideInEx(p_value.z, p_minInclusiveValue.z, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx(Vector3 p_value, float p_minInclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue.x) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue.y) &&
					MathX.IsInsideInEx(p_value.z, p_minInclusiveValue, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx(Vector3 p_value, Vector3 p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.x, p_minInclusiveValue.y, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is inside the range [p_min,p_max[
		/// </summary>
		public static bool IsInsideInEx(Vector3 p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsInsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue) &&
					MathX.IsInsideInEx(p_value.z, p_minInclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		/// Check if two vectors are nearly equal within a specified tolerance. 
		/// </summary>
		public static bool IsNearlyEqual(Vector3 p_lhs, Vector3 p_rhs, float p_tolerance = float.Epsilon)
		{
			return IsNearlyEqual(p_lhs, p_rhs, Vector3.one * p_tolerance);
		}

		/// <summary>
		/// Check if two vectors are nearly equal within a specified tolerance. 
		/// </summary>
		public static bool IsNearlyEqual(Vector3 p_lhs, Vector3 p_rhs, Vector3 p_tolerance)
		{
			return	MathX.IsNearlyEqual(p_lhs.x, p_rhs.x, p_tolerance.x) &&
					MathX.IsNearlyEqual(p_lhs.y, p_rhs.y, p_tolerance.y) &&
					MathX.IsNearlyEqual(p_lhs.z, p_rhs.z, p_tolerance.z);
		}

		/// <summary>
		/// Checks whether vector is near to zero within a specified tolerance.
		/// </summary>
		public static bool IsNearlyZero(this Vector3 p_this, float p_tolerance = float.Epsilon)
		{
			return IsNearlyZero(p_this, Vector3.one * p_tolerance);
		}

		/// <summary>
		/// Checks whether vector is near to zero within a specified tolerance.
		/// </summary>
		public static bool IsNearlyZero(this Vector3 p_this, Vector3 p_tolerance)
		{
			return	MathX.IsNearlyZero(p_this.x, p_tolerance.x) &&
					MathX.IsNearlyZero(p_this.y, p_tolerance.y) &&
					MathX.IsNearlyZero(p_this.z, p_tolerance.z);
		}

		/// <summary>
		/// Checks if two vectors are perpendicular / orthogonal.
		/// </summary>
		/// <returns></returns>
		public static bool IsOrthogonal(Vector3 p_lhs, Vector3 p_rhs)
		{
			return MathX.IsZero(Vector3.Dot(p_lhs, p_rhs));
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx(Vector3 p_value, Vector3 p_minExclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue.x) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue.y) ||
					MathX.IsOutsideEx(p_value.z, p_minExclusiveValue.z, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx(Vector3 p_value, float p_minExclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue.x) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue.y) ||
					MathX.IsOutsideEx(p_value.z, p_minExclusiveValue, p_maxExclusiveValue.y);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx(Vector3 p_value, Vector3 p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue.x, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue.y, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.z, p_minExclusiveValue.z, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max[
		/// </summary
		public static bool IsOutsideEx(Vector3 p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideEx(p_value.x, p_minExclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.y, p_minExclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideEx(p_value.z, p_minExclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn(Vector3 p_value, Vector3 p_minExclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue.x) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue.y) ||
					MathX.IsOutsideExIn(p_value.z, p_minExclusiveValue.z, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn(Vector3 p_value, float p_minExclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue.x) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue.y) ||
					MathX.IsOutsideExIn(p_value.z, p_minExclusiveValue, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn(Vector3 p_value, Vector3 p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue.x, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue.y, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.z, p_minExclusiveValue.z, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range ]p_min, p_max]
		/// </summary
		public static bool IsOutsideExIn(Vector3 p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideExIn(p_value.x, p_minExclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.y, p_minExclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideExIn(p_value.z, p_minExclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn(Vector3 p_value, Vector3 p_minInclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue.x) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue.y) ||
					MathX.IsOutsideIn(p_value.z, p_minInclusiveValue.z, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn(Vector3 p_value, float p_minInclusiveValue, Vector3 p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue.x) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue.y) ||
					MathX.IsOutsideIn(p_value.z, p_minInclusiveValue, p_maxInclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn(Vector3 p_value, Vector3 p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue.x, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue.y, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.z, p_minInclusiveValue.z, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max]
		/// </summary
		public static bool IsOutsideIn(Vector3 p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
		{
			return	MathX.IsOutsideIn(p_value.x, p_minInclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.y, p_minInclusiveValue, p_maxInclusiveValue) ||
					MathX.IsOutsideIn(p_value.z, p_minInclusiveValue, p_maxInclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx(Vector3 p_value, Vector3 p_minInclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue.x) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue.y) ||
					MathX.IsOutsideInEx(p_value.z, p_minInclusiveValue.z, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx(Vector3 p_value, float p_minInclusiveValue, Vector3 p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue.x) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue.y) ||
					MathX.IsOutsideInEx(p_value.z, p_minInclusiveValue, p_maxExclusiveValue.z);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx(Vector3 p_value, Vector3 p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue.x, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue.y, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.z, p_minInclusiveValue.z, p_maxExclusiveValue);
		}

		/// <summary>
		///	Checks if the value given is outside the range [p_min, p_max[
		/// </summary
		public static bool IsOutsideInEx(Vector3 p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
		{
			return	MathX.IsOutsideInEx(p_value.x, p_minInclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.y, p_minInclusiveValue, p_maxExclusiveValue) ||
					MathX.IsOutsideInEx(p_value.z, p_minInclusiveValue, p_maxExclusiveValue);
		}

		/// <summary>
		///	Check if the Vector3 is equal to Vector3.zero.
		/// </summary>
		public static bool IsZero(this Vector3 p_this)
		{
			return p_this == Vector3.zero;
		}

		/// <summary>
		/// Compute a vector made from the largest components of two vectors
		/// </summary>
		public static Vector3 Max(Vector3 p_lhs, Vector3 p_rhs)
		{
			return new Vector3
			(
				MathX.Max(p_lhs.x, p_rhs.x),
				MathX.Max(p_lhs.y, p_rhs.y),
				MathX.Max(p_lhs.y, p_rhs.z)
			);
		}

		/// <summary>
		/// Compute a vector made from the smallest components of two vectors
		/// </summary>
		public static Vector3 Min(Vector3 p_lhs, Vector3 p_rhs)
		{
			return new Vector3
			(
				MathX.Min(p_lhs.x, p_rhs.x),
				MathX.Min(p_lhs.y, p_rhs.y),
				MathX.Min(p_lhs.y, p_rhs.z)
			);
		}

		/// <summary>
		/// Project a point onto a line defined by 'p_lineStartPosition' and 'p_lineDirection';
		/// </summary>
		public static Vector3 ProjectPointOntoLine(Vector3 p_lineStartPosition, Vector3 p_lineDirection, Vector3 p_point)
		{
			//Compute vector pointing from 'p_lineDirection' to 'p_point';
			Vector3 projectLine = p_point - p_lineStartPosition;

			float dotProduct = Vector3.Dot(projectLine, p_lineDirection);

			return p_lineStartPosition + p_lineDirection * dotProduct;
		}

		/// <summary>
		/// Remove all parts from a vector that are pointing in the same direction as '_direction';
		/// </summary>
		public static Vector3 RemoveDotVector(Vector3 p_vector, Vector3 p_direction)
		{
			return p_vector - ExtractDotVector(p_vector, p_direction);
		}

		/// <summary>
		/// Convert a Spherical coordinate system to Cartesian Coordinate System.
		/// </summary>
		/// <param name="p_radius"> The radius or radial distance is the Euclidean distance from the origin O to P.</param>
		/// <param name="p_theta"> The inclination (or polar angle) is the angle between the zenith direction and the line segment OP. </param>
		/// <param name="p_phi"> The azimuth(or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the line segment OP on the reference plane. </param>
		public static Vector3 SphericalToCartesian(float p_radius, float p_theta, float p_phi)
		{
			float sinTheta = MathX.Sin(p_theta);

			return new Vector3
			(
				p_radius * sinTheta * MathX.Cos(p_phi),
				p_radius * sinTheta * MathX.Sin(p_phi),
				p_radius * MathX.Cos(p_theta)
			); ;
		}

		/// <summary>
		/// Convert a Cartesian coordinate system to Cylindrical Coordinate System.
		/// </summary>
		/// <param name="p_this"> The cartesian location to convert. </param>
		/// <param name="p_rho"> The axial distance or radial distance ρ is the Euclidean distance from the z-axis to the point P. </param>
		/// <param name="p_phi"> he azimuth φ is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.</param>
		/// <param name="p_z"> The axial coordinate or height z is the signed distance from the chosen plane to the point P. </param>
		public static void ToCylindrical(this Vector3 p_this, out float p_rho, out float p_phi, out float p_z)
		{
			CartesianToCylindrical(p_this, out p_rho, out p_phi, out p_z);
		}

		/// <summary>
		/// Convert a Cartesian coordinate system to Spherical Coordinate System.
		/// </summary>
		/// <param name="p_this"> The cartesian location to convert. </param>
		/// <param name="p_radius"> The radius or radial distance is the Euclidean distance from the origin O to P.</param>
		/// <param name="p_theta"> The inclination (or polar angle) is the angle between the zenith direction and the line segment OP. </param>
		/// <param name="p_phi"> The azimuth(or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the line segment OP on the reference plane. </param>
		public static void ToSpherical(this Vector3 p_this, out float p_radius, out float p_theta, out float p_phi)
		{
			CartesianToSpherical(p_this, out p_radius, out p_theta, out p_phi);
		}

		/// <summary>
		/// Returns this vector with the specified magnitude.
		/// </summary>
		public static Vector3 WithMagnitude(this Vector3 p_this, float p_magnitude)
		{
			return p_this.normalized * p_magnitude;
		}
	}
}