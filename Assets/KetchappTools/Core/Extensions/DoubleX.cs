using System.Globalization;

namespace KetchappTools.Core.Extensions
{
	public static class DoubleX
	{
		#region // ==============================[Constants]============================== //

			/// <summary>
			/// Cos(1)
			/// </summary>
			public const double Cos_One = 5.403023058681397174009366074429766037e-01;

			/// <summary>
			/// Pi^(1/3)
			/// </summary>
			public const double Cbrt_Pi = 1.464591887561523263020142527263790391e+00;

			/// <summary>
			/// Degree To Radian converter.
			/// </summary>
			public const double DegreeToRadian = 0.017453292519943295474371680597869271e+00;

			/// <summary>
			/// Degree To Gradian converter.
			/// </summary>
			public const double DegreeToGradian = 1.11111111111111111111111111111111111e+00;

			/// <summary>
			/// Degree To Turn converter.
			/// </summary>
			public const double DegreeToTurn = 0.00277777777777777777777777777777778e+00;

			/// <summary>
			/// Radian To Degree converter.
			/// </summary>
			public const double GradianToDegree = 0.90;

			/// <summary>
			/// Radian To Radian converter.
			/// </summary>
			public const double GradianToRadian = 0.015707963267948966192313216916e+00;

			/// <summary>
			/// Radian To Turn converter.
			/// </summary>
			public const double GradianToTurn = 0.0025;

			/// <summary>
			/// Pi
			/// </summary>
			public const double Pi = 3.141592653589793238462643383279502884e+00;

			/// <summary>
			/// Pi / 4
			/// </summary>
			public const double Pi_Div_Four = 0.78539816339744830961566084582e+00;

			/// <summary>
			/// Pi / 2.0f = 0.5f * Pi.
			/// </summary>
			public const double Pi_Div_Two = 1.570796326794896619231321691639751442e+00;

			/// <summary>
			/// Pi^2 = Pi * Pi
			/// </summary>
			public const double Pi_Sqr = 9.869604401089358618834490999876151135e+00;

			/// <summary>
			/// e
			/// </summary>
			public const double E = 2.718281828459045235360287471352662497e+00;

			/// <summary>
			/// e^(Pi)
			/// </summary>
			public const double E_Pow_Pi = 2.314069263277926900572908636794854738e+01;

			/// <summary>
			/// e^(-0.5)
			/// </summary>
			public const double E_Pow_Minus_Half = 6.065306597126334236037995349911804534e-01;

			/// <summary>
			/// e^(-1) = 1 / e
			/// </summary>
			public const double E_Pow_Minus_One = 3.678794411714423215955237701614608674e-01;
			
			/// <summary>
			/// 4Pi / 3 
			/// </summary>
			public const double Four_Pi_Div_Three = 4.188790204786390984616857844373;

			/// <summary>
			/// 1/2 = 0.5f.
			/// </summary>
			public const double Half = 5e-01;

			/// <summary>
			/// 1 / Pi
			/// </summary>
			public const double One_Div_Pi = 0.318309886183790671537767526745;

			/// <summary>
			/// 1 / sqrt(Pi)
			/// </summary>
			public const double One_Div_Root_Pi = 5.641895835477562869480794515607725858e-01;

			/// <summary>
			/// 1 / sqrt(Tau) = 1 / sqrt(2Pi)
			/// </summary>
			public const double One_Div_Root_Tau = 3.989422804014326779399460599343818684e-01;

			/// <summary>
			/// 1 / sqrt(2)
			/// </summary>
			public const double One_Div_Root_Two = 7.071067811865475244008443621048490392e-01;

			/// <summary>
			/// Radian To Degree converter.
			/// </summary>
			public const double RadianToDegree = 57.29577951308232286464772187173366546e+00;

			/// <summary>
			/// Radian To Gradian converter.
			/// </summary>
			public const double RadianToGradian= 63.661977236758134307553505349006e+00;

			/// <summary>
			/// Radian To Turn converter.
			/// </summary>
			public const double RadianToTurn = 0.159154943091895335768883763373e+00;

			/// <summary>
			/// Sqrt(e).
			/// </summary>
			public const double Root_E = 1.648721270700128146848650787814163571e+00;

			/// <summary>
			/// Sqrt(Pi).
			/// </summary>
			public const double Root_Pi = 1.772453850905516027298167483341145182e+00;

			/// <summary>
			/// sqrt(Pi/2)
			/// </summary>
			public const double Root_Pi_Div_Two = 1.253314137315500251207882642405522626e+00;

			/// <summary>
			/// Sqrt(Tau) = Sqrt(2Pi).
			/// </summary>
			public const double Root_Tau = 2.5066282746310002e+00;

			/// <summary>
			/// Sqrt(3).
			/// </summary>
			public const double Root_Three = 1.732050807568877293527446341506e+00;

			/// <summary>
			/// Sqrt(2).
			/// </summary>
			public const double Root_Two = 1.414213562373095048801688724209698078e+00;

			/// <summary>
			/// Sin(1).
			/// </summary>
			public const double Sin_One = 0.0;
	
			/// <summary>
			/// Tau = 2Pi.
			/// </summary>
			public const double Tau = 6.283185307179586476925286766559e+00;

			/// <summary>
			/// 1/3.
			/// </summary>
			public const double Third = 3.333333333333333333333333333333333333e-01;
			
			/// <summary>
			/// 3Pi/4
			/// </summary>
			public const double Three_Pi_Div_Four = 2.356194490192344928846982537459627163e+00;

			/// <summary>
			/// 3/4
			/// </summary>
			public const double Three_Quaters = 7.500000000000000000000000000000000000e-01;

			/// <summary>
			/// Turns To Degrees converter.
			/// </summary>
			public const double TurnToDegree = 360.0;

			/// <summary>
			/// Turns To Gradian converter.
			/// </summary>
			public const double TurnToGradian = 400.0;

			/// <summary>
			/// Turns To Radian converter.
			/// </summary>
			public const double TurnToRadian = Tau;

			/// <summary>
			/// 2Pi/3.
			/// </summary>
			public const double Two_Pi_Div_Three = 2.094395102393195492308428922186335256e+00;

	#endregion

		#region // ==============================[Methods]============================== //

			/// <summary>
			///  Checks if two floating point numbers are nearly equal.
			/// </summary>
			public static bool IsNearlyEqual (this double p_this, double p_rhs, double p_tolerance = double.Epsilon)
			{
				return MathX.IsNearlyZero(p_this - p_rhs, p_tolerance);
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsNearlyZero (this double p_this, double p_tolerance = double.Epsilon)
			{
				return MathX.IsNearlyZero(p_this, p_tolerance);
			}

			/// <summary>
			///  Check if a floating point number is nearly zero.
			/// </summary>
			public static bool IsZero (this double p_this)
			{
				return MathX.IsZero(p_this);
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			public static double Remap (this double p_this, double p_inMin, double p_inMax, double p_outMin, double p_outMax)
			{
				return MathX.Remap(p_this, p_inMin, p_inMax, p_outMin, p_outMax);
			}

			/// <summary>
			/// Returns the value between [p_inMin, p_inMax] recalibrated in the new interval [p_outMin, p_outMax]
			/// </summary>
			/// <returns> The result is Clamped between [p_outMin, p_outMax] </returns>
			public static double RemapClamped (this double p_this, double p_inMin, double p_inMax, double p_outMin, double p_outMax)
			{
				return MathX.RemapClamped(p_this, p_inMin, p_inMax, p_outMin, p_outMax);
			}

			//TODO: Add Summary
			public static string ToStringFormated(this double _value)
			{
				return Format(_value.ToString("#.##", CultureInfo.InvariantCulture));
			}

			private static string Format(string _number)
			{
				// Divide integer and decimal
				string[] numberSplits = _number.Split(new char[] { '.' });

				// Calculate thousand coefficient
				int numberOfThousands = (numberSplits[0].Length - 1) / 3;

				// If inferior at 1000
				if (numberOfThousands <= 0)
					return ((numberSplits[0] == "") ? "0" : numberSplits[0]) + ((numberSplits.Length > 1) ? ("," + numberSplits[1]) : "");

				int decimalAt = numberSplits[0].Length - (numberOfThousands * 3);
				string decimalString = _number.Substring(decimalAt, 2);
				return _number.Substring(0, decimalAt) + ((decimalString != "00") ? ("." + decimalString) : "") + GetSuffix(numberOfThousands);
			}

			private static string GetSuffix(int numberOfThousands)
			{
				switch (numberOfThousands)
				{
					case 1:
						return "K";
					case 2:
						return "M";
					case 3:
						return "B";
					case 4:
						return "T";
					case 5:
						return "Q";
					default:
						return GetSuffix(numberOfThousands - 5) + "Q";
				}
		}

		#endregion
	}
}