using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class Heap<K, D> where K : IComparable<K>
    {

        // This is a nested Node class whose purpose is to represent a node of a heap.
        private class Node : IHeapifyable<K, D>
        {
            // The Data field represents a payload.
            public D Data { get; set; }
            // The Key field is used to order elements with regard to the Binary Min (Max) Heap Policy, i.e. the key of the parent node is smaller (larger) than the key of its children.
            public K Key { get; set; }
            // The Position field reflects the location (index) of the node in the array-based internal data structure.
            public int Position { get; set; }

            public Node(K key, D value, int position)
            {
                Data = value;
                Key = key;
                Position = position;
            }

            // This is a ToString() method of the Node class.
            // It prints out a node as a tuple ('key value','payload','index')}.
            public override string ToString()
            {
                return "(" + Key.ToString() + "," + Data.ToString() + "," + Position + ")";
            }
        }

        // ---------------------------------------------------------------------------------
        // Here the description of the methods and attributes of the Heap<K, D> class starts

        public int Count { get; private set; }

        // The data nodes of the Heap<K, D> are stored internally in the List collection. 
        // Note that the element with index 0 is a dummy node.
        // The top-most element of the heap returned to the user via Min() is indexed as 1.
        private List<Node> data = new List<Node>();

        // We refer to a given comparer to order elements in the heap. 
        // Depending on the comparer, we may get either a binary Min-Heap or a binary  Max-Heap. 
        // In the former case, the comparer must order elements in the ascending order of the keys, and does this in the descending order in the latter case.
        private IComparer<K> comparer;

        // We expect the user to specify the comparer via the given argument.
        public Heap(IComparer<K> comparer)
        {
            this.comparer = comparer;

            // We use a default comparer when the user is unable to provide one. 
            // This implies the restriction on type K such as 'where K : IComparable<K>' in the class declaration.
            if (this.comparer == null) this.comparer = Comparer<K>.Default;

            // We simplify the implementation of the Heap<K, D> by creating a dummy node at position 0.
            // This allows to achieve the following property:
            // The children of a node with index i have indices 2*i and 2*i+1 (if they exist).
            data.Add(new Node(default(K), default(D), 0));
        }

        // This method returns the top-most (either a minimum or a maximum) of the heap.
        // It does not delete the element, just returns the node casted to the IHeapifyable<K, D> interface.
        public IHeapifyable<K, D> Min()
        {
            if (Count == 0) throw new InvalidOperationException("The heap is empty.");
            return data[1];
        }

        // Insertion to the Heap<K, D> is based on the private UpHeap() method
        public IHeapifyable<K, D> Insert(K key, D value)
        {
            Count++;
            Node node = new Node(key, value, Count);
            data.Add(node);
            UpHeap(Count);
            return node;
        }

        private void UpHeap(int start)
        {
            int position = start;
            while (position != 1)
            {
                if (comparer.Compare(data[position].Key, data[position / 2].Key) < 0) Swap(position, position / 2);
                position = position / 2;
            }
        }

        // This method swaps two elements in the list representing the heap. 
        // Use it when you need to swap nodes in your solution, e.g. in DownHeap() that you will need to develop.
        private void Swap(int from, int to)
        {
            Node temp = data[from];
            data[from] = data[to];
            data[to] = temp;
            data[to].Position = to;
            data[from].Position = from;
        }

        public void Clear()
        {
            for (int i = 0; i<=Count; i++) data[i].Position = -1;
            data.Clear();
            data.Add(new Node(default(K), default(D), 0));
            Count = 0;
        }

        public override string ToString()
        {
            if (Count == 0) return "[]";
            StringBuilder s = new StringBuilder();
            s.Append("[");
            for (int i = 0; i < Count; i++)
            {
                s.Append(data[i + 1]);
                if (i + 1 < Count) s.Append(",");
            }
            s.Append("]");
            return s.ToString();
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        // Delete and return node at top, throw invalid if empty
        public IHeapifyable<K, D> Delete()
        {
            // If empty, throw exception
            if (Count == 0) throw new InvalidOperationException("The heap is empty.");

            //Asign min value to result
            Node result = data[1];

            //Swap min with value at last position then remove and update last position
            Swap(1, Count);
            data.RemoveAt(Count);
            Count--;

            //Downheap to restore heap-order property
            DownHeap(1);

            return result;
        }

        //Implement down heap operation to help restore heap-order property after removal
        private void DownHeap(int start)
        {
            //while still have at least a child
            while (2 * start <= Count)
            {
                //assign left as child
                int left = 2 * start;
                int child = left;
                // check if have right
                if (2* start + 1 <= Count)
                {
                    int right = 2 * start + 1;
                    // if right smaller then assign right as child
                    if (comparer.Compare(data[left].Key, data[right].Key) > 0 )
                    {
                        child = right;
                    }
                }
                // Compare child and our value, if child equal or larger then order ok, stop operation
                if (comparer.Compare(data[child].Key, data[start].Key) >= 0 )
                {
                    return;
                }
                // if not then swap, update start to child position to check again downward.
                else
                {
                    Swap(start, child);
                    start = child;
                }
            }
        }

        // Builds a minimum binary heap using the specified data according to the bottom-up approach.
        // Each element derived by key-value pair(keys[i], data[i])
        // Return array casted nodes
        // throw invalid if Heap<K,D> not empty
        public IHeapifyable<K, D>[] BuildHeap(K[] keys, D[] data)
        {
            //If not empty throw invalid operation
            if (Count != 0) throw new InvalidOperationException("Heap is not empty");

            //Create empty array casted node with specified length to store for return
            Node[] array = new Node[Math.Min(keys.Length, data.Length)];

            // Go through list and add node with key-value pair to heap as well as to array
            for (int i = 0; i < Math.Min(keys.Length, data.Length); i++)
            {
                Node node = new Node(keys[i], data[i], ++Count);
                this.data.Add(node);
                array[i] = node;
            }

            //Perform Downheap for the upper entries to maintain heap property
            heapify();

            return array;
        }

        //Go through upper entries and perform Downheap
        protected void heapify()
        {
            int start = Count / 2;
            for (int i = start; i > 0; i--)
            {
                DownHeap(i);
            }
        }


        //Decrease key of specified elemment
        // Throw invalid if node at specified position by element is different to element.
        public void DecreaseKey(IHeapifyable<K, D> element, K new_key)
        {
            //Cast element as a Node to operate on
            Node new_element = element as Node;

            //Check if node stored in the heap at position specified by element is same as element
            if (!new_element.Equals(data[new_element.Position])) throw new InvalidOperationException("the given element is inconsistent to the current state of the Heap<K, D>.");

            //Update new key then upheap
            new_element.Key = new_key;
            UpHeap(new_element.Position);
        }



        //remove element H[x] from min heap H of n elements with O(logn)
        //maintain heap property after deletion
        //Include comment about code running time
        public IHeapifyable<K, D> DeleteElement(IHeapifyable<K, D> element)
        {
            //Cast element as a Node to operate on -- Run time = 1
            Node specified_element = element as Node;

            //Throw error if heap empty -- Run time = 1
            if (Count == 0) throw new InvalidOperationException("The heap is empty.");

            //Prepare a node for return -- Run time = 1
            Node result = data[specified_element.Position];

            // Same operation as in delete min, swap to last element, remove, update Count -- Run time = 7
            Swap(specified_element.Position, Count);
            data.RemoveAt(Count);
            Count--;

            // Compare with the parent node to decide whether to upheap or downheap. -- Run time = 1 (for if) + logn (for upheap or downheap going through height of heap)
            if (comparer.Compare(data[specified_element.Position].Key, data[specified_element.Position / 2].Key) < 0)
            {
                UpHeap(specified_element.Position);
            }
            else
            {
                DownHeap(specified_element.Position);
            }

            // Hence, total runtime is something like O(logn + 12) = O(logn)
            return result;
        }



        //Find Kth min element with O(klogn) time, 1<= K <= n
        //Remember: insert , remove O(logn)
        //Also include running time comment
        public IHeapifyable<K, D> KthMinElement(int k)
        {
            //Create empty array to store the elements deleted -- Runtime = 1
            Node[] array = new Node[k];

            // Throw exception if k is out of range -- Runtime = 1
            if (k < 1 || k > Count) throw new ArgumentOutOfRangeException("Please specified a suitable number");

            // Delete min k time, since remove || delete is O(logn) -- Runtime = O(klogn)
            for (int i = 0; i < k; i++)
            {
                Node result = Delete() as Node;
                array[i] = result;
                
            }

            // Insert back what we deleted k times -- Runtime = O(klogn)
            for (int i = 0; i < k; i++)
            {
                this.Insert(array[i].Key, array[i].Data);
            }

            //Hence, total runtime should be around O(2klogn + 3) = O(klogn)
            return array[k-1];

        }

    }
}
