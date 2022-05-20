using UnityEngine;

namespace KetchappTools.Core.Extensions
{
    public static class FloatX
    {
		#region // ==============================[Constants]============================== //

			/// <summary>
			/// Cos(1)
			/// </summary>
			public const float Cos_One = 5.403023058681397174009366074429766037e-01f;

			/// <summary>
			/// Pi^(1/3)
			/// </summary>
			public const float Cbrt_Pi = 1.464591887561523263020142527263790391e+00f;

			/// <summary>
			/// Degree To Radian converter.
			/// </summary>
			public const float DegreeToRadian = 0.017453292519943295474371680597869271e+00f;

			/// <summary>
			/// Degree To Gradian converter.
			/// </summary>
			public const float DegreeToGradian = 1.11111111111111111111111111111111111e+00f;

			/// <summary>
			/// Degree To Turn converter.
			/// </summary>
			public const float DegreeToTurn = 0.00277777777777777777777777777777778e+00f;

			/// <summary>
			/// Radian To Degree converter.
			/// </summary>
			public const float GradianToDegree = 0.90f;

			/// <summary>
			/// Radian To Radian converter.
			/// </summary>
			public const float GradianToRadian = 0.015707963267948966192313216916e+00f;

			/// <summary>
			/// Radian To Turn converter.
			/// </summary>
			public const float GradianToTurn = 0.0025f;

			/// <summary>
			/// Pi
			/// </summary>
			public const float Pi = 3.141592653589793238462643383279502884e+00f;

			/// <summary>
			/// Pi / 4
			/// </summary>
			public const float Pi_Div_Four = 0.78539816339744830961566084582e+00f;

			/// <summary>
			/// Pi / 2.0f = 0.5f * Pi.
			/// </summary>
			public const float Pi_Div_Two = 1.570796326794896619231321691639751442e+00f;

			/// <summary>
			/// Pi^2 = Pi * Pi
			/// </summary>
			public const float Pi_Sqr = 9.869604401089358618834490999876151135e+00f;

			/// <summary>
			/// e
			/// </summary>
			public const float E = 2.718281828459045235360287471352662497e+00f;

			/// <summary>
			/// e^(Pi)
			/// </summary>
			public const float E_Pow_Pi = 2.314069263277926900572908636794854738e+01f;

			/// <summary>
			/// e^(-0.5)
			/// </summary>
			public const float E_Pow_Minus_Half = 6.065306597126334236037995349911804534e-01f;

			/// <summary>
			/// e^(-1) = 1 / e
			/// </summary>
			public const float E_Pow_Minus_One = 3.678794411714423215955237701614608674e-01f;
			
			/// <summary>
			/// 4Pi / 3 
			/// </summary>
			public const float Four_Pi_Div_Three = 4.188790204786390984616857844373f;

			/// <summary>
			/// 1/2 = 0.5f.
			/// </summary>
			public const float Half = 5e-01f;

			/// <summary>
			/// 1 / Pi
			/// </summary>
			public const float One_Div_Pi = 0.318309886183790671537767526745f;

			/// <summary>
			/// 1 / sqrt(Pi)
			/// </summary>
			public const float One_Div_Root_Pi = 5.641895835477562869480794515607725858e-01f;

			/// <summary>
			/// 1 / sqrt(Tau) = 1 / sqrt(2Pi)
			/// </summary>
			public const float One_Div_Root_Tau = 3.989422804014326779399460599343818684e-01f;

			/// <summary>
			/// 1 / sqrt(2)
			/// </summary>
			public const float One_Div_Root_Two = 7.071067811865475244008443621048490392e-01f;

			/// <summary>
			/// Radian To Degree converter.
			/// </summary>
			public const float RadianToDegree = 57.29577951308232286464772187173366546e+00f;

			/// <summary>
			/// Radian To Gradian converter.
			/// </summary>
			public const float RadianToGradian= 63.661977236758134307553505349006e+00f;

			/// <summary>
			/// Radian To Turn converter.
			/// </summary>
			public const float RadianToTurn = 0.159154943091895335768883763373e+00f;

			/// <summary>
			/// Sqrt(e).
			/// </summary>
			public const float Root_E = 1.648721270700128146848650787814163571e+00f;

			/// <summary>
			/// Sqrt(Pi).
			/// </summary>
			public const float Root_Pi = 1.772453850905516027298167483341145182e+00f;

			/// <summary>
			/// sqrt(Pi/2)
			/// </summary>
			public const float Root_Pi_Div_Two = 1.253314137315500251207882642405522626e+00f;

			/// <summary>
			/// Sqrt(Tau) = Sqrt(2Pi).
			/// </summary>
			public const float Root_Tau = 2.5066282746310002e+00f;

			/// <summary>
			/// Sqrt(3).
			/// </summary>
			public const float Root_Three = 1.732050807568877293527446341506e+00f;

			/// <summary>
			/// Sqrt(2).
			/// </summary>
			public const float Root_Two = 1.414213562373095048801688724209698078e+00f;

			/// <summary>
			/// Sin(1).
			/// </summary>
			public const float Sin_One = 0.0f;
	
			/// <summary>
			/// Tau = 2Pi.
			/// </summary>
			public const float Tau = 6.283185307179586476925286766559e+00f;

			/// <summary>
			/// 1/3.
			/// </summary>
			public const float Third = 3.333333333333333333333333333333333333e-01f;
			
			/// <summary>
			/// 3Pi/4
			/// </summary>
			public const float Three_Pi_Div_Four = 2.356194490192344928846982537459627163e+00f;

			/// <summary>
			/// 3/4
			/// </summary>
			public const float Three_Quaters = 7.500000000000000000000000000000000000e-01f;

			/// <summary>
			/// Turns To Degrees converter.
			/// </summary>
			public const float TurnToDegree = 360.0f;

			/// <summary>
			/// Turns To Gradian converter.
			/// </summary>
			public const float TurnToGradian = 400.0f;

			/// <summary>
			/// Turns To Radian converter.
			/// </summary>
			public const float TurnToRadian = Tau;

			/// <summary>
			/// 2Pi/3.
			/// </summary>
			public const float Two_Pi_Div_Three = 2.094395102393195492308428922186335256e+00f;

		#endregion

		#region // ==============================[Methods]============================== //

			/// <summary>
		///  Checks if two floating point numbers are nearly equal.
		/// </summary>
			public static bool IsNearlyEqual (this float p_this, float p_rhs, float p_tolerance = float.Epsilon)
			{
				return MathX.IsNearlyZero(p_this - p_rhs, p_tolerance);
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsNearlyZero (this float p_this, float p_tolerance = float.Epsilon)
			{
				return MathX.IsNearlyZero(p_this, p_tolerance);
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsZero (this float p_this)
			{
				return MathX.IsZero(p_this);
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			public static float Remap (this float p_this, float p_inMin, float p_inMax, float p_outMin, float p_outMax)
			{
				return MathX.Remap(p_this, p_inMin, p_inMax, p_outMin, p_outMax);
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			/// <returns> The result is Clamped between [p_outMin, p_outMax] </returns>
			public static float RemapClamped (this float p_this, float p_inMin, float p_inMax, float p_outMin, float p_outMax)
			{
				return MathX.RemapClamped(p_this, p_inMin, p_inMax, p_outMin, p_outMax);
			}

			//TODO: Add Summary
			public static string ToStringFormated(this float _value)
			{
				return DoubleX.ToStringFormated(_value);
			}

		#endregion
	}
}