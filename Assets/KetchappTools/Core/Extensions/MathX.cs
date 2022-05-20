using System;
using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	/// <summary>
	/// TODO
	/// </summary>
	public static class MathX
	{
		#region // ==============================[Methods]============================== //

			/// <summary>
			///	Compute the absolute value of p_value.
			/// </summary>
			public static float Abs(float p_value)
			{
				return Math.Abs(p_value);
			}

			/// <summary>
			///	Compute the absolute value of p_value.
			/// </summary>
			public static double Abs(double p_value)
			{
				return Math.Abs(p_value);
			}

			/// <summary>
			///	Compute the absolute of p_value.
			/// </summary>
			public static int Abs(int p_value)
			{
				return Math.Abs(p_value);
			}

			/// <summary>
			/// Compute the arc cosine with the value given.
			/// </summary>
			/// <param name="p_value">Floating point value whose arc cosine is computed, in the interval [-1,1].</param>
			/// <returns> The result of the arc cosine of p_value, in the interval [0,pi] radian. If "p_value" the argument is out of the interval [-1,1], return Nan.</returns>
			public static float Acos(float p_value)
			{
				return (float)Math.Acos(p_value);
			}

			/// <summary>
			/// Compute the arc cosine with the value given.
			/// </summary>
			/// <param name="p_value">Floating point value whose arc cosine is computed, in the interval [-1,1].</param>
			/// <returns> The result of the arc cosine of p_value, in the interval [0,pi] radian. If "p_value" the argument is out of the interval [-1,1], return Nan.</returns>
			public static double Acos(double p_value)
			{
				return Math.Acos(p_value);
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Cosine with the value given.
			/// </summary>
			public static float AcosH(float p_value)
			{
				return Ln(p_value + Sqrt(Square(p_value) - 1.0f));
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Cosine with the value given.
			/// </summary>
			public static double AcosH(double p_value)
			{
				return Ln(p_value + Sqrt(Square(p_value) - 1.0f));
			}
			
			/// <summary>
			/// Compute the Inverse Hyperbolic cotangent with the value given.
			/// </summary>
			public static float AcotH(float p_value)
			{
				return 0.5f * Ln((1 + p_value) / (p_value - 1));
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic cotangent with the value given.
			/// </summary>
			public static double AcotH(double p_value)
			{
				return 0.5 * Ln((1 + p_value) / (p_value - 1));
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic cosecant with the value given.
			/// </summary>
			public static float AcscH(float p_value)
			{
				return Ln((1.0f + Sqrt(1.0f + Square(p_value))) / p_value);
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic cosecant with the value given.
			/// </summary>
			public static double AcscH(double p_value)
			{
				return Ln((1.0f + Sqrt(1.0f + Square(p_value))) / p_value);
		    }

			/// <summary>
			/// Compute the Inverse Hyperbolic secant with the value given.
			/// </summary>
			public static float AsecH(float p_value)
			{
				return Ln((1.0f + Sqrt(1.0f - Square(p_value))) / p_value);
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic secant with the value given.
			/// </summary>
			public static double AsecH(double p_value)
			{
				return Ln((1.0 + Sqrt(1.0 - Square(p_value))) / p_value);
			}

			/// <summary>
			/// Compute the arc sine with the value given.
			/// </summary>
			/// <param name="p_value">Floating point value whose arc sine is computed, in the interval [-1,1].</param>
			/// <returns> The result of the arc sine of p_value, in the interval [-pi/2, +pi/2] radian. If "p_value" the argument is out of the interval [-1,1], return Nan.</returns>
			public static float Asin(float p_value)
			{
				return (float)Math.Asin(p_value);
			}

			/// <summary>
			/// Compute the arc sine with the value given.
			/// </summary>
			/// <param name="p_value">Floating point value whose arc sine is computed, in the interval [-1,1].</param>
			/// <returns> The result of the arc sine of p_value, in the interval [-pi/2, +pi/2] radian. If "p_value" the argument is out of the interval [-1,1], return Nan.</returns>
			public static double Asin(double p_value)
			{
				return Math.Asin(p_value);
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Sine with the value given.
			/// </summary>
			public static float AsinH(float p_value)
			{
				return Ln(p_value + Sqrt(Square(p_value) + 1.0f));
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Sine with the value given.
			/// </summary>
			public static double AsinH(double p_value)
			{
				return Ln(p_value + Sqrt(Square(p_value) + 1.0f));
			}

			/// <summary>
			/// Compute the arc tangent with the value given.
			/// Notice that because of the sign ambiguity, the function cannot determine with certainty in which quadrant
			/// the angle falls only by its tangent value. See atan2 for an alternative that takes a fractional argument instead.
			/// </summary>
			/// <param name="p_value"> Floating point value whose arc tangent is computed.</param>
			/// <returns> The result of the arc tangent of p_value, in the interval -pi/2,+pi/2] radian.</returns>
			public static float Atan(float p_value)
			{
				return (float)Math.Atan(p_value);
			}

			/// <summary>
			/// Compute the arc tangent with the value given.
			/// Notice that because of the sign ambiguity, the function cannot determine with certainty in which quadrant
			/// the angle falls only by its tangent value. See atan2 for an alternative that takes a fractional argument instead.
			/// </summary>
			/// <param name="p_value"> Floating point value whose arc tangent is computed.</param>
			/// <returns> The result of the arc tangent of p_value, in the interval -pi/2,+pi/2] radian.</returns>
			public static double Atan(double p_value)
			{
				return Math.Atan(p_value);
		    }

			/// <summary>
			/// Compute the arc tangent with two parameters given.
			/// To compute the result, the function takes into account the sign of both arguments in order to determine the quadrant.
			/// </summary>
			/// <param name="p_y"> Floating point value representing the proportion of the y-coordinate.</param>
			/// <param name="p_x"> Floating point value representing the proportion of the x-coordinate.</param>
			/// <returns> The principal value of the arc tangent of p_y/p_x, expressed in radians.</returns>
			public static float Atan2(float p_y, float p_x)
				{
					return (float)Math.Atan2(p_y, p_x);
				}

			/// <summary>
			/// Compute the arc tangent with two parameters given.
			/// To compute the result, the function takes into account the sign of both arguments in order to determine the quadrant.
			/// </summary>
			/// <param name="p_y"> Floating point value representing the proportion of the y-coordinate.</param>
			/// <param name="p_x"> Floating point value representing the proportion of the x-coordinate.</param>
			/// <returns> The principal value of the arc tangent of p_y/p_x, expressed in radians.</returns>
			public static double Atan2(double p_y, double p_x)
			{
				return Math.Atan2(p_y, p_x);
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Tangent with the value given.
			/// </summary>
			public static float AtanH(float p_value)
			{
				return 0.5f * Ln((1.0f + p_value) / (1.0f - p_value));
			}

			/// <summary>
			/// Compute the Inverse Hyperbolic Tangent with the value given.
			/// </summary>
			public static double AtanH(double p_value)
			{
				return 0.5 * Ln((1.0 + p_value) / (1.0 - p_value));
			}

			/// <summary>
			/// Compute the cubic root of the value given
			/// </summary>
			public static float Cbrt(float p_value)
			{
				return NthRoot(p_value, 3.0f);
			}

			/// <summary>
			/// Compute the cubic root of the value given
			/// </summary>
			public static double Cbrt(double p_value)
			{
				return NthRoot(p_value, 3.0f);
			}

			/// <summary>
			/// Converts a value given to the nearest greater or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static float Ceil(float p_value)
			{
				return (float)Math.Ceiling(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest greater or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static double Ceil(double p_value)
			{
				return Math.Ceiling(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest greater or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static int CeilToInt(float p_value)
			{
				return (int)Ceil(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest greater or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static int CeilToInt(double p_value)
			{
				return (int)Ceil(p_value);
			}

			/// <summary>
			/// Clamps the value given between two value given min and max included.
			/// </summary>
			public static float Clamp(float p_value, float p_min = 0.0f, float p_max = 1.0f)
			{
				return Min(Max(p_value, p_min), p_max);
			}

			/// <summary>
			/// Clamps the value given between two value given min and max included.
			/// </summary>
			public static double Clamp(double p_value, double p_min = 0.0, double p_max = 1.0)
			{
				return Min(Max(p_value, p_min), p_max);
			}

			/// <summary>
			/// Returns the closest power of two value.
			/// </summary>
			public static int ClosestPowerOfTwo(int p_value)
			{
				return Mathf.ClosestPowerOfTwo(p_value);
			}

			/// <summary>
			///	Compute the cubic root of p_value.
			/// </summary>
			public static float Cube(float p_value)
			{
				return Square(p_value) * p_value;
			}

			/// <summary>
			/// Compute the cosine of an angle given in radians.
			/// </summary>
			public static float Cos(float p_value)
			{
				return (float)Math.Cos(p_value);
			}

			/// <summary>
			/// Compute the cosine of an angle given in radians.
			/// </summary>
			public static double Cos(double p_value)
			{
				return Math.Cos(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cosine of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static float CosH(float p_value)
			{
				return (float)Math.Cosh(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cosine of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static double CosH(double p_value)
			{
				return Math.Cosh(p_value);
			}

			/// <summary>
			/// Compute the cotangent of an angle given in radians. Cot(x) = 1 / tan(x).
			/// </summary>
			public static float Cot(float p_value)
			{
				return 1.0f / Tan(p_value);
			}

			/// <summary>
			/// Compute the cotangent of an angle given in radians. Cot(x) = 1 / tan(x).
			/// </summary>
			public static double Cot(double p_value)
			{
				return 1.0f / Tan(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cotangent function of the angle given.
			/// </summary>
			public static float CotH(float p_value)
			{
				return 1.0f / TanH(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cotangent function of the angle given.
			/// </summary>
			public static double CotH(double p_value)
			{
				return 1.0 / TanH(p_value);
			}

			/// <summary>
			/// Compute the cosecant of an angle given in radians. Csc(x) = 1 / sin(x) = hypotenuse / y.
			/// </summary>
			public static float Csc(float p_value)
			{
				return 1.0f / Sin(p_value);
			}

			/// <summary>
			/// Compute the cosecant of an angle given in radians. Csc(x) = 1 / sin(x) = hypotenuse / y.
			/// </summary>
			public static double Csc(double p_value)
			{
				return 1.0f / Sin(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cosecant function of the angle given.
			/// </summary>
			public static float CscH(float p_value)
			{
				return 1.0f / SinH(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic cosecant function of the angle given.
			/// </summary>
			public static double CscH(double p_value)
			{
				return 1.0 / SinH(p_value);
			}

			/// <summary>
			///	Compute the cubic root of p_value.
			/// </summary>
			public static double Cube(double p_value)
			{
				return Square(p_value) * p_value;
			}

			/// <summary>
			///	Compute the cubic root of p_value.
			/// </summary>
			public static int Cube(int p_value)
			{
				return Square(p_value) * p_value;
			}

			/// <summary>
			/// Convert a Cylindrical coordinate system to Spherical Coordinate System.
			/// </summary>
			/// <param name="p_cylRho"> The axial distance or radial distance ρ is the Euclidean distance from the z-axis to the point P. </param>
			/// <param name="p_cylPhi"> he azimuth φ is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.</param>
			/// <param name="p_cylZ"> The axial coordinate or height z is the signed distance from the chosen plane to the point P. </param>
			/// <param name="p_sphereRadius"> The radius or radial distance is the Euclidean distance from the origin O to P.</param>
			/// <param name="p_sphereTheta"> The inclination (or polar angle) is the angle between the zenith direction and the line segment OP. </param>
			/// <param name="p_spherePhi"> The azimuth(or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the line segment OP on the reference plane. </param>
			public static void CylindricalToSpherical(float p_cylRho, float p_cylPhi, float p_cylZ, out float p_sphereRadius, out float p_sphereTheta, out float p_spherePhi)
			{
				p_sphereRadius = Sqrt(Square(p_cylRho) + Square(p_cylZ));
				p_sphereTheta = Atan2(p_cylRho, p_cylZ);
				p_spherePhi = p_cylPhi;
			}

			/// <summary>
			///	Convert degrees to radians.
			/// </summary>
			public static float DegreeToRadian(float p_degree)
			{
				return p_degree * FloatX.DegreeToRadian;
			}
		
			/// <summary>
			///	Convert degrees to radians.
			/// </summary>
			public static double DegreeToRadian(double p_degree)
			{
				return p_degree * DoubleX.DegreeToRadian;
			}

			/// <summary>
			///	Convert degrees to gradians.
			/// </summary>
			public static float DegreeToGradian(float p_degree)
			{
				return p_degree * FloatX.DegreeToGradian;
			}

			/// <summary>
			///	Convert degrees to gradians.
			/// </summary>
			public static double DegreeToGradian(double p_degree)
			{
				return p_degree * DoubleX.DegreeToGradian;
			}

			/// <summary>
			///	Convert degrees to turns.
			/// </summary>
			public static float DegreeToTurn(float p_degree)
			{
				return p_degree * FloatX.DegreeToTurn;
			}

			/// <summary>
			///	Convert degrees to turns.
			/// </summary>
			public static double DegreeToTurn(double p_degree)
			{
				return p_degree * DoubleX.DegreeToTurn;
			}

			/// <summary>
			///	Compute the euclidean division between a dividend and a divisor given.
			/// </summary>
			public static int DivRem(int p_dividend, int p_divisor, out int p_remainder)
			{
				return Math.DivRem(p_dividend, p_divisor, out p_remainder);
			}

			/// <summary>
			/// Computes the exponential raised to the specified power
			/// </summary>
			/// <param name="p_value">Floating point value of the exponent </param>
			/// <returns></returns>
			public static float Exp(float p_exponent)
			{
				return (float)Math.Exp(p_exponent);
			}

			/// <summary>
			/// Computes the exponential raised to the specified power
			/// </summary>
			/// <param name="p_value">Floating point value of the exponent </param>
			/// <returns></returns>
			public static double Exp(double p_exponent)
			{
				return Math.Exp(p_exponent);
			}

			/// <summary>
			/// Converts a value given to the nearest less or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static float Floor(float p_value)
			{
				return (float)Math.Floor(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest less or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static double Floor(double p_value)
			{
				return Math.Floor(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest less or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static int FloorToInt(float p_value)
			{
				return (int)Floor(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest less or equal integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static int FloorToInt(double p_value)
			{
				return (int)Floor(p_value);
			}

			/// <summary>
			/// Returns the fractional part of the value given.
			/// </summary>
			/// <returns>The fractional part of p_value. It's a value in the range [0,1[.</returns>
			public static float Frac(float p_value)
			{
				return p_value - Floor(p_value);
			}

			/// <summary>
			/// Returns the fractional part of the value given.
			/// </summary>
			/// <returns>The fractional part of p_value. It's a value in the range [0,1[.</returns>
			public static double Frac(double p_value)
			{
				return p_value - Floor(p_value);
			}

			/// <summary>
			///	Compute the greatest common divisor.
			/// </summary>
			public static int GreatestCommonDivisor(int p_dividend, int p_divisor)
			{
				int a = Max(p_dividend, p_divisor);
				int b = Min(p_dividend, p_divisor);
				int e = DivRem(a, b, out int r);
				if (r == 0)
					return b;
				else
					return GreatestCommonDivisor(b, r);
			}

			/// <summary>
			/// Compute the percentage of p_value representative of the range [p_a, p_b]
			/// </summary>
			public static float InverseLerp(float p_a, float p_b, float p_value)
			{
				return (p_value - p_a) / (p_b - p_a);
			}

			/// <summary>
			/// Compute the percentage of p_value representative of the range [p_a, p_b]
			/// </summary>
			public static double InverseLerp(double p_a, double p_b, double p_value)
			{
				return (p_value - p_a) / (p_b - p_a);
			}

			/// <summary>
			/// Compute the percentage of p_value representative of the range [p_a, p_b]. The result is between [0;1]
			/// </summary>
			public static float InverseLerpClamped(float p_a, float p_b, float p_value)
			{
				return Mathf.Clamp((p_value - p_a) / (p_b - p_a), 0.0F, 1.0F);
			}

			/// <summary>
			/// Compute the percentage of p_value representative of the range [p_a, p_b]. The result is between [0;1]
			/// </summary>
			public static double InverseLerpClamped(double p_a, double p_b, double p_value)
			{
				return Clamp((p_value - p_a) / (p_b - p_a));
			}

			/// <summary>
			/// Compute the inverse of the value given.
			/// </summary>
			public static float Inv(float p_value)
			{
				return 1.0f / p_value;
			}

			/// <summary>
			/// Compute the inverse of the value given.
			/// </summary>
			public static double Inv(double p_value)
			{
				return 1.0 / p_value;
			}

			/// <summary>
			///	Compute the inverse square root of p_value.
			/// </summary>
			public static float InvSqrt(float p_value)
			{
				return 1.0f / Sqrt(p_value);
			}

			/// <summary>
			///	Compute the inverse square root of p_value.
			/// </summary>
			public static double InvSqrt(double p_value)
			{
				return 1.0 / Math.Sqrt(p_value);
			}

			/// <summary>
			///	Compute the inverse square root of p_value.
			/// </summary>
			public static float InvSqrt(int p_value)
			{
				return 1.0f / Sqrt(p_value);
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max]
			/// </summary>
			public static bool IsInsideIn(float p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max]
			/// </summary>
			public static bool IsInsideIn(double p_value, double p_minInclusiveValue, double p_maxInclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max]
			/// </summary>
			public static bool IsInsideIn(int p_value, int p_minInclusiveValue, int p_maxInclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max[
			/// </summary>
			public static bool IsInsideInEx(float p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value < p_maxExclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max[
			/// </summary>
			public static bool IsInsideInEx(double p_value, double p_minInclusiveValue, double p_maxExclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value < p_maxExclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range [p_min,p_max[
			/// </summary>
			public static bool IsInsideInEx(int p_value, int p_minInclusiveValue, int p_maxExclusiveValue)
			{
				return p_minInclusiveValue <= p_value && p_value < p_maxExclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max[
			/// </summary>
			public static bool IsInsideEx(float p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value < p_maxExclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max[
			/// </summary>
			public static bool IsInsideEx(double p_value, double p_minExclusiveValue, double p_maxExclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value < p_maxExclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max[
			/// </summary>
			public static bool IsInsideEx(int p_value, int p_minExclusiveValue, int p_maxExclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value < p_maxExclusiveValue;
			}
			
			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max]
			/// </summary>
			public static bool IsInsideExIn(float p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max]
			/// </summary>
			public static bool IsInsideExIn(double p_value, double p_minExclusiveValue, double p_maxInclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///	Checks if the value given is inside the range ]p_min,p_max]
			/// </summary>
			public static bool IsInsideExIn(int p_value, int p_minExclusiveValue, int p_maxInclusiveValue)
			{
				return p_minExclusiveValue < p_value && p_value <= p_maxInclusiveValue;
			}

			/// <summary>
			///  Checks if two floating point numbers are nearly equal.
			/// </summary>
			public static bool IsNearlyEqual(float p_lhs, float p_rhs, float p_tolerance = float.Epsilon)
			{
				return IsNearlyZero(p_lhs - p_rhs, p_tolerance);
			}

			/// <summary>
			///  Checks if two floating point numbers are nearly equal.
			/// </summary>
			public static bool IsNearlyEqual(double p_lhs, double p_rhs, double p_tolerance = double.Epsilon)
			{
				return (p_lhs - p_rhs).IsNearlyZero(p_tolerance);
			}

			/// <summary>
			///  Checks if two an interger number are nearly equal.
			/// </summary>
			public static bool IsNearlyEqual(int p_lhs, int p_rhs, int p_tolerance = 0)
			{
				return (p_lhs - p_rhs).IsNearlyZero(p_tolerance);
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsNearlyZero(float p_value, float p_tolerance = float.Epsilon)
			{
				return MathX.Abs(p_value) <= p_tolerance;
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsNearlyZero(double p_value, double p_tolerance = double.Epsilon)
			{
				return MathX.Abs(p_value) <= p_tolerance;
			}

			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max[
			/// </summary
			public static bool IsOutsideEx (float p_value, float p_minExclusiveValue, float p_maxExclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxExclusiveValue < p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max[
			/// </summary
			public static bool IsOutsideEx(double p_value, double p_minExclusiveValue, double p_maxExclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxExclusiveValue < p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max[
			/// </summary
			public static bool IsOutsideEx(int p_value, int p_minExclusiveValue, int p_maxExclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxExclusiveValue < p_value;
			}
			
			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max]
			/// </summary
			public static bool IsOutsideExIn(float p_value, float p_minExclusiveValue, float p_maxInclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxInclusiveValue <= p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max]
			/// </summary
			public static bool IsOutsideExIn(double p_value, double p_minExclusiveValue, double p_maxInclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxInclusiveValue <= p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range ]p_min,p_max]
			/// </summary
			public static bool IsOutsideExIn(int p_value, int p_minExclusiveValue, int p_maxInclusiveValue)
			{
				return p_value < p_minExclusiveValue || p_maxInclusiveValue <= p_value;
			}
	
			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max]
			/// </summary
			public static bool IsOutsideIn(float p_value, float p_minInclusiveValue, float p_maxInclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxInclusiveValue <= p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max]
			/// </summary
			public static bool IsOutsideIn(double p_value, double p_minInclusiveValue, double p_maxInclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxInclusiveValue <= p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max]
			/// </summary
			public static bool IsOutsideIn(int p_value, int p_minInclusiveValue, int p_maxInclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxInclusiveValue <= p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max[
			/// </summary
			public static bool IsOutsideInEx(float p_value, float p_minInclusiveValue, float p_maxExclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxExclusiveValue < p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max[
			/// </summary
			public static bool IsOutsideInEx(double p_value, double p_minInclusiveValue, double p_maxExclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxExclusiveValue < p_value;
			}

			/// <summary>
			///	Checks if the value given is outside the range [p_min, p_max[
			/// </summary
			public static bool IsOutsideInEx(int p_value, int p_minInclusiveValue, int p_maxExclusiveValue)
			{
				return p_value <= p_minInclusiveValue || p_maxExclusiveValue < p_value;
			}

			/// <summary>
			/// Checks whether a number is a power of two.
			/// </summary>
			public static bool IsPowerOfTwo(int p_value)
			{
				return (p_value & (p_value-1)) == 0;
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsZero(float p_value)
			{
				return p_value == 0.0f;
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsZero(double p_value)
			{
				return p_value == 0.0f;
			}

			/// <summary>
			///  Computes a linear interpolation between two value.
			/// </summary>
			public static float Lerp(float p_a, float p_b, float p_t)
			{
				return p_a + (p_b - p_a) * p_t;
			}

			/// <summary>
			///  Computes a linear interpolation between two value.
			/// </summary>
			public static double Lerp(double p_a, double p_b, double p_t)
			{
				return p_a + (p_b - p_a) * p_t;
			}

			/// <summary>
			///  Computes a linear interpolation between two value.
			/// </summary>
			/// <returns> The result is clamped between [p_a, p_b] </returns>
			public static double LerpClamped(float p_a, float p_b, float p_t)
			{
				return Lerp(p_a, p_b, Clamp(p_t));
			}

			/// <summary>
			///  Computes a linear interpolation between two value.
			/// </summary>
			/// <returns> The result is clamped between [p_a, p_b] </returns>
			public static double LerpClamped(double p_a, double p_b, double p_t)
			{
				return Lerp(p_a, p_b, Clamp(p_t));
			}

			/// <summary>
			/// Get the natural logarithm of the value given.
			/// </summary>
			/// <returns> LogE(p_value) = Ln(p_value).</returns>
			public static float Ln(float p_value)
			{
				return LogE(p_value);
			}

			/// <summary>
			/// Get the natural logarithm of the value given.
			/// </summary>
			/// <returns> LogE(p_value) = Ln(p_value).</returns>
			public static double Ln(double p_value)
			{
				return LogE(p_value);
			}

			/// <summary>
			/// Compute the base 2 logarithm of the value given
			/// </summary>
			public static float Log2(float p_value)
			{
				return LogX(p_value, 2.0f);
			}

			/// <summary>
			/// Compute the base 2 logarithm of the value given
			/// </summary>
			public static double Log2(double p_value)
			{
				return LogX(p_value, 2.0);
			}

			/// <summary>
			/// Computes the base 10 logarithm of the value given.
			/// </summary>
			public static float Log10(float p_value)
			{
				return (float)Math.Log10(p_value);
			}

			/// <summary>
			/// Computes the base 10 logarithm of the value given.
			/// </summary>
			public static double Log10(double p_value)
			{
				return Math.Log10(p_value);
			}

			/// <summary>
			/// Get the natural logarithm of the value given.
			/// </summary>
			/// <returns> LogE(p_value) = Ln(p_value).</returns>
			public static float LogE(float p_value)
			{
				return (float)Math.Log(p_value);
			}

			/// <summary>
			/// Get the natural logarithm of the value given.
			/// </summary>
			/// <returns> LogE(p_value) = Ln(p_value).</returns>
			public static double LogE(double p_value)
			{
				return Math.Log(p_value);
			}

			/// <summary>
			/// Compute the logarithm with a base and value given.
			/// </summary>
			/// <param name="p_value">The floating point to compute the logarithm with.</param>
			/// <param name="p_base">The base of the logarithm.</param>
			public static float LogX(float p_value, float p_base)
			{
				return Ln(p_value) / Ln(p_base);
			}

			/// <summary>
			/// Compute the logarithm with a base and value given.
			/// </summary>
			/// <param name="p_value">The floating point to compute the logarithm with.</param>
			/// <param name="p_base">The base of the logarithm.</param>
			public static double LogX(double p_value, double p_base)
			{
				return Ln(p_value) / Ln(p_base);
			}

			/// <summary>
		    ///	Give the higher value between two value given.
		    /// </summary
			public static float Max(float p_a, float p_b)
			{
				return Math.Max(p_a, p_b);
			}

			/// <summary>
			///	Give the higher value between two value given.
			/// </summary
			public static double Max(double p_a, double p_b)
			{
				return Math.Max(p_a, p_b);
			}

			/// <summary>
			///	Give the higher value between two value given.
			/// </summary
			public static int Max(int p_a, int p_b)
			{
				return Math.Max(p_a, p_b);
			}

			/// <summary>
			///	Give the higher value between three value given.
			/// </summary
			public static float Max(float p_a, float p_b, float p_c)
			{
				return Max(Max(p_a, p_b), p_c);
			}

			/// <summary>
			///	Give the higher value between three value given.
			/// </summary
			public static double Max(double p_a, double p_b, double p_c)
			{
				return Max(Max(p_a, p_b), p_c);
			}

			/// <summary>
			///	Give the higher value between three value given.
			/// </summary
			public static int Max(int p_a, int p_b, int p_c)
			{
				return Max(Max(p_a, p_b), p_c);
			}

			/// <summary>
			///	Give the highest value in a value list.
			/// </summary
			public static float Max(params float[] p_values)
			{
				int lenght = p_values.Length;
				float result = 0.0f;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] > result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Give the highest value in a value list.
			/// </summary
			public static double Max(params double[] p_values)
			{
				int lenght = p_values.Length;
				double result = 0.0;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] > result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Give the highest value in a value list.
			/// </summary
			public static int Max(params int[] p_values)
			{
				int lenght = p_values.Length;
				int result = 0;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] > result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Give the lowest value between two value given.
			/// </summary
			public static float Min(float p_a, float p_b)
			{
				return Math.Min(p_a, p_b);
			}

			/// <summary>
			///	Give the lowest value between two value given.
			/// </summary
			public static double Min(double p_a, double p_b)
			{
				return Math.Min(p_a, p_b);
			}

			/// <summary>
		    ///	Give the lowest value between two value given.
		    /// </summary
			public static int Min(int p_a, int p_b)
			{
				return Math.Min(p_a, p_b);
			}

			/// <summary>
			///	Give the lowest value between three value given.
			/// </summary
			public static float Min(float p_a, float p_b, float p_c)
			{
				return Min(Min(p_a, p_b), p_c);
			}

			/// <summary>
			///	Give the lowest value between three value given.
			/// </summary
			public static double Min(double p_a, double p_b, double p_c)
			{
				return Min(Min(p_a, p_b), p_c);
			}

			/// <summary>
			///	Give the lowest value between three value given.
			/// </summary
			public static int Min(int p_a, int p_b, int p_c)
			{
				return Min(Min(p_a, p_b), p_c);
			}
			
			/// <summary>
			///	Give the lowest value in a value list.
			/// </summary
			public static float Min(params float[] p_values)
			{
				int lenght = p_values.Length;
				float result = 0.0f;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] < result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Give the lowest value in a value list.
			/// </summary
			public static double Min(params double[] p_values)
			{
				int lenght = p_values.Length;
				double result = 0.0;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] < result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Give the lowest value in a value list.
			/// </summary
			public static int Min(params int[] p_values)
			{
				int lenght = p_values.Length;
				int result = 0;
				if (result != 0)
				{ 
					result = p_values[0];
					for (int index = 1; index < lenght; index++)
						if (p_values[index] < result)
							result = p_values[index];
				}
				return result;
			}

			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static float Modf(float p_value, out float p_intPart)
			{
				Modf(p_value, out p_intPart, out float fractPart);
				return fractPart;
			}

			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static double Modf(double p_value, out double p_intPart)
			{
				Modf(p_value, out p_intPart, out double fractPart);
				return fractPart;
			}

			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static float Modf(float p_value, out int p_intPart)
			{
				Modf(p_value, out p_intPart, out float fractPart);
				return fractPart;
			}

			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static double Modf(double p_value, out int p_intPart)
			{
				Modf(p_value, out p_intPart, out double fractPart);
				return fractPart;
			}
			
			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static void Modf(float p_value, out float p_intPart, out float p_fracPart)
			{
				p_intPart = Floor(p_value);
				p_fracPart = p_value - p_intPart;
			}
			
			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static void Modf(double p_value, out double p_intPart, out double p_fracPart)
			{
				p_intPart = Math.Floor(p_value);
				p_fracPart = p_value - p_intPart;
			}
			
			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static void Modf(float p_value, out int p_intPart, out float p_fracPart)
			{
				p_intPart = FloorToInt(p_value);
				p_fracPart = p_value - p_intPart;
			}
			
			/// <summary>
			///	Breaks the given value into an integral and a fractional part.
			/// </summary
			public static void Modf(double p_value, out int p_intPart, out double p_fracPart)
			{
				p_intPart = (int)Math.Floor(p_value);
				p_fracPart = p_value - p_intPart;
			}

			/// <summary>
			/// Returns the next power of two that is equal to, or greater than, the argument.
			/// </summary>
			public static int NextPowerOfTwo(int p_value)
			{
				return Mathf.NextPowerOfTwo(p_value);
			}

			/// <summary>
			/// Compute the Nth root equivalent to Pow(value, 1/nthRoot)
			/// Ex : Sqrt(x) = NthRoot(x, 2) = Pow(value, 1/nthRoot)
			/// </summary>
			/// <param name="p_value"> Floating point value to compute with</param>
			/// <param name="p_nthRoot">Floating point value corresponding to the nth root</param>
			public static float NthRoot(float p_value, float p_nthRoot)
			{
				return Pow(p_value, 1.0f/p_nthRoot);
			}

			/// <summary>
			/// Compute the Nth root equivalent to Pow(value, 1/nthRoot)
			/// Ex : Sqrt(x) = NthRoot(x, 2) = Pow(value, 1/nthRoot)
			/// </summary>
			/// <param name="p_value"> Floating point value to compute with</param>
			/// <param name="p_nthRoot">Floating point value corresponding to the nth root</param>
			public static double NthRoot(double p_value, double p_nthRoot)
			{
				return Pow(p_value, 1.0/p_nthRoot);
			}

			/// <summary>
			/// Computes the base given raised to the power given.
			/// </summary>
			/// <param name="p_base"> Floating point base value.</param>
			/// <param name="p_exponent"> Floating point exponent value.</param>
			/// <returns>The result of p_base^(p_exponent)</returns>
			public static float Pow(float p_base, float p_exponent)
			{
				return (float)Math.Pow(p_base, p_exponent);
			}

			/// <summary>
			/// Computes the base given raised to the power given.
			/// </summary>
			/// <param name="p_base"> Floating point base value.</param>
			/// <param name="p_exponent"> Floating point exponent value.</param>
			/// <returns>The result of p_base^(p_exponent)</returns>
			public static double Pow(double p_base, double p_exponent)
			{
				return Math.Pow(p_base, p_exponent);
			}

			/// <summary>
			///	Convert Radians to Degrees.
			/// </summary>
			public static float RadianToDegree(float p_radian)
			{
				return p_radian * FloatX.RadianToDegree;
			}

			/// <summary>
			///	Convert Radians to Degrees.
			/// </summary>
			public static double RadianToDegree(double p_radian)
			{
				return p_radian * DoubleX.RadianToDegree;
			}

			/// <summary>
			///	Convert Radians to Gradian.
			/// </summary>
			public static float RadianToGradian(float p_radian)
			{
				return p_radian * FloatX.RadianToDegree;
			}

			/// <summary>
			///	Convert Radians to Gradian.
			/// </summary>
			public static double RadianToGradian(double p_radian)
			{
				return p_radian * DoubleX.RadianToDegree;
			}

			/// <summary>
			///	Convert Radians to Turns.
			/// </summary>
			public static float RadianToTurn(float p_radian)
			{
				return p_radian * FloatX.RadianToTurn;
			}

			/// <summary>
			///	Convert Radians to Turns.
			/// </summary>
			public static double RadianToTurn(double p_radian)
			{
				return p_radian * DoubleX.RadianToTurn;
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			public static float Remap(float p_value, float p_inMin, float p_inMax, float p_outMin, float p_outMax)
			{
				return Lerp(p_outMin, p_outMax, InverseLerp(p_inMin, p_inMax, p_value));
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			public static double Remap(double p_value, double p_inMin, double p_inMax, double p_outMin, double p_outMax)
			{
				return Lerp(p_outMin, p_outMax, InverseLerp(p_inMin, p_inMax, p_value));
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			/// <returns> The result is Clamped between [p_outMin, p_outMax] </returns>
			public static float RemapClamped(float p_value, float p_inMin, float p_inMax, float p_outMin, float p_outMax)
			{
				return Lerp(p_outMin, p_outMax, InverseLerpClamped(p_inMin, p_inMax, p_value));
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			/// <returns> The result is Clamped between [p_outMin, p_outMax] </returns>
			public static double RemapClamped(double p_value, double p_inMin, double p_inMax, double p_outMin, double p_outMax)
			{
				return Lerp(p_outMin, p_outMax, InverseLerpClamped(p_inMin, p_inMax, p_value));
			}

			/// <summary>
			/// Converts a value given to the nearest integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static float Round(float p_value)
			{
				return (float)Math.Round(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static double Round(double p_value)
			{
				return Math.Round(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static float RoundToInt(float p_value)
			{
				return (int)Round(p_value);
			}

			/// <summary>
			/// Converts a value given to the nearest integer.
			/// </summary>
			/// <param name="p_value"> The floating value to convert.</param>
			public static double RoundToInt(double p_value)
			{
				return (int)Round(p_value);
			}

			/// <summary>
			/// Compute the secant of an angle given in radians. Sec(x) = 1 / cos(x) = hypotenuse / x.
			/// </summary>
			public static float Sec(float p_value)
			{
				return 1.0f / Cos(p_value);
			}

			/// <summary>
			/// Compute the secant of an angle given in radians. Sec(x) = 1 / cos(x) = hypotenuse / x.
			/// </summary>
			public static double Sec(double p_value)
			{
				return 1.0f / Cos(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic secant function of the angle given.
			/// </summary>
			public static float SecH(float p_value)
			{
				return 1.0f / CosH(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic secant function of the angle given.
			/// </summary>
			public static double SecH(double p_value)
			{
				return 1.0 / CosH(p_value);
			}

            /// <summary>
            /// Solves a quadratic equation (ax² + bx + c = 0), returns the 2 results of the equation
            /// </summary>
            public static float[] SolveQuadraticEquation(float a, float b, float c)
            {
                return new float[]
                {
                    (-b - MathX.Sqrt(b * b - 4 * a * c)) / (2 * a),
                    (-b + MathX.Sqrt(b * b - 4 * a * c)) / (2 * a)
                };
            }

			/// <summary>
			/// Convert a Spherical coordinate system to Cylindrical Coordinate System.
			/// </summary>
			/// <param name="p_sphereRadius"> The radius or radial distance is the Euclidean distance from the origin O to P.</param>
			/// <param name="p_sphereTheta"> The inclination (or polar angle) is the angle between the zenith direction and the line segment OP. </param>
			/// <param name="p_spherePhi"> The azimuth(or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the line segment OP on the reference plane. </param>
			/// <param name="p_cylRho"> The axial distance or radial distance ρ is the Euclidean distance from the z-axis to the point P. </param>
			/// <param name="p_cylPhi"> he azimuth φ is the angle between the reference direction on the chosen plane and the line from the origin to the projection of P on the plane.</param>
			/// <param name="p_cylZ"> The axial coordinate or height z is the signed distance from the chosen plane to the point P. </param>
			public static void SphericalToCylindrical(float p_sphereRadius, float p_sphereTheta, float p_spherePhi, out float p_cylRho, out float p_cylPhi, out float p_cylZ)
			{
				p_cylRho = p_sphereRadius * Sin(p_sphereTheta);
				p_cylPhi = p_spherePhi;
				p_cylZ = p_sphereRadius * Cos(p_sphereTheta);
			}

			/// <summary>
			/// Compute the sign of the value given.
			/// </summary>
			/// <returns>
			/// Return -1 : p_value < 0
			/// Return 0 : p_value == 0
			/// Return 1 : p_value > 0
			/// </returns>
			public static int Sign(float p_value)
			{
				return Math.Sign(p_value);
			}

			/// <summary>
			/// Compute the sign of the value given.
			/// </summary>
			/// <returns>
			/// Return -1 : p_value < 0
			/// Return 0 : p_value == 0
			/// Return 1 : p_value > 0
			/// </returns>
			public static int Sign(double p_value)
			{
				return Math.Sign(p_value);
			}

			/// <summary>
			/// Compute the sign of the value given.
			/// </summary>
			/// <returns>
			/// Return -1 : p_value < 0
			/// Return 0 : p_value == 0
			/// Return 1 : p_value > 0
			/// </returns>
			public static int Sign(int p_value)
			{
				return Math.Sign(p_value);
			}

			/// <summary>
			/// Compute the sine of an angle given in radians.
			/// </summary>
			/// <param name="p_value">Floating point value representing an angle expressed in radians. </param>
			public static float Sin(float p_value)
			{
				return (float)Math.Sin(p_value);
			}

			/// <summary>
			/// Compute the sine of an angle given in radians.
			/// </summary>
			/// <param name="p_value">Floating point value representing an angle expressed in radians. </param>
			public static double Sin(double p_value)
			{
				return Math.Round(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic sine of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static float SinH(float p_value)
			{
				return (float)Math.Sinh(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic sine of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static double SinH(double p_value)
			{
				return Math.Sinh(p_value);
			}

			/// <summary>
			/// Compute the square root of the value given.
			/// </summary>
			/// <param name="p_value"> Floating point value whose square root is computed. If the argument is negative, a domain error occurs. </param>
			public static float Sqrt(float p_value)
			{
				return (float)Math.Sqrt(p_value);
			}

			/// <summary>
			/// Compute the square root of the value given.
			/// </summary>
			/// <param name="p_value"> Floating point value whose square root is computed. If the argument is negative, a domain error occurs. </param>
			public static double Sqrt(double p_value)
			{
				return Math.Sqrt(p_value);
			}

			/// <summary>
			///	Compute the square of the value
			/// </summary>
			public static float Square(float p_value)
			{
				return p_value * p_value;
			}

			/// <summary>
			///	Compute the square of the value
			/// </summary>
			public static double Square(double p_value)
			{
				return p_value * p_value;
			}

			/// <summary>
			///	Compute the square of the value
			/// </summary>
			public static int Square(int p_value)
			{
				return p_value * p_value;
			}

			/// <summary>
			/// Computes the tangent of an angle given in radians.
			/// </summary>
			/// <param name="p_value">Floating point value representing an angle expressed in radians. </param>
			public static float Tan(float p_value)
			{
				return (float)Math.Tan(p_value);
			}

			/// <summary>
			/// Computes the tangent of an angle given in radians.
			/// </summary>
			/// <param name="p_value">Floating point value representing an angle expressed in radians. </param>
			public static double Tan(double p_value)
			{
				return Math.Tan(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic tangent of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static float TanH(float p_value)
			{
				return (float)Math.Tanh(p_value);
			}

			/// <summary>
			/// Compute the hyperbolic tangent of an angle given in radians
			/// </summary>
			/// <param name="p_value"> Floating point value representing a hyperbolic angle.</param>
			public static double TanH(double p_value)
			{
				return Math.Tanh(p_value);
			}

			/// <summary>
			/// Converts a value to an integer with truncation towards zero.
			/// </summary>
			/// <param name="p_value"> The value to truncate.</param>
			/// <returns>The nearest integral value that is not larger in magnitude than p_value.</returns>
			public static float Trunc(float p_value)
			{
				return (float)Math.Truncate(p_value);
			}

			/// <summary>
			/// Converts a value to an integer with truncation towards zero.
			/// </summary>
			/// <param name="p_value"> The value to truncate.</param>
			/// <returns>The nearest integral value that is not larger in magnitude than p_value.</returns>
			public static double Trunc(double p_value)
			{
				return Math.Truncate(p_value);
			}

			/// <summary>
			///	Convert Turns to Degrees.
			/// </summary>
			public static float TurnToDegree(float p_radian)
			{
				return p_radian * FloatX.TurnToDegree;
			}

			/// <summary>
			///	Convert Turns to Degrees.
			/// </summary>
			public static double TurnToDegree(double p_radian)
			{
				return p_radian * DoubleX.TurnToDegree;
			}

			/// <summary>
			///	Convert Turns to Gradian.
			/// </summary>
			public static float TurnToGradian(float p_radian)
			{
				return p_radian * FloatX.TurnToGradian;
			}

			/// <summary>
			///	Convert Turns to Gradian.
			/// </summary>
			public static double TurnToGradian(double p_radian)
			{
				return p_radian * DoubleX.TurnToGradian;
			}

			/// <summary>
			///	Convert Turns to Radians.
			/// </summary>
			public static float TurnToRadian(float p_radian)
			{
				return p_radian * FloatX.TurnToRadian;
			}

			/// <summary>
			///	Convert Turns to Radians.
			/// </summary>
			public static double TurnToRadian(double p_radian)
			{
				return p_radian * DoubleX.TurnToRadian;
			}

		#endregion
	}
}