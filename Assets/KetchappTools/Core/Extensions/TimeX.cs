using System;
using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class	TimeX
	{
		/// <summary>
		/// Returns true if time is up.
		/// </summary>
		public static bool		IsTimeUp(float startTime, float duration)
		{
			return Time.time - startTime >= duration;
		}

		/// <summary>
		/// Returns the time progression between 0.0F and 1.0F.
		/// </summary>
		public static float		TimeProgression(float startTime, float duration)
		{
			return Mathf.Clamp01((Time.time - startTime) / duration);
		}

		/// <summary>
		/// Save the current DateTime in a PlayerPrefs.
		/// </summary>
		public static void		SaveActualDateTime()
		{
			PlayerPrefs.SetString("TimeX_LastSessionDateTime", DateTime.Now.ToBinary().ToString());
		}

		/// <summary>
		/// Returns the last DateTime saved if it exists, else returns the current DateTime.
		/// </summary>
		public static DateTime	GetDateTimeSaved()
		{
			string		date = PlayerPrefs.GetString("TimeX_LastSessionDateTime", DateTime.Now.ToBinary().ToString());
			long		temp = Convert.ToInt64(date);
			DateTime	lastDateTime = DateTime.FromBinary(temp);

			return lastDateTime;
		}

		/// <summary>
		/// Returns the TimeSpan since the last DateTime saved.
		/// </summary>
		public static TimeSpan	GetTimeSpan()
		{
			DateTime lastDateTime = GetDateTimeSaved();
			TimeSpan durationDateTime = DateTime.Now.Subtract(lastDateTime);

			return durationDateTime;
		}

		/// <summary>
		/// Returns the number of elapsed days since the last DateTime saved.
		/// </summary>
		public static double	GetDaysSinceLastDateTimeSave()
		{
			return GetTimeSpan().TotalDays;
		}


		/// <summary>
		/// Returns the number of elapsed hours since the last DateTime saved.
		/// </summary>
		public static double	GetHoursSinceLastDateTimeSave()
		{
			return GetTimeSpan().TotalHours;
		}


		/// <summary>
		/// Returns the number of elapsed minutes since the last DateTime saved.
		/// </summary>
		public static double	GetMinutesSinceLastDateTimeSave()
		{
			return GetTimeSpan().TotalMinutes;
		}


		/// <summary>
		/// Returns the number of elapsed seconds since the last DateTime saved.
		/// </summary>
		public static double	GetSecondsSinceLastDateTimeSave()
		{
			return GetTimeSpan().TotalSeconds;
		}

		/// <summary>
		/// Returns the number of elapsed milliseconds since the last DateTime saved.
		/// </summary>
		public static double	GetMillisecondsSinceLastDateTimeSave()
		{
			return GetTimeSpan().TotalMilliseconds;
		}
	}
}