using System;
using System.Text.RegularExpressions;

namespace KetchappTools.Core.Extensions
{
	public static class StringX
	{
		/// <summary>
		/// Returns a string using nomenclature of a Class.
		/// </summary>
		/// <returns></returns>
		public static string ClassNameClean(this string _value)
		{
			string	output = "";
			bool	nextUpper = true;
			bool	nextUpperBuffer;
			int		startByNumber = 0;

			// Remove special characters
			for (int i = 0; i < _value.Length; i++)
			{
				nextUpperBuffer = nextUpper;
				nextUpper = false;

				if (_value[i] == ' ' || _value[i] == '-' || _value[i] == '_')
				{
					nextUpper = true;
					continue;
				}

				if (startByNumber == i &&
					(_value[i] == '0' || _value[i] == '1' || _value[i] == '2' || _value[i] == '3' ||_value[i] == '4' ||
					 _value[i] == '5' || _value[i] == '6' || _value[i] == '7' || _value[i] == '8' || _value[i] == '9'))
					startByNumber++;

				if (nextUpperBuffer)
					output += Char.ToUpper(_value[i]);
				else
					output += _value[i];
			}

			// Move number at end if there at beginning
			if (startByNumber > 0)
				output = output.Substring(startByNumber, output.Length - startByNumber) + output.Substring(0, startByNumber);

			return output;
		}

		/// <summary>
		/// Returns a string using nomenclature of a file.
		/// </summary>
		/// <returns></returns>
		public static string FileNameClean(this string _value)
		{
			string output = "";
			bool nextUpper = true;

			if (string.IsNullOrEmpty(_value))
				_value = "unnamed";

			// Remove special characters
			for (int i = 0; i < _value.Length; i++)
			{
				bool nextUpperBuffer = nextUpper;
				nextUpper = false;

				if (Regex.IsMatch(_value[i].ToString(), "[^a-zA-Z0-9]"))
				{
					nextUpper = true;
					continue;
				}

				if (nextUpperBuffer)
					output += Char.ToUpper(_value[i]);
				else
					output += _value[i];
			}

			return output;
		}

		/// <summary>
		/// Converts this string to an hexadecimal format.
		/// </summary>
		public static string ConvertAsciiToHex(this string _ascii)
		{
			string hex = "";
			foreach (char c in _ascii)
			{
				int tmp = c;
				hex += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
			}
			return hex;
		}

		/// <summary>
		/// Converts this string to an ascii format.
		/// </summary>
		public static string ConvertHexToAscii(this string _hex)
		{
			string ascii = "";
			while (_hex.Length > 0)
			{
				ascii += System.Convert.ToChar(System.Convert.ToUInt32(_hex.Substring(0, 4), 16)).ToString();
				_hex = _hex.Substring(4, _hex.Length - 4);
			}
			return ascii;
		}

		/// <summary>
		/// Add spaces every thousands fo an integer
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public static string SeparateThousands(this int n, bool replaceSpaceByComa = false)
		{
			string s = "";
			string str = n.ToString();
			string space = replaceSpaceByComa ? "," : " ";

			//found the first index to insert a space
			int i;
			MathX.DivRem(str.Length, 3, out i);
			// init the string with the first part
			s += str.Substring(0, i);
			// add a space every 3 number
			while (i < str.Length - 2)
			{
				s += space + str.Substring(i, 3);
				i += 3;
			}

			if (s[0] == ' ' || s[0] == ',')
				return s.Substring(1);
			return s;

		}

		/// <summary>
		/// convert an int to a string like 12 225 => "12,2k"
		/// </summary>
		/// <param name="value"></param>
		/// <param name="ceil">if true ceil the hundred else floor it</param>
		/// <returns></returns>
		public static string Contract(this int value, bool ceil, int tmproLetterRatio = 100)
		{
			string suffix = "";

			int mainUnit = 0;
			int secondUnit = 0;
			int divider = 1;
			if (value >= 1000000)
			{
				divider = 1000000;
				suffix = "m";
			}
			else if (value >= 1000)
			{
				divider = 1000;
				suffix = "k";
			}

			int r1;
			mainUnit = MathX.DivRem(value, divider, out r1);
			int r2;
			secondUnit = MathX.DivRem(r1, divider / 10, out r2);

			if (ceil && r2 != 0)
			{
				if (secondUnit < 9)
					secondUnit++;
				else
				{
					secondUnit = 0;
					mainUnit++;
				}
			}

			if (tmproLetterRatio != 100)
				suffix = "<size=" + tmproLetterRatio + "%>" + suffix + "</size>";
			string s = mainUnit + "," + secondUnit.ToString("0") + suffix;

			return s;
		}

		/// <summary>
		/// convert an int to a string like 12 225 => "12,2k"
		/// </summary>
		/// <param name="value"></param>
		/// <param name="ceil">if true ceil the hundred else floor it</param>
		/// <returns></returns>
		public static string ToThousandK(this int value, bool ceil, int tmproKRatio = 100)
		{
			int r;
			int thousandUnit = MathX.DivRem(value, 1000, out r);
			int r1;
			int hundredUnits = MathX.DivRem(r, 100, out r1);
			if (ceil && r1 != 0)
			{
				if (hundredUnits < 9)
					hundredUnits++;
				else
				{
					hundredUnits = 0;
					thousandUnit++;
				}
			}

			string suffix = "k";
			if (tmproKRatio != 100)
				suffix = "<size=" + tmproKRatio + "%>k</size>";
			string h = "";
			if (hundredUnits != 0)
				h = "," + hundredUnits.ToString("0");
			string s = thousandUnit + h + suffix;

			return s;
		}

		/// <summary>
		/// Return a random alpha numeric string
		/// </summary>
		/// <param name="characterCount"></param>
		/// <returns></returns>
		public static string RandomAlphanumericString(int characterCount)
		{
			string ID = System.IO.Path.GetRandomFileName();
			ID = ID.Substring(0, characterCount);

			return ID;
		}
	}
}