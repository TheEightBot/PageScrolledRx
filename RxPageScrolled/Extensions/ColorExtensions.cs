using System;
using Android.Graphics;

namespace RxPageScrolled
{
	public static class ColorExtensions
	{
		public static Color Lerp (this Color color, Color to, double amount)
		{
			return new Color(
				(int)LerpF (color.R, to.R, (float)amount), 
				(int)LerpF (color.G, to.G, (float)amount), 
				(int)LerpF (color.B, to.B, (float)amount));
		}

		static float LerpF (float value1, float value2, float amount) {
			return value1 + (value2 - value1) * amount;
		}
	}
}

