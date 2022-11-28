using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class Vector<T> : IEnumerable<T> where T : IComparable<T> {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity
        {
            get { return data.Length; }
        }

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[Capacity + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        public ISorter Sorter { set; get; } = new DefaultSorter();

        internal class DefaultSorter : ISorter
        {
            public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
            {
                if (comparer == null) comparer = Comparer<K>.Default;
                Array.Sort(sequence, comparer);
            }
        }

        public void Sort()
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            Sorter.Sort(data, null);
        }

        public void Sort(IComparer<T> comparer)
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            if (comparer == null) Sorter.Sort(data, null);
            else Sorter.Sort(data, comparer);
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public override string ToString()
        {
            if (Count == 0) return "Vector empty";
            string text = "";
            text = text + "[" + data[0] + ",";
            for (int i = 1; i < Count - 1; i++)
            {
                text = text + data[i] + ",";
            }
            text = text + data[Count - 1] + "]";
            return text;
        }


        // Implement 2 method required by the task sheet
        // This work by calling the recursiveBinarySearch method I implemented below.
        public int BinarySearch(T element, IComparer<T> comparer)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            int result = recursiveBinarySearch(data, 0, data.Length, element, comparer);
            return result;

        }

        public int BinarySearch(T element)
        {
            IComparer<T> comparer = Comparer<T>.Default;
            int result = recursiveBinarySearch(data, 0, data.Length, element, comparer);
            return result;
        }


        // Recursive binary search
        //
        public int recursiveBinarySearch(T[] data, int start, int end, T element, IComparer<T> comparer)
        {
            //Get the middle index
            int mid = (start + end) / 2;

            //if the element equal to the mid index, return it
            if (comparer.Compare(data[mid], element) == 0)
            {
                return mid;
            }
            //when end is equal or smaller than start || start larger or equal end  ==> finish searching and element is not found
            else if (start >= end)
            {
                return -1;
            }
            //search left half
            else if (comparer.Compare(element, data[mid]) < 0)
            {
                return recursiveBinarySearch(data, start, mid - 1, element, comparer);
            }
            //search remaining half
            else
            {
                return recursiveBinarySearch(data, mid + 1, end, element, comparer);
            }
        }
        // TODO: Add your methods for implementing the appropriate interface here

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new VectorEnumerator(this);
        }

        // TODO: Add an Iterator as an inner class here
        internal class VectorEnumerator : IEnumerator<T>
        {
            private Vector<T> _v;
            private int _currentIndex;

            public VectorEnumerator(Vector<T> v)
            {
                _v = v;
                _currentIndex = -1;
            }
            public bool MoveNext()
            {
                _currentIndex++;
                return (_currentIndex < _v.Count);
            }
            public T Current
            {
                get
                {
                    try
                    {
                        return _v[_currentIndex];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return default(T);
                    }
                }
            }
            public void Reset()
            {
                _currentIndex = -1;
            }
            public void Dispose() { }
            object IEnumerator.Current
            {
                get { return Current; }
            }

        }
    }
}