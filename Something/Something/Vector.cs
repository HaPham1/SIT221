using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class Vector<T>
    {
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
        public int Capacity { get; private set; } = 0;

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
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
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

        // TODO:********************************************************************************************
        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.


        // Extend capacity first if out of capacity
        // If added to last index, use add method
        // If add in middle of the array, increase count, push the element after index up 1, then set the element to the index value specified.
        // If index out of range throw exception.
        public void Insert(int index, T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
            if (index == Count) { Add(element); }
            else if (index >= 0 && index < Count)
            {
                Count++;
                for (int i = Count; i > index; i--) data[i] = data[i - 1];
                data[index] = element;
            }
            else throw new IndexOutOfRangeException();
        }
        
        //Create empty array and assign the current one to it
        //Reset count to 0
        public void Clear()
        {
            T[] newData = new T[Count];
            data = newData;
            Count = 0;
        }

        // assign the value of IndexOf to a variable index
        // Check the value of index to output true or false
        public bool Contains(T element)
        {
            int index = IndexOf(element);
            if (index == -1) { return false; }
            else { return true; }
        }


        // Try remove at the index specified, if not return false, if ok return true
        public bool Remove(T element)
        {
            try
            {
                RemoveAt(IndexOf(element));
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        // Remove element at the index specified inside the vector, shift every element count toward index by 1, decrease count to fill the blank left
        // Throw error if not in index range
        public void RemoveAt(int index)
        {
            // You should replace this plug by your code.
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count; i++) data[i] = data[i + 1];
                Count--;
            }
            else { throw new IndexOutOfRangeException();  }
        }
       
        // Print out the vector in a readable manner
        // Not print if vector is empty
        // 1st: Print [ + 1st element
        // 2nd: Print inbetween + ,
        // Final: Print last element + ]
        public override string ToString()
        {
            if (Count == 0) return "Vector empty";
            string text = "";
            text = text +"[" + data[0] + ",";
            for (int i = 1; i < Count - 1; i++)
            {
                text = text + data[i] + ",";
            }
            text = text + data[Count - 1] + "]" ;
            return text;
        }
    }
}
