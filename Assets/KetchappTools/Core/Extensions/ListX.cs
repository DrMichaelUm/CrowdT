using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class ListX
	{
		/// <summary>
		/// Return last item T from the list.
		/// </summary>
		/// <returns>Last item T from the list</returns>
		public static T		LastItem<T>(this List<T> _list)
		{
			return _list[_list.Count - 1];
		}

		/// <summary>
		/// Return first item T from the list.
		/// </summary>
		/// <returns>First item T from the list</returns>
		public static T		FirstItem<T>(this List<T> _list)
		{
			return _list[0];
		}

		/// <summary>
		/// Shuffle items on the list randomly
		/// </summary>
		public static void	Shuffle<T>(this List<T> _list)
		{
			System.Random rng = new System.Random();
			int n = _list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = _list[k];
				_list[k] = _list[n];
				_list[n] = value;
			}
		}

		/// <summary>
		/// Shuffle items on the list randomly
		/// </summary>
		public static void	Shuffle<T>(this List<T> _list, int _seed)
		{
			//UnityEngine.Random.InitState(_seed);
			System.Random rng = new System.Random(_seed);
			int n = _list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				//int k = UnityEngine.Random.Range(0, n + 1);
				T value = _list[k];
				_list[k] = _list[n];
				_list[n] = value;
			}
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="_list">List to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static T		RandomItem<T>(this IList<T> _list)
		{
			if (_list.Count == 0) throw new System.IndexOutOfRangeException("Can't get a random item from an empty list !");
			return _list[UnityEngine.Random.Range(0, _list.Count)];
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="_list">List to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static T		RandomItem<T>(this IList<T> _list, int _seed)
		{
			if (_list.Count == 0) throw new System.IndexOutOfRangeException("Can't get a random item from an empty list !");
			System.Random rng = new System.Random(_seed);
			return _list[rng.Next(_list.Count)];
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="_list">List to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static int	RandomIndex<T>(this IList<T> _list)
		{
			if (_list.Count == 0) throw new System.IndexOutOfRangeException("Can't get a random item from an empty list !");
			return UnityEngine.Random.Range(0, _list.Count);
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="_list">List to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static int	RandomIndex<T>(this IList<T> _list, int _seed)
		{
			if (_list.Count == 0) throw new System.IndexOutOfRangeException("Can't get a random item from an empty list !");
			System.Random rng = new System.Random(_seed);
			return rng.Next(_list.Count);
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="_list">List to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static T		ShuffleRandomItem<T>(this List<T> _list, int _seed)
		{
			int count = _list.Count;
			if (count == 0) throw new System.IndexOutOfRangeException("Can't get a random item from an empty list !");
			List<T> list = new List<T>(_list);
			list.Shuffle(MathX.FloorToInt(_seed / count));
			//System.Random rng = new System.Random(_seed);
			//return _list[rng.Next(count)];
			return list[_seed % count];
		}

		/// <summary>
		/// Add item T on the list if not contain in the list
		/// </summary>
		/// <param name="_list">List to add the item from</param>
		/// <param name="_item">Item to add</param>
		public static void	AddIfNotContains<T>(this List<T> _list, T _item)
		{
			if (!_list.Contains(_item))
				_list.Add(_item);
		}

		/// <summary>
		/// Add list of items T on the list if not contain in the list
		/// </summary>
		/// <param name="_list">List to add the items from</param>
		/// <param name="_items">List of items to add</param>
		public static void	AddIfNotContains<T>(this List<T> _list, List<T> _items)
		{
			foreach (T iItem in _items)
				_list.AddIfNotContains(iItem);
		}

		/// <summary>
		/// Remove item T on the list if not contain in the list
		/// </summary>
		/// <param name="_list">List to revome the item from</param>
		/// <param name="_item">Item to remove</param>
		public static void	RemoveIfContains<T>(this List<T> _list, T _item)
		{
			if (_list.Contains(_item))
				_list.Remove(_item);
		}

		/// <summary>
		/// Remove list of items T on the list if not contain in the list
		/// </summary>
		/// <param name="_list">List to revome the items from</param>
		/// <param name="_items">List of items to remove</param>
		public static void	RemoveIfContains<T>(this List<T> _list, List<T> _items)
		{
			foreach (T iItem in _items)
				_list.RemoveIfContains(iItem);
		}
	}
}