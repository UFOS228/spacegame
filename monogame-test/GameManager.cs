using System;
using System.Linq;
namespace monogametest
{
	public static class GameManager
	{
		public static bool HasItemInArray<T>(T[] array, T value)
		{
			//foreach (T item in array)
			//{
			//	if (item.Equals(value))
			//	{
			//		return true;
			//	}
			//}
			//return false;
			return array.Contains(value);
		}
	}
}

