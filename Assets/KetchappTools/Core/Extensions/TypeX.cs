using System;

namespace KetchappTools.Core.Extensions
{
	public static class TypeX
	{
		/// <summary>
		/// Returns the full name and the assembly of this Type.
		/// </summary>
		public static string GetFullNameAndAssembly(this Type type)
		{
			return type != null
					? type.FullName + ", " + type.Assembly.GetName().Name
					: "";
		}
	}
}