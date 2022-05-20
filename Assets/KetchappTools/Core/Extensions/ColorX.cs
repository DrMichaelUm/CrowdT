using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class ColorExtensions
	{
		/// <summary>
		/// Returns a color from a hex representation, e.g FF0000.
		/// </summary>
		public static Color			HexToColor(this string s)
		{
			byte r = byte.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(s.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r, g, b, 255);
		}

		/// <summary>
		/// Change the color but keep the alpha.
		/// </summary>
		public static Color			WithColor(this Color _colorA, Color _colorB)
		{
			_colorA.r = _colorB.r;
			_colorA.g = _colorB.g;
			_colorA.b = _colorB.b;
			return _colorA;
		}

		/// <summary>
		/// Returns a copy of this color with the specified alpha.
		/// </summary>
		public static Color			WithAlpha(this Color _colorA, float _alpha)
		{
			_colorA.a = _alpha;
			return _colorA;
		}

		/// <summary>
		/// Generates a new Texture2D from the color with the specified size.
		/// </summary>
		/// <param name="_color">Color of the new texture</param>
		/// <param name="_width">Width of the new texture</param>
		/// <param name="_height">Height of the new texture</param>
		public static Texture2D		ToTexture2D(this Color _color, int _width = 1, int _height = 1)
		{
			Color[] pixels = new Color[_width * _height];

			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = _color;

			Texture2D texture2D = new Texture2D(_width, _height);
			texture2D.SetPixels(pixels);
			texture2D.Apply();

			return texture2D;
		}
	}
}
