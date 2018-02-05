using System;
using System.Collections;
using System.Collections.Generic;

namespace MyExpression.Core
{
	/// <summary>
	/// Description of MySortedList.
	/// </summary>
	public class MySortedList<T> : IList<T>
		where T : IComparable
	{
		protected SortedList<T, bool> Base;

		public T this[int index]
		{
			get => Base.Keys[index];
			set => Base.Keys[index] = value;
		}

		public MySortedList()
		{
			Base = new SortedList<T, bool>();
		}

		public MySortedList(Comparer<T> comparer)
		{
			Base = new SortedList<T, bool>(comparer);
		}

		public int IndexOf(T item)
		{
			return Base.Keys.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			Add(item);
		}

		public void RemoveAt(int index)
		{
			Base.RemoveAt(index);
		}

		public void Add(T item)
		{
			Base.Add(item, true);
		}

		public void Clear()
		{
			Base.Clear();
		}

		public bool Contains(T item)
		{
			return Base.Keys.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Base.Keys.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return Base.Remove(item);
		}

		public int Count => Base.Values.Count;

		public bool IsReadOnly => Base.Keys.IsReadOnly;

		public IEnumerator<T> GetEnumerator()
		{
			return Base.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Base.Keys.GetEnumerator();
		}
	}
}
