namespace KetchappTools.Core.Extensions
{
	public static class IntX
	{
		#region // ==============================[Methods]============================== //

			/// <summary>
			///  Checks if two an interger number are nearly equal.
			/// </summary>
			public static bool IsNearlyEqual (this int p_this, int p_rhs, int p_tolerance = 0)
			{
				return MathX.IsNearlyEqual(p_this, p_rhs, p_tolerance);
			}

			/// <summary>
			///  Check if an interger point number is nearly zero.
			/// </summary>
			public static bool IsNearlyZero (this int p_this, int p_tolerance = 0)
			{
				return MathX.IsNearlyZero(p_this, p_tolerance);
			}

			/// <summary>
			///  Check if an interger number is equlal zero.
			/// </summary>
			public static bool IsZero (this int p_this)
			{
				return MathX.IsZero(p_this);
			}

			//TODO: Add Summaries.

			public static int Length(this int _value)
			{
				if (_value == 0)
					return 1;
				else if (_value < 0)
					return -(int)MathX.Floor(MathX.Log10(-_value)) + 1;
				else
					return (int)MathX.Floor(MathX.Log10(_value)) + 1;
			}

			public static string NormalizeToString(this int value, int nbDigits)
			{
				int cpy = value;
				int max = (int)MathX.Pow(10, nbDigits - 1);
				string str = "";

				while (cpy < max)
				{
					cpy *= 10;
					str += "0";
				}

				str += value;

				return str;
			}

			public static string ToStringFormated(this int _value)
			{
				return DoubleX.ToStringFormated(_value);
			}

		#endregion
	}
}
