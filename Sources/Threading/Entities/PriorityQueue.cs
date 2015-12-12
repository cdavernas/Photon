using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Threading
{

    /// <summary>
    /// Represents a queue of values prioritized thanks to the specified <see cref="IComparer{T}"/>
    /// </summary>
    /// <typeparam name="TValue">The type of the prioritized values</typeparam>
    /// <remarks>This implementation is based on the solution provided by Alexey Kurakin on http://www.codeproject.com/Articles/126751/Priority-queue-in-C-with-the-help-of-heap-data-str</remarks>
    public class PriorityQueue<TValue>
    {

        /// <summary>
        /// The <see cref="PriorityQueue{TValue}"/>'s base heap
        /// </summary>
        private SynchronizedCollection<TValue> _BaseHeap;
        /// <summary>
        /// The predicate used to get the priority of a value
        /// </summary>
        private Func<TValue, int> _GetPriorityPredicate;
        /// <summary>
        /// The <see cref="IComparer{T}"/> used to prioritize the values
        /// </summary>
        private IComparer<int> _PriorityComparer;

        /// <summary>
        /// Initializes a new <see cref="PriorityQueue{TValue}"/>
        /// </summary>
        /// <param name="getPriorityPredicate">The predicate used to retrieve the priority of a value</param>
        /// <param name="comparePredicate">The predicate used to prioritize values</param>
        public PriorityQueue(Func<TValue, int> getPriorityPredicate, Func<int, int, int> comparePredicate)
        {
            this._BaseHeap = new SynchronizedCollection<TValue>();
            this._GetPriorityPredicate = getPriorityPredicate;
            this._PriorityComparer = Comparer<int>.Create(new Comparison<int>(comparePredicate));
        }

        /// <summary>
        /// Gets the <see cref="PriorityQueue{TValue}"/>'s element count
        /// </summary>
        public int Count
        {
            get
            {
                return this._BaseHeap.Count;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="PriorityQueue{TValue}"/> is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (this.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Enqueues the specified value
        /// </summary>
        /// <param name="value">The value to enqueue to the <see cref="PriorityQueue{TValue}"/></param>
        public void Enqueue(TValue value)
        {
            this.Insert(value);
        }

        /// <summary>
        /// Dequeues and returns the element with the lowest/highest priority residing in the <see cref="PriorityQueue{TValue}"/>
        /// </summary>
        /// <returns>The element with the lowest/highest priority residing in the <see cref="PriorityQueue{TValue}"/></returns>
        public TValue Dequeue()
        {
            TValue value;
            if (this.IsEmpty)
            {
                throw new Exception("The PriorityQueue is empty and has therefore nothing to dequeue");
            }
            value = this._BaseHeap[0];
            this.DeleteRoot();
            return value;
        }

        /// <summary>
        /// Tries to dequeue the element with the lowest/highest priority residing in the <see cref="PriorityQueue{TValue}"/> and returns a boolean indicating whether or not the operation was succesfull
        /// </summary>
        /// <param name="value">The element with the lowest/highest priority residing in the <see cref="PriorityQueue{TValue}"/></param>
        /// <returns>True if an element was dequeued, false</returns>
        public bool TryToDequeue(out TValue value)
        {
            if (this.IsEmpty)
            {
                value = default(TValue);
                return false;
            }
            value = this.Dequeue();
            return true;
        }

        /// <summary>
        /// Inserts the value with the specified priority into the <see cref="PriorityQueue{TValue}"/>
        /// </summary>
        /// <param name="value">The value to enqueue to the <see cref="PriorityQueue{TValue}"/></param>
        private void Insert(TValue value)
        {
            this._BaseHeap.Add(value);
            // heapify after insert, from end to beginning
            this.HeapifyFromEndToBeginning(this._BaseHeap.Count - 1);
        }

        /// <summary>
        /// Heapifies the base heap, from the specified position to the end
        /// </summary>
        /// <param name="position">The position at which to start heapifying</param>
        private void HeapifyFromBeginningToEnd(int position)
        {
            int smallest, left, right;
            if (position >= this._BaseHeap.Count)
            {
                return;
            }
            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];
            while (true)
            {
                // on each iteration exchange element with its smallest child
                smallest = position;
                left = 2 * position + 1;
                right = 2 * position + 2;
                if (left < this._BaseHeap.Count &&
                    this._PriorityComparer.Compare(this._GetPriorityPredicate(this._BaseHeap[smallest]), this._GetPriorityPredicate(this._BaseHeap[left])) > 0)
                {
                    smallest = left;
                }
                if (right < _BaseHeap.Count &&
                    this._PriorityComparer.Compare(this._GetPriorityPredicate(this._BaseHeap[smallest]), this._GetPriorityPredicate(this._BaseHeap[right])) > 0)
                {
                    smallest = right;
                }
                if (smallest != position)
                {
                    this.ExchangeElements(smallest, position);
                    position = smallest;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Heapifies the base heap, from the start position to the specified position
        /// </summary>
        /// <param name="position">The position until which to heapify</param>
        private int HeapifyFromEndToBeginning(int position)
        {
            int parentPosition;
            if (position >= this._BaseHeap.Count)
            {
                return -1;
            }
            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];
            while (position > 0)
            {
                parentPosition = (position - 1) / 2;
                if (this._PriorityComparer.Compare(this._GetPriorityPredicate(this._BaseHeap[parentPosition]), this._GetPriorityPredicate(this._BaseHeap[position])) > 0)
                {
                    this.ExchangeElements(parentPosition, position);
                    position = parentPosition;
                }
                else
                {
                    break;
                }
            }
            return position;
        }

        /// <summary>
        /// Exchange the elements at the specified positions
        /// </summary>
        /// <param name="position1">The index of the first element to exchange</param>
        /// <param name="position2">The index of the second element to exchange</param>
        private void ExchangeElements(int position1, int position2)
        {
            TValue value;
            value = this._BaseHeap[position1];
            this._BaseHeap[position1] = this._BaseHeap[position2];
            this._BaseHeap[position2] = value;
        }

        /// <summary>
        /// Deletes the root of the <see cref="PriorityQueue{TValue}"/>
        /// </summary>
        private void DeleteRoot()
        {
            if (this._BaseHeap.Count <= 1)
            {
                this._BaseHeap.Clear();
                return;
            }
            this._BaseHeap[0] = this._BaseHeap[this._BaseHeap.Count - 1];
            this._BaseHeap.RemoveAt(this._BaseHeap.Count - 1);
            this.HeapifyFromBeginningToEnd(0);
        }

    }

}
